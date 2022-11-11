using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageModifier
{
    IEnumerable<float> GetAdditiveModifiers();
    IEnumerable<float> GetPercentageModifiers();
}
