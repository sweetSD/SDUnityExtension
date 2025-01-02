using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SDUnityExtension.Scripts.Pattern
{
    public class SDObjectPool : MonoBehaviour
    {
        public bool Initialized { get; private set; }
        
        [Header("Prefab")]
        [Tooltip("오브젝트 풀링에 사용될 오브젝트입니다.")]
        [SerializeField] private GameObject poolObject;

        [Header("Settings")]
        [Tooltip("기본 생성 오브젝트 수입니다.")]
        [SerializeField] private int initialSize = 4;

        [Tooltip("오브젝트를 갖고 있을 부모 Transform 입니다. (null일 경우 풀 오브젝트 사용)")]
        [SerializeField] private Transform poolRoot;

        [Tooltip("오브젝트가 모두 사용중일 경우 새로 생성할지 여부입니다.")]
        [SerializeField] private bool flexible = true;

        private int PoolObjectID => poolObject.GetInstanceID();
        private HashSet<int> Objects { get; } = new();
        private LinkedList<GameObject> Pool { get; } = new();

       private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            if (poolObject == null)
            {
                throw new NullReferenceException("Pooling object is null.");
            }

            Initialized = true;

            poolRoot ??= transform;

            for (int i = 0; i < initialSize; i++)
            {
                var instance = Instantiate(poolObject, poolRoot);
                instance.SetActive(false);
                Objects.Add(instance.GetInstanceID());
                Pool.AddLast(instance);
            }

            if (pools.ContainsKey(PoolObjectID) == false)
            {
                pools[PoolObjectID] = this;
            }
        }

        private void OnDestroy()
        {
#if !UNITY_EDITOR
            // Prevent null reference when stop game in editor
            foreach (var pooledObject in Pool)
            {
                Destroy(pooledObject);
            }
#endif
            pools.Remove(PoolObjectID);
        }

        public GameObject Get()
        {
            GameObject instance = null;
            
            Initialize();

            if (Pool.Count == 0)
            {
                if (flexible)
                {
                    instance = Instantiate(poolObject, poolRoot);
                    Objects.Add(instance.GetInstanceID());
                    Pool.AddLast(instance);
                }
                else
                {
                    throw new InvalidOperationException("Object pool is empty");
                }
            }
            
            instance = Pool.First.Value;
            Pool.RemoveFirst();
            instance.SetActive(true);

            links[instance.GetInstanceID()] = this;
            return instance;
        }

        public T Get<T>() where T : Component
        {
            return Get().GetComponent<T>();
        }

        public GameObject Get(Transform parent)
        {
            var instance = Get();
            instance.transform.SetParent(parent);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;
            return instance;
        }
        
        public T Get<T>(Transform parent) where T : Component
        {
            return Get(parent).GetComponent<T>();
        }
        
        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            var instance = Get();
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            return instance;
        }
        
        public T Get<T>(Vector3 position, Quaternion rotation) where T : Component
        {
            return Get(position, rotation).GetComponent<T>();
        }

        public void Put(GameObject instance)
        {
            Initialize();

            if (Objects.Contains(instance.GetInstanceID()))
            {
                instance.transform.SetParent(poolRoot);
                instance.gameObject.SetActive(false);
                Pool.AddLast(instance);
                links.Remove(instance.GetInstanceID());
            }
            else
            {
                Debug.LogWarning($"{instance.name}은 해당 오브젝트 풀에 등록되어 있지 않습니다.");
            }
        }

        public void Put(Component component)
        {
            Put(component.gameObject);
        }

        #region Static
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            pools.Clear();
            links.Clear();
        }

        // Key: Prefab GameObject의 Instance ID
        // Value: 해당 GameObject를 prefab으로 만든 Object Pool
        private static readonly Dictionary<int, SDObjectPool> pools = new();
        // Key: Instance GameObject의 Instance ID
        // Value: 해당 GameObject를 소유중인 Object Pool
        private static readonly Dictionary<int, SDObjectPool> links = new();

        public static SDObjectPool GetPool(GameObject obj)
        {
            if (pools.TryGetValue(obj.GetInstanceID(), out var pool))
            {
                return pool;
            }
            
            var poolObject = new GameObject($"[{obj.name}] Object Pool");
            pool = poolObject.AddComponent<SDObjectPool>();
            pool.poolObject = obj;
            pool.Initialize();

            return pools[obj.GetInstanceID()];
        }
        
        public static GameObject Spawn(GameObject obj)
        {
            return GetPool(obj).Get();
        }

        public static GameObject Spawn(GameObject obj, Transform parent)
        {
            return GetPool(obj).Get(parent);
        }
        
        public static GameObject Spawn(GameObject obj, Vector3 position, Quaternion rotation)
        {
            return GetPool(obj).Get(position, rotation);
        }
        
        public static T Spawn<T>(GameObject obj) where T : Component
        {
            return GetPool(obj).Get<T>();
        }

        public static T Spawn<T>(GameObject obj, Transform parent) where T : Component
        {
            return GetPool(obj).Get<T>(parent);
        }
        
        public static T Spawn<T>(GameObject obj, Vector3 position, Quaternion rotation) where T : Component
        {
            return GetPool(obj).Get<T>(position, rotation);
        }
        
        public static T Spawn<T>(T obj) where T : Component
        {
            return Spawn<T>(obj.gameObject);
        }
        
        public static T Spawn<T>(T obj, Transform parent) where T : Component
        {
            return Spawn<T>(obj.gameObject, parent);
        }
        
        public static T Spawn<T>(T obj, Vector3 position, Quaternion rotation) where T : Component
        {
            return Spawn<T>(obj.gameObject, position, rotation);
        }

        public static void Despawn(GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            if (links.TryGetValue(obj.GetInstanceID(), out var pool))
            {
                pool.Put(obj);
            }
            else
            {
                Destroy(obj);
            }
        }
        
        public static void Despawn(Component obj)
        {
            Despawn(obj.gameObject);
        }

        public static async UniTask Despawn(GameObject obj, float delay)
        {
            if (delay > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            }
            Despawn(obj);
        }
        
        public static async UniTask Despawn(Component obj, float delay)
        {
            if (delay > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            }
            Despawn(obj);
        }

        #endregion
    }
}
