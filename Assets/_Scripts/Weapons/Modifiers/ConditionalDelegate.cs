using UnityEngine;
using Whiskey.Weapons.Components;

namespace Whiskey.Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}