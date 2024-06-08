using Whiskey.Combat.KnockBack;
using Whiskey.ModifierSystem;

namespace Whiskey.Weapons.Modifiers
{
    public class BlockKnockBackModifier : Modifier<KnockBackData>
    {
        private readonly ConditionalDelegate isBlocked;

        public BlockKnockBackModifier(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override KnockBackData ModifyValue(KnockBackData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.Strength *= (1 - blockDirectionInformation.KnockBackAbsorption);
            }

            return value;
        }
    }
}