using System;
using Cysharp.Threading.Tasks;
using SDUnityExtension.Scripts.Pattern;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPoolTest : MonoBehaviour
{
    [AssetsOnly, SerializeField] private GameObject object1;
    [SerializeField] private int objectSpawnCount = 1000;
    [SerializeField] private SDObjectPool pool1;

    private async UniTaskVoid Start()
    {
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        
        for (int i = 0; i < objectSpawnCount; i++)
        {
            SDObjectPool.Despawn(SDObjectPool.Spawn(object1, Random.insideUnitSphere * 5f, Quaternion.identity), 5f);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(6f));
        
        for (int i = 0; i < objectSpawnCount; i++)
        {
            pool1.Get(Random.insideUnitSphere * 5f, Quaternion.identity);
        }
    }
}
