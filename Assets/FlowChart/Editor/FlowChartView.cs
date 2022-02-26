using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace JuicyFlowChart
{
    public class FlowChartView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<FlowChartView, GraphView.UxmlTraits> { }
        private FlowChart _flowChart;

        public Action<NodeView> OnNodeSelected { get; internal set; }

        public FlowChartView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(FlowChartEditorPath.ussPath);
            styleSheets.Add(styleSheet);
        }

        internal void ShowView(FlowChart flowChart)
        {
            _flowChart = flowChart;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements.ToList());
            graphViewChanged += OnGraphViewChanged;

            DrawNode();
            DrawEdge();
        }

        internal void ClearView()
        {
            DeleteElements(graphElements.ToList());
        }

        private void DrawNode()
        {
            _flowChart.Nodes.ForEach(node => CreateNodeView(node));
        }

        private void DrawEdge()
        {
            _flowChart.Nodes.ForEach(node =>
            {
                node.ChildrenID.ForEach(childID =>
                {
                    NodeView parentView = FindNodeView(node.ID);
                    NodeView childView = FindNodeView(childID);

                    Edge edge = parentView.Output.ConnectTo(childView.Input);
                    AddElement(edge);
                });
            });
        }

        private NodeView FindNodeView(int nodeID)
        {
            return GetNodeByGuid(nodeID.ToString()) as NodeView;
        }


        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node, node.ID == _flowChart.RootID, () => EditorUtility.SetDirty(_flowChart));
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        /// <summary>
        /// 그래프 뷰 내에서 특정 이벤트(삭제, 엣지생성, 노드이동 등)가 발생했을 경우 실행되는 콜백함수
        /// </summary>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (_flowChart == null || Application.isPlaying)
                return graphViewChange;

            // Delete Node
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(element =>
                {
                    NodeView nodeView = element as NodeView;
                    if (nodeView != null)
                    {
                        _flowChart.DeleteNode(nodeView.Node);
                    }

                    Edge edge = element as Edge;
                    if (edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        _flowChart.RemoveChild(parentView.Node, childView.Node);
                    }
                });
            }

            // Create Edge
            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    _flowChart.AddChild(parentView.Node, childView.Node);
                });
            }
            return graphViewChange;
        }

        /// <summary>
        /// 마우스 우클릭시 실행되는 콜백함수
        /// </summary>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_flowChart == null || Application.isPlaying)
                return;

            evt.menu.AppendSeparator();
            ShowNodeTypes<Action>(evt);
            ShowNodeTypes<Condition>(evt);
        }

        private void ShowNodeTypes<T>(ContextualMenuPopulateEvent evt) where T : Task
        {
            VisualElement contentViewContainer = ElementAt(1);
            Vector3 screenMousePosition = evt.localMousePosition;
            Vector2 worldMousePosition = screenMousePosition - contentViewContainer.transform.position;
            worldMousePosition *= 1 / contentViewContainer.transform.scale.x;

            var types = TypeCache.GetTypesDerivedFrom<T>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Create {type.BaseType.Name}/{type.Namespace} {type.Name}", (actionEvent) =>
                {
                    Node node = _flowChart.CreateNode(type.Name, type.Namespace, type.BaseType.Name, worldMousePosition);
                    CreateNodeView(node);

                    if (_flowChart.RootID == 0)
                    {
                        Node root = _flowChart.CreateRootNode(node);
                        CreateNodeView(root);
                        DrawEdge();
                    }
                });
            }
        }

        /// <summary>
        /// Edge 생성시 Edge를 붙힐 수 있는 가능한 Port List를 반환해주는 함수
        /// </summary>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            if (Application.isPlaying)
            {
                List<Port> emptyPort = new List<Port>();
                return emptyPort;
            }

            return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
        }

        internal void UpdateNodeState()
        {
            nodes.ForEach((n) =>
            {
                NodeView view = n as NodeView;
                view.UpdateState();
            });
        }
    }
}