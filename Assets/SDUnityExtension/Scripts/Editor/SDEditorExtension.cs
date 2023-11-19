using UnityEditor;
using UnityEngine;

namespace SDUnityExtension.Scripts.Editor
{
    /// <summary>
    /// Created by sweetSD. (Editor Extension)
    /// 
    /// 유니티 에디터 내부에서 사용할 수 있는 유틸리티 함수들이 있는 클래스입니다.
    /// 
    /// </summary>
    public class SDEditorExtension : MonoBehaviour
    {
        [MenuItem("Tools/SDUnityExtension/Create Manager Prefab")]
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
