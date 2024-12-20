using SDUnityExtension.Scripts.Pattern;
using UnityEngine;

namespace SDUnityExtension.Scripts.Manager
{
    public enum ToastLength
    {
        Short = 0,
        Long = 1,
    }

    public class SDDeviceManager : SDSingleton<SDDeviceManager>
    {
        [Header("Device")]

        [Tooltip("목표 FPS"), SerializeField] private int targetFPS = 300;
        public int TargetFPS
        {
            get => targetFPS;
            set
            {
                targetFPS = value;
                Application.targetFrameRate = targetFPS;
            }
        }

        [Tooltip("백그라운드 실행 여부"), SerializeField] private bool runInBackground = true;
        public bool RunInBackground
        {
            get => runInBackground;
            set
            {
                runInBackground = value;
                Application.runInBackground = runInBackground;
            }
        }

        [Tooltip("절전모드 실행 여부"), SerializeField] private bool neverSleep = true;
        public bool NeverSleep
        {
            get => neverSleep;
            set
            {
                neverSleep = value;
                Screen.sleepTimeout = neverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
            }
        }

        [Tooltip("화면 회전"), SerializeField] private ScreenOrientation screenOrientation = ScreenOrientation.LandscapeLeft;
        public ScreenOrientation ScreenOrientation
        {
            get => screenOrientation;
            set
            {
                screenOrientation = value;
                Screen.orientation = screenOrientation;
            }
        }
        
        public float ScreenWidth => Screen.width;
        public float ScreenHeight => Screen.height;

        private void Awake()
        {
            SetInstance(this, true);

            Application.targetFrameRate = targetFPS;
            Application.runInBackground = runInBackground;
            Screen.sleepTimeout = neverSleep ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;

            Screen.orientation = screenOrientation;

            if (Application.platform == RuntimePlatform.Android)
            {
                InitializeAndroidObjects();
            }
        }
    
        AndroidJavaObject currentActivity;
        AndroidJavaClass unityPlayer;
        AndroidJavaObject context;
        AndroidJavaObject toastInstance;

        public void InitializeAndroidObjects()
        {
#if UNITY_ANDROID
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
#endif
        }

        /// <summary>
        /// 안드로이드 Native Toast 메시지를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 Toast 메시지 내용</param>
        /// <param name="length">Toast 메시지 출력 시간</param>
        public void ShowToast(string message, ToastLength length = ToastLength.Short)
        {
#if UNITY_ANDROID
        currentActivity.Call
        (
            "runOnUiThread",
            new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass toast = new AndroidJavaClass("android.widget.Toast");

                AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", message);

                toastInstance = toast.CallStatic<AndroidJavaObject>
                (
                    "makeText", context, javaString, toast.GetStatic<int>(length.ToString())
                );

                toastInstance.Call("show");
            })
         );
#endif
        }

        /// <summary>
        /// 출력중인 Toast 메시지를 지웁니다.
        /// </summary>
        public void CancelToast()
        {
#if UNITY_ANDROID
        currentActivity.Call("runOnUiThread",
            new AndroidJavaRunnable(() =>
            {
                if (toastInstance != null) toastInstance.Call("cancel");
            }));
#endif
        }
    }
}