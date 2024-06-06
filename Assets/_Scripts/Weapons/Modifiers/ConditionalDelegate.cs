using Whiskey.Weapons.Components;
using UnityEngine;

namespace Whiskey.Weapons.Modifiers
{ 
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}