using System;
using Whiskey.Combat.Damage;
using Whiskey.ProjectileSystem.Components;
using UnityEngine;

namespace Whiskey.ProjectileSystem
{
    /*
     * This MonoBehaviour is simply used to print the damage amount received in the ProjectileTestScene
     */
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(DamageData data)
        {
            print($"{gameObject.name} Damaged: {data.Amount}");
        }
    }
}