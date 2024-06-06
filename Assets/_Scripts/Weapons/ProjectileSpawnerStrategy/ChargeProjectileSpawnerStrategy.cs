using System;
using Whiskey.ObjectPoolSystem;
using Whiskey.ProjectileSystem;
using Whiskey.Weapons.Components;
using UnityEngine;


namespace Whiskey.Weapons
{
    /*
    *这个类代表了一个自定义策略，用于生成投射物。Out ProjectileSpawner武器组件在想要刷出时使用策略
    *炮弹。默认策略位于ProjectileSpawnerStrategy类中，该类继承了该类。
    ＊
    *这个策略通过考虑一些其他参数来修改我们如何生成炮弹，在这种情况下，是装药量和角度变化。而不是生成一个
    当调用spawn抛射函数时，它会产生与我们有电荷一样多的抛射。它还改变了每个炮弹的生成角度
    *这样平均刷出方向就是传入的方向。
    */
    [Serializable]
    public class ChargeProjectileSpawnerStrategy : ProjectileSpawnerStrategy
    {
        // These values is set from some external source. E.G: ChargeToProjectileSpawner weapon component
        public float AngleVariation;
        public int ChargeAmount;

        // A working variable that holds the current direction we want to spawn the next projectile in the loop in
        private Vector2 currentDirection;

        public override void ExecuteSpawnStrategy(ProjectileSpawnInfo projectileSpawnInfo, Vector3 spawnerPos,
            int facingDirection,
            ObjectPools objectPools, Action<Projectile> OnSpawnProjectile)
        {
            // If there are no charges, we don't spawn any projectiles.
            if (ChargeAmount <= 0)
                return;

            // If there is only one charge, the direction to spawn the projectile in is the same as the direction that has been passed in.
            if (ChargeAmount == 1)
            {
                currentDirection = projectileSpawnInfo.Direction;
            }
            else
            {
                /*
                 * If there are more than one charge, we need to rotate the current direction by half of the total angle variation.
                 * Total angle variation = (ChargeAmount - 1) * AngleVariation
                 * This creates the initialRotationQuaternion. By multiplying this by the passed in spawn direction, we get a new direction that
                 * has been rotated anti-clockwise by that amount.
                 */
                var initialRotationQuaternion = Quaternion.Euler(0f, 0f, -((ChargeAmount - 1f) * AngleVariation / 2f));

                // Rotate the vector to set our first spawn direction
                currentDirection = initialRotationQuaternion * projectileSpawnInfo.Direction;
            }

            // The quaternion that we will use to rotate the spawn direction by our angle variation for every projectile we spawn
            var rotationQuaternion = Quaternion.Euler(0f, 0f, AngleVariation);

            for (var i = 0; i < ChargeAmount; i++)
            {
                // Projectile spawn methods. See ProjectileSpawnerStrategy class for more details
                SpawnProjectile(projectileSpawnInfo, currentDirection, spawnerPos, facingDirection, objectPools,
                    OnSpawnProjectile);

                // Rotate the spawn direction for next projectile.
                currentDirection = rotationQuaternion * currentDirection;
            }
        }
    }
}