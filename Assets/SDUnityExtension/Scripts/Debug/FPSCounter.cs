using UnityEngine;
using UnityEngine.Serialization;

public class FPSCounter : MonoBehaviour
{
    private static readonly Vector2 NativeScreenSize = new(640, 480);
    
    [SerializeField, Tooltip("Viewport 좌표계 기준 위치 좌표\nTop-Left: (0, 0), Bottom-Right: (1, 1)")] 
    private Vector2 textPosition;
    [SerializeField, Tooltip("Text 크기")] 
    private int textSize = 10;
    [SerializeField, Tooltip("Text 정렬")] 
    private TextAnchor textAlignment = TextAnchor.UpperLeft;
    
    private float timeCounter = 0f;
    private int tick = 0;
    private int averageFps = 0;

    private void Update()
    {
        timeCounter += Time.unscaledDeltaTime;
        tick++;

        if (timeCounter < 1) return;
        
        timeCounter -= 1;
        averageFps = tick;
        tick = 0;
    }

    private void OnGUI()
    {
        var udt = Time.unscaledDeltaTime;
        var text = $"Avg. {averageFps} ({1000f / averageFps}ms)\nCur. {Mathf.RoundToInt(1 / Time.unscaledDeltaTime)} ({udt * 1000}ms)";
        var position = new Vector2(Screen.width * textPosition.x, Screen.height * textPosition.y);
        var style = new GUIStyle()
        {
            fontSize = (int)(textSize * (Screen.width / NativeScreenSize.x)),
            clipping = TextClipping.Overflow,
            alignment = textAlignment
        };
        GUI.Label(new Rect(position, Vector2.zero), text, style);
    }
}
