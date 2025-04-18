using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Soldier : Entity
{
    public E_Soldier_IdleState idleState { get; private set; }
    public E_Soldier_MoveState moveState { get; private set; }
    public E_Soldier_PlayerDetectedState playerDetectedState { get; private set; }
    public E_Soldier_ChargeState chargeState { get; private set; }
    public E_Soldier_LookForPlayerState lookForPlayerState { get; private set; }
    public E_Soldier_MeleeAttackState meleeAttackState { get; private set; }
    public E_Soldier_StunState stunState { get; private set; }
    public E_Soldier_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        moveState = new E_Soldier_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E_Soldier_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E_Soldier_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E_Soldier_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E_Soldier_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E_Soldier_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new E_Soldier_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E_Soldier_DeadState(this, stateMachine, "dead", deadStateData, this);

       
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);        
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
