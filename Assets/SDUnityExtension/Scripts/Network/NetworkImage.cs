using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NetworkImage : MonoBehaviour
{
    [SerializeField] private string currentUrl;
    public string Url
    {
        get => currentUrl;
        set
        {
            currentUrl = value;
            StartCoroutine(CO_LoadImageFromUrl(currentUrl));
        }
    }

    [SerializeField] private Image image;
    [SerializeField] private bool useCache = true;

    private void Start()
    {
        if (image == null) image = GetComponent<Image>();
        if (currentUrl.IsNotEmpty()) Url = currentUrl;
    }

    private IEnumerator CO_LoadImageFromUrl(string url)
    {
        string directoryPath = Application.persistentDataPath + "/Cache/";
        string path = directoryPath + url.GetHashCode() + ".png";
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        if (File.Exists(path) && useCache)
        {
            var bytes = File.ReadAllBytes(path);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            texture.Apply();
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector3.one * 0.5f);
        }
        else
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector3.one * 0.5f);

                    if(useCache)
                    {
                        var bytes = texture.EncodeToPNG();
                        File.WriteAllBytes(path, bytes);
                    }
                }
                else
                {
                    Debug.LogError(uwr.error);
                }

            }
        }
    }
}
