# Unity Damage Popup Optimization

2026-1 FOSS Project

Unity Action Pattern과 Object Pooling을 활용한 데미지 팝업 UI 최적화 프로젝트입니다.  
데미지 팝업 시스템을 `Before / After` 구조로 구현하고, Unity Profiler를 통해 성능 차이를 비교했습니다.

전투 상황에서 데미지 팝업 UI는 짧은 시간 안에 반복적으로 생성될 수 있습니다.  
단순히 `Instantiate / Destroy` 방식으로 구현하면 오브젝트 생성 비용, 메모리 할당, GC Alloc 증가로 인해 성능 저하가 발생할 수 있습니다.

본 프로젝트에서는 C# `Action`을 활용한 이벤트 기반 구조와 Object Pooling을 적용하여, 데미지 팝업 UI를 재사용하는 방식으로 개선했습니다.

### Tech Stack

- **Engine**: Unity
- **Language**: C#
- **Tool**: Unity Profiler

### Key Features

- Unity 3D 데미지 팝업 UI 구현
- `Instantiate / Destroy` 방식의 문제점 분석
- C# `Action` 기반 이벤트 구조 적용
- Object Pooling을 통한 UI 오브젝트 재사용
- Unity Profiler를 활용한 Before / After 비교

### Before

Before 버전에서는 데미지 발생 시마다 `Instantiate`로 새로운 UI 오브젝트를 생성하고, 연출이 끝난 뒤 `Destroy`로 제거했습니다.  
구현은 직관적이지만, 많은 데미지 팝업이 짧은 시간 안에 반복 생성될 경우 메모리 할당과 GC Alloc이 증가할 수 있습니다.

### After

After 버전에서는 데미지 팝업 오브젝트를 미리 생성해 Pool에 저장하고, 필요할 때 꺼내 사용한 뒤 다시 반환하는 구조로 개선했습니다.  
또한 C# `Action`을 활용해 Monster의 데미지 처리 로직과 DamageManager의 UI 표시 로직을 분리했습니다.

### Before / After Comparison

동일한 수의 몬스터에게 동일한 방식으로 데미지를 적용한 뒤, Unity Profiler를 통해 성능 차이를 확인한다.

- 프레임 안정성
- 데미지 팝업 생성 시 CPU 사용량
- 메모리 할당량
- GC Alloc 발생 여부
- Object 생성 및 삭제 횟수
- 코드 구조의 유지보수성

Before 버전은 구조가 단순하지만, 많은 팝업이 발생할수록 생성과 삭제 비용이 증가한다. 반면 After 버전은 초기 구조가 조금 더 복잡하지만, 반복 사용 상황에서 훨씬 안정적이고 확장성이 높다.

| 항목 | Before | After |
|---|---|---|
| 데미지 발생 시 | Instantiate | Dequeue |
| 연출 종료 시 | Destroy | Enqueue |
| Monster 구조 | UI 직접 호출 | Event 발생 |
| UI 관리 | 개별 처리 | DamageManager 집중 관리 |
| 성능 | Spike 발생 가능 | 더 안정적 |
