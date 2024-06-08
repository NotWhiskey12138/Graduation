using System;
using UnityEngine;

namespace Whiskey.Weapons.Components
{
    [Serializable]
    public class AttackChargeToProjectileSpawner : AttackData
    {
        [field: SerializeField, Range(0f, 360f)] public float AngleVariation { get; private set; }
    }
}