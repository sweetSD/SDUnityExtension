# SDUnityExtension
유니티를 조금 더 편하게 사용할 수 있게 해주는 유니티 패키지입니다.

## Installation
DoTween, UniTask의 추가적인 설치가 필요합니다.

**기능을 정상적으로 사용하기 위해서 패키지 Import 후 SDUnityExtension/Create Manager Prefab 버튼을 눌러서 씬에 매니저 오브젝트를 생성하여야 합니다.**

## SDSingleton
- 싱글톤 디자인 패턴입니다. 싱글톤 적용을 원하는 클래스에 다음과 같이 상속받아 사용합니다.

<pre>
<code>
public class SDDeviceManager : SDSingleton< SDDeviceManager >
{ ... }
</code>
</pre>

다음과 같이 싱글톤 객체에 접근합니다.

<pre>
<code>
SDDeviceManager.I.DoSomthing();
</code>
</pre>

DontDestroyOnLoad를 사용하고싶으면 Awake 혹은 Start 함수 등에서 SetInstance(this); 함수를 호출하면 됩니다.

## SDSecurityManager
- 싱글톤 변수로 접근하여 사용할 수 있습니다.

AES256 알고리즘을 사용하여 주어진 (문자열)데이터를 암호화 / 복호화 합니다.

<pre>
<code>
string encript = SDSecurityManager.I.Encrypt("Hello"); // print -> 설정한 Key, IV값에 따라 다르게 출력됩니다.
string decrypt = SDSecurityManager.I.Decrypt(data); // print -> Hello
</code>
</pre>

## SDSecurityPlayerPrefs
- 기본 PlayerPrefs를 래핑하는 클래스입니다. 암호화 / 복호화를 사용하여 유저가 데이터를 직접적으로 변경할 수 없도록 합니다.

<pre>
<code>
public bool HasKey(string key) // 해당 키가 존재하는지 확인합니다.

public void DeleteKey(string key) // 해당 키를 삭제합니다.

public void DeleteAll() // 모든 데이터를 삭제합니다.

public void Save() // 저장합니다.

public void SetInt(string key, int value) // Int 값을 저장합니다.

public void SetString(string key, string value) // string 값을 저장합니다.

public void SetFloat(string key, float value) // float 값을 저장합니다.

public int GetInt(string key, int defaultValue = 0) // int 값을 가져옵니다.

public string GetString(string key, string defaultValue = "") // string 값을 가져옵니다.

public float GetFloat(string key, float defaultValue = 0f) // float 값을 가져옵니다.
</code>
</pre>


## SDDeviceManager
- 실행 기기에 대한 정보와 기본 세팅 변수들이 있습니다.
<div>
<img src="https://github.com/sweetSD/SDUnityExtension/assets/29685039/3874f4b9-60f1-43c9-a96f-c433f1cd3b9b" width="40%" height="30%" title="SDDeviceManager" alt="SDDeviceManager"></img>
</div>

<pre>
<code>
public int TargetFPS; // 목표 FPS를 설정하는 변수입니다.

public bool RunInBackground; // 백그라운드에서 실행할지 여부입니다.

public bool NeverSleep; // 절전모드에 들어가지 않을지에 대한 여부입니다.

public ScreenOrientation ScreenOrientation; // 화면 회전에 대한 설정입니다.

public float ScreenWidth; // 화면의 가로 크기입니다.

public float ScreenHeight; // 화면의 세로 크기입니다.

public void ShowToast(string message, ToastLength length = ToastLength.Short) // (Android Only) 토스트 메시지를 출력합니다.

public void CancelToast() // (Android Only) 출력중인 토스트 메시지를 제거합니다.
</code>
</pre>

## SDSceneManager
- 씬을 로딩하는 매니저 클래스입니다.

```
public void LoadScene(int index) // Scene Index를 이용하여 새로운 Scene을 로딩합니다.

public void LoadScene(string name) // Scene Name을 이용하여 새로운 Scene을 로딩합니다.

public event Action<float> OnSceneLoadProgress // wScene Load Progress가 업데이트 될 때 마다 호출되는 Action입니다.

public event Action<float> OnSceneLoaded // Scene이 완전히 로딩 되었을 때 호출되는 Action입니다.
```

## GameObjectExtension, SDObjectManager
- GameObject의 추가적인 Extension 함수가 있습니다.

<pre>
<code>
// GameObjectExtension class
public static void SetActive(this GameObject obj, bool active, float seconds = 0f) // 주어진 seconds 뒤의 obj를 active 활성화 상태로 변경합니다.

// SDObjectManager class (Singleton)
public void SetActiveAfterSeconds(GameObject obj, bool active, float seconds = 0f) // 주어진 seconds 뒤의 obj를 active 활성화 상태로 변경합니다.

public void StopSetActive(GameObject obj) // 대기중인 SetActive가 있다면 취소합니다.
</code>
</pre>

## SDAudioManager
- AudioSource의 FadeIn / FadeOut 효과를 줍니다.

<pre>
<code>
AudioSource audioSource = GetComponent<AudioSource>();
audioSource.FadeIn(1f);
audioSource.FadeOut(1f);
</code>
</pre>

## SDObjectPool
- 오브젝트 풀 사용을 도와주는 스크립트입니다.

```
string PoolName; // 오브젝트 풀의 이름입니다.

List<GameObject> ObjectPool; // 생성된 오브젝트들을 관리하는 List입니다.

GameObject PoolObject; // 풀링할 오브젝트입니다.

int InitialSize; // 처음 생성할 오브젝트 풀의 사이즈입니다.

Transform PoolRoot; // 생성된 오브젝트들의 부모가 될 Transform입니다.

bool Flexible; // 오브젝트 요청 시 풀의 오브젝트를 모두 사용중일 경우 추가로 생성할지 여부입니다.

/// <summary>
/// 오브젝트 풀에서 오브젝트 하나를 가져옵니다.
/// </summary>
/// <param name="posVec3">위치</param>
/// <param name="rotVec3">회전</param>
/// <param name="scaleVec3">스케일</param>
/// <param name="doNotActive">오브젝트를 활성화 하지 않고 반환할지 여부</param>
/// <returns>T 형식으로 변환된 오브젝트</returns>
public T ActiveObject<T>(Vector3 posVec3, Vector3 rotVec3, Vector3? scaleVec3 = null, bool doNotActive = false) where T : Component
public T ActiveObject<T>(Vector3 posVec3, Quaternion rot, Vector3? scaleVec3 = null, bool doNotActive = false) where T : Component
  
public GameObject ActiveObject(Vector3 posVec3, Quaternion rot, Vector3? sclVec3 = null, bool doNotActive = false) // GameObject로 반환합니다.
  
public static SDObjectPool GetPool(string name) // 오브젝트 풀의 이름으로 풀을 가져옵니다.

```

## SDMath
- Unity의 Mathf에는 없는 추가적인 수학 함수를 제공합니다.

```
SDMath.Map(value, in_min, in_max, out_min, out_max)
주어진 in 구간의 value를 out 구간의 값으로 변환합니다.
Ex. (value = 10. in_min = 0, in_max = 100, out_min = 0, out_max = 1) -> return 0.1
```

## DoTween Extension
- DoTween의 기능을 편하게 사용할 수 있도록 도와주는 스크립트입니다. 추가적인 스크립트를 사용하지 않고 컴포넌트를 추가하여 사용할 수 있습니다.

- DoBase.cs - 확장 스크립트의 기반이 되는 추상 클래스입니다.
- DoController.cs - DoBase를 상속한 모든 클래스를 관리하는 클래스입니다.

- DoColor.cs - SpriteRenderer의 color를 사용합니다.

- DoPosition.cs - Transform의 localPosition / position을 사용합니다.
- DoRotation.cs - Transform의 localRotation / rotation을 사용합니다.
- DoScale.cs - Transform의 localScale을 사용합니다.
- DoPunchPosition.cs Transform의 position을 사용합니다.
- DoPunchRotation.cs Transform의 rotation을 사용합니다.
- DoPunchScale.cs Transform의 localScale을 사용합니다.

- DoUIColor.cs - Graphic 클래스를 상속받은 UI의 color를 사용합니다.
- DoUIFade.cs - CanvasGroup의 alpha를 사용합니다.
- DoUIFill.cs - Image 클래스의 fillAmount를 사용합니다.
- DoUIPosition.cs - RectTransform의 anchoredPosition를 사용합니다.

## Network Image
- 네트워크 상에서 받아온 이미지를 적용시켜야 할 때 사용할 수 있습니다.
- 로컬 캐싱 기능을 제공합니다.
- 실제 처리 로직은 Static Method를 사용했기 때문에 컴포넌트를 사용하지 않고 텍스쳐를 스크립트로 받아올 수 있습니다.

<div>
<img src="https://github.com/sweetSD/SDUnityExtension/assets/29685039/40549b3a-4658-4b6f-9602-1bd2278ccb8d" width="40%" height="30%" title="SDDeviceManager" alt="SDDeviceManager"></img>
</div>

```
public string Url // 불러올 이미지의 Url입니다. (Unity Web Request 특성인지 Url의 끝이 꼭 .png, .jpg 등으로 끝나야 정상 작동합니다.)

private Image image // 불러온 이미지를 적용시킬 Image 컴포넌트입니다.

private bool useCache = true // 지정된 Url에 한하여 로컬 캐싱을 진행할지 여부입니다.
```


앞으로 계속 업데이트 될 예정입니다.
