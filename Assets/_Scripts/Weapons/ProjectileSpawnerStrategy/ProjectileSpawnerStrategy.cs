using System;
using Whiskey.ObjectPoolSystem;
using Whiskey.ProjectileSystem;
using Whiskey.Weapons.Components;
using UnityEngine;

namespace Whiskey.Weapons
{
    /*
    *这是基本的ProjectileSpawnerStrategy类，或者默认的刷出方法。它只是按要求生成一个弹丸，不做任何其他事情。
    ＊
    策略是做某事的封装算法。通过像这样封装它，这意味着我们可以在运行时交换我们的逻辑。我们需要一个刷出策略
    *因为我们希望我们的冲锋武器组件能够调整每次攻击产生的射弹数量。
    ＊
    */
    public class ProjectileSpawnerStrategy : IProjectileSpawnerStrategy
    {
         // Working variables
        private Vector2 spawnPos;
        private Vector2 spawnDir;
        private Projectile currentProjectile;

        // The function used to initiate the strategy
        public virtual void ExecuteSpawnStrategy
        (
            ProjectileSpawnInfo projectileSpawnInfo,
            Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools,
            Action<Projectile> OnSpawnProjectile
        )
        {
            // Simply spawns one projectile based on the passed in parameters
            SpawnProjectile(projectileSpawnInfo, projectileSpawnInfo.Direction, spawnerPos, facingDirection,
                objectPools, OnSpawnProjectile);
        }

        // Spawn a projectile
        protected virtual void SpawnProjectile(ProjectileSpawnInfo projectileSpawnInfo, Vector2 spawnDirection,
            Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            SetSpawnPosition(spawnerPos, projectileSpawnInfo.Offset, facingDirection);
            SetSpawnDirection(spawnDirection, facingDirection);
            GetProjectileAndSetPositionAndRotation(objectPools, projectileSpawnInfo.ProjectilePrefab);
            InitializeProjectile(projectileSpawnInfo, OnSpawnProjectile);
        }

        protected virtual void GetProjectileAndSetPositionAndRotation(ObjectPools objectPools, Projectile prefab)
        {
            // Get projectile from pool
            currentProjectile = objectPools.GetObject(prefab);

            // Set position, rotation, and other related info
            currentProjectile.transform.position = spawnPos;

            var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
            currentProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        protected virtual void InitializeProjectile(ProjectileSpawnInfo projectileSpawnInfo,
            Action<Projectile> OnSpawnProjectile)
        {
            // Reset projectile from pool
            currentProjectile.Reset();

            // Send through new data packages
            currentProjectile.SendDataPackage(projectileSpawnInfo.DamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.KnockBackData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.PoiseDamageData);
            currentProjectile.SendDataPackage(projectileSpawnInfo.SpriteDataPackage);

            // Broadcast new projectile has been spawned so other components can  pass through data packages
            OnSpawnProjectile?.Invoke(currentProjectile);

            // Initialize the projectile
            currentProjectile.Init();
        }

        protected virtual void SetSpawnDirection(Vector2 direction, int facingDirection)
        {
            // Change spawn direction based on FacingDirection
            spawnDir.Set(direction.x * facingDirection,
                direction.y);
        }

        protected virtual void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
        {
            // Add offset to player position, accounting for FacingDirection
            spawnPos = referencePosition;
            spawnPos.Set(
                spawnPos.x + offset.x * facingDirection,
                spawnPos.y + offset.y
            );
        }
    }
}