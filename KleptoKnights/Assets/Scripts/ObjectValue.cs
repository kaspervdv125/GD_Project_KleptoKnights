using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValue : MonoBehaviour
{
    [Range(1, 50)]
    public int Value;

    public static float ValuePickupTimeMultiplier = 0.1f;
}
