using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Classes : MonoBehaviour
{
    public enum PlayerClass
    {
        Knight,
        Rogue,
        Builder
    }

    public enum ItemType
    {
        Sword,
        Hammer,
        Dagger
    }

    public class Item
    {
        public ItemType Type { get; }

        public Item(ItemType type)
        {
            Type = type;
        }
    }

    public class Player
    {
        public PlayerClass Class { get; private set; }

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
                        return 50;
                    case PlayerClass.Rogue:
                        return 35;
                    case PlayerClass.Builder:
                        return 65;
                    default:
                        return 50;
                }
            }
        }

        public void PickupItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Sword:
                    Class = PlayerClass.Knight;
                    break;
                case ItemType.Hammer:
                    Class = PlayerClass.Builder;
                    break;
                case ItemType.Dagger:
                    Class = PlayerClass.Rogue;
                    break;
                default:
                    throw new ArgumentException("Unknown item type");
            }
        }
    }
}   
