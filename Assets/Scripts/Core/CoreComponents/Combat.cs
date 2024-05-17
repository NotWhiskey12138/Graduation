using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
        
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;
    
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;

    [SerializeField] private float maxKnockbackTime = 0.2f;
    
    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        Stats?.DecreaseHealth(amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && (Movement.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground) ||
            Time.time >= knockbackStartTime + maxKnockbackTime) 
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}