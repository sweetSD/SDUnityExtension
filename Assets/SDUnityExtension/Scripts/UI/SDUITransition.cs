using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coffee.UIEffects;

public class SDUITransition : MonoBehaviour
{
    [SerializeField] private UITransitionEffect _transitionEffect;
    public UITransitionEffect TransitionEffect => _transitionEffect;
}
