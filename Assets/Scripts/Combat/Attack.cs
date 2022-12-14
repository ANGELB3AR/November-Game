using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    [field:SerializeField] public string AnimationName { get; private set; }
    [field:SerializeField] public float TransitionDuration { get; private set; }
    [field:SerializeField] public float ComboAttackTime { get; private set; }
    [field:SerializeField] public int ComboIndex { get; private set; } = -1;
    [field: SerializeField] public AudioClip SoundEffect { get; private set; } = null;
}
