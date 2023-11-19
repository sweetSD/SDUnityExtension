using System.Collections.Generic;
using SDUnityExtension.Scripts.Object;
using UnityEngine;

namespace SDUnityExtension.Scripts.Logic
{
    public class SceneEvent : DelayedEventHandler
    {
        [SerializeField] private List<DelayedEvent> onAwake;
        [SerializeField] private List<DelayedEvent> onStart;
        [SerializeField] private List<DelayedEvent> onEnable;
        [SerializeField] private List<DelayedEvent> onUpdate;
        [SerializeField] private List<DelayedEvent> onDisable;
        [SerializeField] private List<DelayedEvent> onDestroy;

        private void Awake()
        {
            RegistEvents(onAwake);
        }
    
        private void Start()
        {
            RegistEvents(onStart);
        }

        private void OnEnable()
        {
            RegistEvents(onEnable);
        }

        private void Update()
        {
            RegistEvents(onUpdate);
        }

        private void OnDisable()
        {
            RegistEvents(onDisable);
        }

        private void OnDestroy()
        {
            #if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            #endif
            RegistEvents(onDestroy);
        }
    }
}
