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

    [SerializeField] private GameObject _knightHead;
    [SerializeField] private GameObject _rogueHead;
    [SerializeField] private GameObject _builderHead;

    private GameObject _head;

    private void Start()
    {
        ChangeClass(PlayerClass.Knight);
    }

    public void ChangeClass(PlayerClass targetClass)
    {
        Class = targetClass;

        Destroy(_head);

        switch (targetClass)
        {
            case PlayerClass.Knight:
                _head = Instantiate(_knightHead);
                break;
            case PlayerClass.Rogue:
                _head = Instantiate(_rogueHead);
                break;
            case PlayerClass.Builder:
                _head = Instantiate(_builderHead);
                break;
        }

        _head.transform.parent = transform;
        _head.transform.localPosition = Vector3.zero;
        _head.transform.localRotation = Quaternion.identity;
    }

    public enum PlayerClass
    {
        Knight,
        Rogue,
        Builder
    }
}