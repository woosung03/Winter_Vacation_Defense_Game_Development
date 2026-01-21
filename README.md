# Winter_Vacation_Defense_Game_Development
## 문제 해결 과정 (Troubleshooting)

본 프로젝트 개발 과정에서 Unity 환경 설정, 스크립트 연결, UI 연동 등에서 여러 문제가 발생하였으며,  
각 문제를 원인 분석 → 해결 → 개선의 흐름으로 해결하였다.

---

### 1. Unity 프로젝트와 코드 편집기(Cursor) 간 경로 불일치 문제

#### 문제 상황
- Cursor에서 작성한 스크립트가 Unity 에디터에 보이지 않음
- Unity에서는 `GameManager.cs`만 존재하고, 새로 작성한 스크립트가 인식되지 않음

#### 원인 분석
- Unity 프로젝트 경로와 Cursor가 참조하는 작업 폴더 경로가 서로 달랐음
  - Unity: `C:\Game\Winter_Vacation_Defense_Game_Development`
  - Cursor: `C:\Users\...\Winter_Vacation_Defense_Game_Development`

#### 해결 방법
- Cursor의 워크스페이스를 Unity 프로젝트가 위치한 경로로 재설정
- 이후 생성한 스크립트가 즉시 Unity에 반영됨

#### 개선점
- 개발 도구 사용 시 **프로젝트 루트 경로를 먼저 통일**해야 함을 인지
- Git 기반 프로젝트에서는 항상 `.sln` 또는 `Assets` 기준으로 작업 경로를 확인

---

### 2. 스크립트 생성 후 Unity에서 Add Component가 되지 않는 문제

#### 문제 상황
- `UpgradeUI.cs` 스크립트를 추가했으나
- Unity에서 `Can't add script component` 오류 발생

#### 원인 분석
- 스크립트 내부에서 참조 중인 타입이 컴파일되지 않아 Unity가 클래스를 인식하지 못함
- 주요 원인:
  - namespace 누락
  - TextMeshPro 관련 컴파일 에러
  - 변수 선언 누락 (`upgradeManager`)

#### 해결 방법
- `using Systems;`, `using TMPro;` 등 필요한 네임스페이스 명시
- 스크립트 파일명과 클래스명 일치 여부 확인
- Console 에러를 모두 해결한 후 다시 Add Component 수행

#### 개선점
- Unity는 **컴파일 에러가 하나라도 있으면 새 스크립트를 붙일 수 없다는 점**을 이해
- Console 에러 확인을 최우선 디버깅 단계로 습관화

---

### 3. UI 스크립트 슬롯 연결 개념 이해 부족

#### 문제 상황
- “UpgradeUI.cs를 빈 UI 오브젝트에 붙이고 슬롯 연결”이라는 설명이 이해되지 않음
- UI 스크립트가 어디에 붙는지 혼란 발생

#### 원인 분석
- UI 로직과 게임 로직의 역할 구분에 대한 이해 부족
- Unity Inspector의 직렬화 필드(슬롯) 개념 미숙

#### 해결 방법
- Canvas 하위에 빈 GameObject 생성
- 해당 오브젝트에 UI 관리 스크립트 부착
- TextMeshPro, Button 컴포넌트를 Inspector 슬롯에 직접 드래그하여 연결

#### 개선점
- UI 스크립트는 “보여주는 역할”, 게임 시스템은 “계산/처리 역할”이라는 구조적 이해 확보

---

### 4. 적이 생성되지 않는 문제 (Enemy Spawner)

#### 문제 상황
- Play 시 적이 전혀 생성되지 않음
- 이전에는 정상 동작하던 기능이 갑자기 작동하지 않음

#### 원인 분석
- `EnemySpawner`의 `Enemy Prefab` 슬롯이 비어 있었음
- 프리팹 이동/재설정 과정에서 참조가 끊어짐

#### 해결 방법
- Project 창의 Enemy 프리팹을 다시 Spawner 슬롯에 연결
- 씬 저장 후 재실행하여 정상 스폰 확인

#### 개선점
- Prefab 참조는 씬 저장 전 반드시 확인
- 프리팹은 고정된 경로(`Assets/Prefabs`)에서 관리

---

### 5. 모바일 화면 비율(19:9)을 선택했으나 가로로 출력되는 문제

#### 문제 상황
- Game View에서 `Mobile 19:9`를 선택했으나 화면이 가로로 표시됨

#### 원인 분석
- PC(Windows) 플랫폼에서는 화면 방향(Portrait/Landscape) 설정이 존재하지 않음
- Android 플랫폼으로 전환하지 않은 상태에서 설정을 변경하려 했음

#### 해결 방법
- Build Settings에서 플랫폼을 Android로 변경
- Player Settings → Resolution and Presentation에서 Portrait 고정 설정

#### 개선점
- Unity의 플랫폼별 Player Settings 차이를 이해
- 모바일 게임 개발 시 초기 단계에서 플랫폼 전환을 우선 진행

---

### 6. 기능 확장 중 기존 시스템에 영향이 발생하는 문제

#### 문제 상황
- 업그레이드 시스템 및 UI 추가 후 기존 기능(적 스폰 등)이 비정상 동작

#### 원인 분석
- 컴파일 에러 또는 참조 누락 시 일부 스크립트가 실행되지 않음
- 기능 추가가 기존 시스템에 간접적 영향을 줄 수 있음

#### 해결 방법
- 기능 추가 후 반드시 Console 에러 확인
- 시스템 단위로 기능 분리하여 의존성 최소화

#### 개선점
- “기능 추가 → 에러 확인 → 테스트” 순서를 개발 루틴으로 정착

---

## 문제 해결을 통해 얻은 교훈

- Unity 개발에서는 **에러 메시지와 Console 로그가 가장 중요한 단서**
- 기능 자체보다 **구조 설계와 참조 관계 관리**가 안정성을 좌우함
- 작은 문제라도 원인을 분석하고 기록하면 이후 개발 속도가 크게 향상됨
