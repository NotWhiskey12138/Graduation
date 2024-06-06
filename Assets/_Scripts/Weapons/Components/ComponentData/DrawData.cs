namespace Whiskey.Weapons.Components
{
    public class DrawData : ComponentData<AttackDraw>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Draw);
        }
    }
}