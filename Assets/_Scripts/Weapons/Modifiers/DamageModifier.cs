using System;
using Whiskey.ModifierSystem;
using Whiskey.Utilities;
using Whiskey.Weapons.Components;
using UnityEngine;
using DamageData = Whiskey.Combat.Damage.DamageData;
using Movement = Whiskey.CoreSystem.Movement;

namespace Whiskey.Weapons.Modifiers
{
    /*
    *Block武器组件使用的修改器，通过修改伤害量来阻止传入的伤害。     
    */
    public class DamageModifier : Modifier<DamageData>
    {
        // Event that fires off if the block was actually successful
        public event Action<GameObject> OnModified;

        // The function that we call to determine if a block was successful. 
        private readonly ConditionalDelegate isBlocked;

        public DamageModifier(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        /*
         * The meat and potatoes. Damage data is passed in when player gets damaged (before damage is applied). This modifier then
         * checks the angle of the attacker to the player and compares that to the block data angles. If block is successful, damage amount is modified
         * based on the DamageAbsorption field. If not successful, data is not modified.
         */
        public override DamageData ModifyValue(DamageData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.DamageAbsorption));
                OnModified?.Invoke(value.Source);
            }

            return value;
        }
    }
}