using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    public Vector3 ReceiveKnockback(Vector3 direction, float distance)
    {
        return direction * distance;
    }
}
