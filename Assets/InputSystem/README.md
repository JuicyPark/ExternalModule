# InputSystem

## 디렉토리

- Assets폴더
  - InputSystem폴더
    - Editor폴더
      - [InputSystemEditor.cs](https://github.com/JuicyPark/ExternalModule/blob/main/Assets/InputSystem/Editor/InputSystemEditor.cs)
    - [InputManager.cs](https://github.com/JuicyPark/ExternalModule/blob/main/Assets/InputSystem/InputManager.cs)
    - [InputSetting.cs](https://github.com/JuicyPark/ExternalModule/blob/main/Assets/InputSystem/InputSetting.cs)

***

## 작업로그

[Input System 모듈개발 #4](https://github.com/ECONO-UNION/union-mentoring-1-Unity/pull/4)

[PlayerInput 개발 및 InputSystem 로직 개선 #10](https://github.com/ECONO-UNION/union-mentoring-1-Unity/pull/10)

[키변경 #15](https://github.com/ECONO-UNION/union-mentoring-1-Unity/pull/15)

***

## 사용방법

![image](https://user-images.githubusercontent.com/31693348/133870084-016571ce-8cf5-4593-b2c2-b42920bc800b.png)

상단에 InputSystem 메뉴를 들어가서 Set Input system을 누르면 InputSystem Editor창이 뜨게됩니다. 사용자는 원하는 위치에 사용할 키의 정보들이 들어갈 InputSetting을 해당 위치에 지정후 **Refresh**하게 되면 해당 InputSetting을 불러오게 됩니다.



![image](https://user-images.githubusercontent.com/31693348/133870102-d6daaff1-3bb7-47b3-be9c-9ad2c4668295.png)

사용할 키들을 Key 리스트에 string : Name과 KeyCode : Code를 입력한 뒤 **Apply Input System**을 누를경우 하나의 파일(InputNames)이 생성됩니다. 해당 파일엔 방금 사용자가 지정한 Key들의 이름과 InputSetting의 위치를 담고 있습니다.



![image](https://user-images.githubusercontent.com/31693348/133870109-df4aeaf6-aa13-4d08-a2ec-0489ca94667a.png)

사용자는 InputManager의 GetKey 메소드를 이용해서 자신이 정의한 Key들의 상태를 불러올 수 있습니다.

![image](https://user-images.githubusercontent.com/31693348/133870117-07f471a9-8294-4e6f-8f70-44ca6a082616.png)