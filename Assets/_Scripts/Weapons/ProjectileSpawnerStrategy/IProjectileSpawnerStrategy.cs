using System;
using Whiskey.ObjectPoolSystem;
using Whiskey.ProjectileSystem;
using Whiskey.Weapons.Components;
using UnityEngine;

namespace Whiskey.Weapons
{
    /*
     * ProjectileSpawnerStrategy接口。我们有一个函数，它接受了ProjectileSpawnInfo、产卵器的位置、产卵器的facingDirection、从中获取投射物的对象池，以及在产生投射物时调用的动作。
     */
    
    public interface IProjectileSpawnerStrategy
    {
        void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos, int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile);
    }
}