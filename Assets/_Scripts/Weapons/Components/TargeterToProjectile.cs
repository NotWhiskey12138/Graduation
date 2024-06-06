using Whiskey.ProjectileSystem;
using Whiskey.ProjectileSystem.DataPackages;

namespace Whiskey.Weapons.Components
{
    /*
    *当炮弹生成时，该组件从Targeter武器组件获得所有目标的列表并将其传递出去到targetsDataPackage中的射弹。
     */
    public class TargeterToProjectile : WeaponComponent
    {
        private ProjectileSpawner projectileSpawner;
        private Targeter targeter;

        private readonly TargetsDataPackage targetsDataPackage = new TargetsDataPackage();
        
        private void HandleSpawnProjectile(Projectile projectile)
        {
            targetsDataPackage.targets = targeter.GetTargets();

            projectile.SendDataPackage(targetsDataPackage);
        }

        #region Plumbing

        protected override void Start()
        {
            base.Start();

            projectileSpawner = GetComponent<ProjectileSpawner>();
            targeter = GetComponent<Targeter>();

            projectileSpawner.OnSpawnProjectile += HandleSpawnProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            projectileSpawner.OnSpawnProjectile -= HandleSpawnProjectile;
        }

        #endregion
    }
}