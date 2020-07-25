# SDUnityExtension
유니티를 조금 더 편하게 사용할 수 있게 해주는 유니티 패키지입니다.

## SDSingleton
- 싱글톤 디자인 패턴입니다. 싱글톤 적용을 원하는 클래스에 다음과 같이 상속받아 사용합니다.

<pre>
<code>
public class SDDeviceManager : SDSingleton<SDDeviceManager>
{ ... }
</code>
</pre>

다음과 같이 싱글톤 객체에 접근합니다.

<pre>
<code>
SDDeviceManager.I.DoSomthing();
</code>
</pre>

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
<img src="https://user-images.githubusercontent.com/29685039/88453266-5dae1380-cea0-11ea-889f-7b325d3eea34.png" width="40%" height="30%" title="SDDeviceManager" alt="SDDeviceManager"></img>
</div>

<pre>
<code>
public int TargetFPS; // 목표 FPS를 설정하는 변수입니다.

public bool runInBackground; // 백그라운드에서 실행할지 여부입니다.

public bool isNeverSleep; // 절전모드에 들어가지 않을지에 대한 여부입니다.

public ScreenOrientation ScreenOrientation; // 화면 회전에 대한 설정입니다.

public float ScreenWidth; // 화면의 가로 크기입니다.

public float ScreenHeight; // 화면의 세로 크기입니다.

[SerializeField] private string[] _requiredPermission; // 필수로 받아야 할 접근 권한 목록입니다.

[SerializeField] private string[] _optionalPermission; // 선택적으로 받는 접근 권한 목록입니다.

public void ShowToast(string message, E_TOAST length = E_TOAST.LENGTH_SHORT) // (Android Only) 토스트 메시지를 출력합니다.

public void CancelToast() // (Android Only) 출력중인 토스트 메시지를 제거합니다.
</code>
</pre>

접근 권한 요청 기능은 [Android Runtime Permission](https://github.com/yasirkula/UnityAndroidRuntimePermissions)플러그인을 사용하였습니다.

>접근 권한을 사용하기 위해서는 {Project}/Assets/Plugins/Android/AndroidManifest.xml 파일에 해당 권한에 대한 구문을 추가해야합니다.
```
<manifest ...>
  <application ...>
  </application>
  <uses-permission android:name="android.permission.CAMERA" />
</manifest>
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

앞으로 계속 업데이트 될 예정입니다.
