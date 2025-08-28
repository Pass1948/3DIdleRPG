<img width="400" height="300" alt="image" src="https://github.com/user-attachments/assets/3b8cfda7-68a0-49c4-8a42-1694a4975c69" />

- # 🕹️ 3D 이벤토리 기능과 상태머신 구현 프로젝트
프로젝트 소개 > 방치형rpg의 자동공격하는 플레이어의 상태머신과 UI인벤토리기능에 아이템과 스탯 데이터를 연결하는 과정을 담은 프로젝트입니다
---
## 📅 개발 기간
- 시작: 2025.08.25  
- 종료: 2025.08.29  

## 🧰 개발 환경
- **Engine**: Unity 2022.3.17f1 (LTS)
- **Language**: C#
- **IDE**: JetBrains Rider / Visual Studio 2022
- **Target**: Windows (PC) *(선택적으로 Android/iOS 확장 가능)*
- **Version Control**: Git + GitHub

---
## 🧩 게임 주요 기능

### 1) 플레이어가 자동으로 적을 추적 및 공격
- 플레이어의 상태는
- 1.기본(탐색)상태 : 탐색범위 내에 있는 Enemy 태그가 달린 오브젝트를 탐색함
- 2.추적상태 : 탐색된 타겟을 프레임마다 위치를 수정하여 추적함
- 3.공격상태 : 공격범위내에 타겟이 들어오면 추적에서 공격상태로 바뀌고 공격을 진행함

### 2) UI 상태창
- 화면 오른쪽에 Status와 Inventory 버튼이 있음
- 그중 Status를 누르면 플레이어의 스탯 상태창이 뜨게됨
- <img width="300" height="250" alt="image" src="https://github.com/user-attachments/assets/703d7db0-e023-49bf-a918-47e620f638b6" />

### 3) UI 인벤토리
- 화면 오른쪽에 Inventory 버튼을 누르면 인벤토리가 뜨게 되고
- 아래 Add Item Button이라는 버튼을 누르게 되면 아이템이 추가됨
- 아이템을 누르면 Equip버튼이 나타나고 누르면 아이템을 장착하게됨
- 그리고 장착된 아이템을 다시 누르면 해제하기 버튼이 활성화 되고 누르면 아이템을 해제하게됨
- <img width="300" height="350" alt="image" src="https://github.com/user-attachments/assets/bed6a5fb-5e87-4e5b-899a-31a88e2c8dca" />
- <img width="300" height="350" alt="image" src="https://github.com/user-attachments/assets/1880d19f-a91d-4cd7-be51-65555a6bac16" />

### 6) 캐릭터와 아이템 데이터 관리
- ScriptableObject로 관리하여 클래스에 바인딩하여 사용
- <img width="600" height="650" alt="image" src="https://github.com/user-attachments/assets/9aeee00b-34f5-40c3-bd97-592081fa24de" />
