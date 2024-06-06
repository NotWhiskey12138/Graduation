using System;
using UnityEngine;

namespace Whiskey.Weapons.Components
{
    [Serializable]
    public class AttackTargeter : AttackData
    {
        [field: SerializeField] public Rect Area { get; private set; }
        [field: SerializeField] public LayerMask DamageableLayer { get; private set; }
    }
}