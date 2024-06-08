﻿namespace Whiskey.Weapons.Components
{
    public class TargeterData : ComponentData<AttackTargeter>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Targeter);
        }
    }
}