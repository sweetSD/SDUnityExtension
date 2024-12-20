using UnityEditor;
using UnityEngine;

namespace SDUnityExtension.Scripts.Editor
{
    public class SDEditorExtension : MonoBehaviour
    {
        [MenuItem("SDUnityExtension/Create Manager Prefab")]
        public static void CreateManagerPrefab()
        {
            var mngObject = Resources.Load<GameObject>("Prefab/SDManager");
            if (mngObject)
            {
                var mngInst = PrefabUtility.InstantiatePrefab(mngObject);
                Selection.activeObject = mngInst;
                mngInst.name = "<-- SD Manager -->";
            }
            else
            {
                Debug.LogError("매니저 프리팹이 존재하지 않습니다.");
            }
        }
    }
}
