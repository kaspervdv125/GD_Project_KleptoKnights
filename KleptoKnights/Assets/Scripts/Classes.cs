using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public float MovementSpeed
    {
        get
        {
            switch (Class)
            {
                case PlayerClass.Knight:
                    return 7.5f;
                case PlayerClass.Rogue:
                    return 10.0f;
                case PlayerClass.Builder:
                    return 5.0f;
                default:
                    return 7.5f;
            }
        }
    }

    public int WeightLimit
    {
        get
        {
            switch (Class)
            {
                case PlayerClass.Knight:
                    return 100;
                case PlayerClass.Rogue:
                    return 60;
                case PlayerClass.Builder:
                    return 140;
                default:
                    return 100;
            }
        }
    }


    public PlayerClass Class;

    public enum PlayerClass
    {
        Knight,
        Rogue,
        Builder
    }
}