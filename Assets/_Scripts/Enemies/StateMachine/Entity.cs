using System.Collections;
using System.Collections.Generic;
using Whiskey.CoreSystem;
using UnityEngine;

public class Entity : MonoBehaviour {
	private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }

	private Movement movement;

	public FiniteStateMachine stateMachine;

	public D_Entity entityData;

	public Animator anim { get; private set; }
	public AnimationToStatemachine atsm { get; private set; }
	public int lastDamageDirection { get; private set; }
	public Core Core { get; private set; }

	[SerializeField]
	private Transform wallCheck;
	[SerializeField]
	private Transform ledgeCheck;
	[SerializeField]
	private Transform playerCheck;
	[SerializeField]
	private Transform groundCheck;

	private float currentHealth;
	private float currentStunResistance;
	private float lastDamageTime;

	private Vector2 velocityWorkspace;

	protected bool isStunned;
	protected bool isDead;

	protected Stats stats;
	protected ParryReceiver parryReceiver;

	public virtual void Awake() {
		Core = GetComponentInChildren<Core>();

		stats = Core.GetCoreComponent<Stats>();
		parryReceiver = Core.GetCoreComponent<ParryReceiver>();

		parryReceiver.OnParried += HandleParry;

		currentHealth = entityData.maxHealth;
		currentStunResistance = entityData.stunResistance;

		anim = GetComponent<Animator>();
		atsm = GetComponent<AnimationToStatemachine>();

		stateMachine = new FiniteStateMachine();
	}

	public virtual void Update() {
		Core.LogicUpdate();
		stateMachine.currentState.LogicUpdate();

		anim.SetFloat("yVelocity", Movement.RB.velocity.y);

		if (Time.time >= lastDamageTime + entityData.stunRecoveryTime) {
			ResetStunResistance();
		}
	}

	protected virtual void HandleParry()
	{
		
	}

	public virtual void FixedUpdate() {
		stateMachine.currentState.PhysicsUpdate();
	}

	public virtual bool CheckPlayerInMinAgroRange() {
		return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
	}

	public virtual bool CheckPlayerInMaxAgroRange() {
		return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
	}

	public virtual bool CheckPlayerInCloseRangeAction() {
		return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
	}

	public virtual void DamageHop(float velocity) {
		velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
		Movement.RB.velocity = velocityWorkspace;
	}

	public virtual void ResetStunResistance() {
		isStunned = false;
		currentStunResistance = entityData.stunResistance;
	}

	public virtual void OnDrawGizmos() {
		if (Core != null) {
			Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
			Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
			Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
		}
	}
}
