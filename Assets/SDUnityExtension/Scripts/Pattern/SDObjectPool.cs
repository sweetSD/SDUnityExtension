﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by sweetSD.
/// 
/// 오브젝트 풀입니다.
/// 원하시는 오브젝트로 상속하여 사용할 수 있습니다.
/// </summary>
public class SDObjectPool : MonoBehaviour
{
    static Dictionary<string, SDObjectPool> m_Pools = new Dictionary<string, SDObjectPool>();

    [Header("Pool Info")]
    [Tooltip("오브젝트 풀 이름입니다.")]
    [SerializeField] protected string _poolName = "";

    [Header("Pool Objects")]
    [Tooltip("오브젝트 풀링에 사용될 오브젝트들입니다.")]
    [SerializeField] protected List<GameObject> _objectPool = null;
    public List<GameObject> ObjectPool => _objectPool;

    [Header("Pool Object")]
    [Tooltip("오브젝트 풀링에 사용될 오브젝트입니다.")]
    [SerializeField] protected GameObject _poolObject;

    [Header("Pool Object Count")]
    [Tooltip("기본 생성 오브젝트 수입니다.")]
    [SerializeField] protected int _initialSize = 32;

    [Header("Pool Root")]
    [Tooltip("오브젝트를 갖고 있을 부모 Transform 입니다.")]
    [SerializeField] protected Transform _poolRoot;

    [Header("Flexible")]
    [Tooltip("오브젝트가 모두 사용중일 경우 새로 생성할지 여부입니다.")]
    [SerializeField] protected bool _flexible = true;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        DestroyPool();
    }

    /// <summary>
    /// 오브젝트 풀을 초기화합니다.
    /// </summary>
    public virtual void Initialize()
    {
        if (_poolRoot == null)
            _poolRoot = GetComponent<Transform>();

        _objectPool = new List<GameObject>();

        for (int i = 0; i < _initialSize; i++)
        {
            _objectPool.Add(Instantiate(_poolObject, Vector3.zero, Quaternion.identity, _poolRoot));
            _objectPool[i].SetActive(false);
        }

        var poolName = _poolName.IsNotEmpty() ? _poolName : _poolObject.name;
        if (!m_Pools.ContainsKey(poolName))
        {
            m_Pools.Add(poolName, this);
        }
    }

    /// <summary>
    /// 오브젝트 풀에서 오브젝트 하나를 가져옵니다.
    /// </summary>
    /// <param name="posVec3">위치</param>
    /// <param name="rotVec3">회전</param>
    /// <param name="scaleVec3">스케일</param>
    /// <param name="doNotActive">오브젝트를 활성화 하지 않고 반환할지 여부</param>
    /// <returns></returns>
    public virtual T ActiveObject<T>(Vector3 posVec3, Vector3 rotVec3, Vector3? scaleVec3 = null, bool doNotActive = false) where T : Component
    {
        return ActiveObject(posVec3, Quaternion.Euler(rotVec3), scaleVec3 ?? Vector3.one, doNotActive).GetComponent<T>();
    }

    public virtual T ActiveObject<T>(Vector3 posVec3, Quaternion rot, Vector3? scaleVec3 = null, bool doNotActive = false) where T : Component
    {
        return ActiveObject(posVec3, rot, scaleVec3 ?? Vector3.one, doNotActive).GetComponent<T>();
    }

    public virtual GameObject ActiveObject(Vector3 posVec3, Quaternion rot, Vector3? sclVec3 = null, bool doNotActive = false)
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if (!_objectPool[i].activeSelf)
            {
                var objTransform = _objectPool[i].transform;
                objTransform.localPosition = posVec3;
                objTransform.rotation = rot;
                objTransform.localScale = sclVec3 ?? Vector3.one;

                objTransform.gameObject.SetActive(!doNotActive);

                // 한번 사용한 오브젝트를 리스트 제일 하위로 이동
                _objectPool.RemoveAt(i);
                _objectPool.Add(objTransform.gameObject);

                return objTransform.gameObject;
            }
        }
        if (_flexible)
        {
            var objInst = Instantiate(_poolObject, posVec3, rot, _poolRoot);
            objInst.SetActive(!doNotActive);
            _objectPool.Add(objInst);
            return objInst;
        }
        else
            return null;
    }

    /// <summary>
    /// 오브젝트 풀을 비웁니다.
    /// </summary>
    public void DestroyPool()
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            Destroy(_objectPool[i]);
        }
        _objectPool.Clear();

        var poolName = _poolName.IsNotEmpty() ? _poolName : _poolObject.name;
        if (m_Pools.ContainsKey(poolName))
        {
            m_Pools.Remove(poolName);
        }
    }

    #region STATIC_FUNCTIONS
    public static SDObjectPool GetPool(string name)
    {
        SDObjectPool p;
        m_Pools.TryGetValue(name, out p);
        return p;
    }
    #endregion
}
