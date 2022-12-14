using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float refreshRate = 0.1f;
    private float timeCounter = 0f;

    private void Start()
    {
        if (text == null) text = GetComponent<Text>();
    }

    private void Update()
    {
        if (timeCounter >= refreshRate)
            text.text = $"{Mathf.Floor(1 / Time.deltaTime)} FPS";
        
        timeCounter += Time.deltaTime;
    }
}
