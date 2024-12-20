using System.IO;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Extension;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace SDUnityExtension.Scripts.Network
{
    [RequireComponent(typeof(Image))]
    public class NetworkImage : MonoBehaviour
    {
        [SerializeField] private string url;
        public string Url
        {
            get => url;
            set 
            {
                url = value;
                LoadImageFromUrl(url, useCache).ContinueWith(e =>
                {
                    targetImage.sprite = Sprite.Create(e, new Rect(0, 0, e.width, e.height), Vector3.one * 0.5f);
                }).Forget();
            }
        }   

        [SerializeField] private Image targetImage;
        [SerializeField] private bool useCache = true;

        private void Start()
        {
            if (targetImage == null) targetImage = GetComponent<Image>();
            if (url.IsNotEmpty()) Url = url;
        }

        public static async UniTask<Texture2D> LoadImageFromUrl(string url, bool useCache = true)
        {
            Texture2D image = null;
            string directoryPath = $"{Application.persistentDataPath}/Cache";
            string path = $"{directoryPath}/{url.GetHashCode()}";
            
            if (useCache && Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            if (useCache && File.Exists(path))
            {
                var bytes = await File.ReadAllBytesAsync(path);
                var texture = new Texture2D(2, 2);
                texture.LoadImage(bytes);
                texture.Apply();
                image = texture;
            }
            else
            {
                using UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);

                uwr.timeout = 5;
                
                await uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    image = texture;

                    if (useCache == false) return image;
                    var bytes = texture.EncodeToPNG();
                    await File.WriteAllBytesAsync(path, bytes);
                }
                else
                {
                    Debug.LogError(uwr.error);
                }
            }

            return image;
        }
    }
}
