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

![image](https://user-images.githubusercontent.com/31693348/133869602-266e1814-9d4b-48ee-b372-96cfd46d8f9f.png)

상단에 InputSystem 메뉴를 들어가서 Set Input system을 누르면 InputSystem Editor창이 뜨게됩니다. 사용자는 원하는 위치에 사용할 키의 정보들이 들어갈 InputSetting을 해당 위치에 지정후 **Refresh**하게 되면 해당 InputSetting을 불러오게 됩니다.



![image](https://user-images.githubusercontent.com/31693348/133869684-4d001ce2-7274-4227-99e2-99d03faa3dbb.png)

사용할 키들을 Key 리스트에 string : Name과 KeyCode : Code를 입력한 뒤 **Apply Input System**을 누를경우 하나의 파일(InputNames)이 생성됩니다. 해당 파일엔 방금 사용자가 지정한 Key들의 이름과 InputSetting의 위치를 담고 있습니다.



![image](https://user-images.githubusercontent.com/31693348/133869738-a3c32617-80dc-4d53-9026-63256d2e2b66.png)

사용자는 InputManager의 GetKey 메소드를 이용해서 자신이 정의한 Key들의 상태를 불러올 수 있습니다.

![image](https://user-images.githubusercontent.com/31693348/133869766-814d9211-1925-4716-9d63-8ca401778ad6.png)