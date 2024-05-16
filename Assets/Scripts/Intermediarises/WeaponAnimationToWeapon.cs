using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }
    
    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    private void AnimationTurnOffTrigger()
    {
        weapon.AnimationTurnOffFlipTrigger();
    }

    private void AnimationTurnOnTrigger()
    {
        weapon.AnimationTurnOnFlipTrigger();
    }

    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
