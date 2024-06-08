﻿using UnityEngine;
using Whiskey.Combat.Damage;
using static Whiskey.Utilities.CombatDamageUtilities;

namespace Whiskey.Weapons.Components
{
    public class DamageOnParry : WeaponComponent<DamageOnParryData, AttackDamage>
    {
        private Parry parry;
        
        private void HandleParry(GameObject parriedGameObject)
        {
            TryDamage(
                parriedGameObject,
                new DamageData(currentAttackData.Amount, Core.Root),
                out _
            );
        }

        protected override void Start()
        {
            base.Start();
            
            parry = GetComponent<Parry>();

            parry.OnParry += HandleParry;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            parry.OnParry -= HandleParry;
        }
    }
}