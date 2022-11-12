using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : IDamageModifier
{
    [field:SerializeField] public string AnimationName { get; private set; }
    [field:SerializeField] public float baseDamage { get; private set; }
    [field:SerializeField] public float percentageBonusDamage { get; private set; }
    [field:SerializeField] public float TransitionDuration { get; private set; }
    [field:SerializeField] public float ComboAttackTime { get; private set; }
    [field:SerializeField] public int ComboIndex { get; private set; } = -1;

    public IEnumerable<float> GetAdditiveModifiers()
    {
        yield return baseDamage;
    }

    public IEnumerable<float> GetPercentageModifiers()
    {
        yield return percentageBonusDamage;
    }
}
