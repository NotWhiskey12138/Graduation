﻿using System;
using System.Collections;
using System.Collections.Generic;
using Whiskey.CoreSystem;
using Whiskey.FSM;
using Whiskey.Weapons;
using UnityEngine;
using UnityEngine.Rendering;
using Whiskey.Interaction;

public class Player : MonoBehaviour,ISaveable
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    //public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    public PlayerStunState PlayerStunState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    public Stats Stats { get; private set; }
    
    public InteractableDetector InteractableDetector { get; private set; }
    
    [SerializeField] private PlayerStatBar statBar;
    [SerializeField] private PauseMenu pauseMenu;
    
    #endregion

    #region Other Variables         

    private Vector2 workspace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        
        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);

        Stats = Core.GetCoreComponent<Stats>();
        InteractableDetector = Core.GetCoreComponent<InteractableDetector>();
        
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        //DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", secondaryWeapon, CombatInputs.secondary);
        PlayerStunState = new PlayerStunState(this, StateMachine, playerData, "stun");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();

        InputHandler.OnInteractInputChanged += InteractableDetector.TryInteract;
        
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();

        Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;
        
        StateMachine.Initialize(IdleState);
    }
    
    private void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
        Stats.Health.OnCurrentValueZero += Dead;
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
        Stats.Health.OnCurrentValueZero -= Dead;

    }

    private void HandlePoiseCurrentValueZero()
    {
        StateMachine.ChangeState(PlayerStunState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        
        statBar.OnHealthChange(Stats.Health.CurrentValue / Stats.Health.MaxValue);

        
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Stats.Poise.OnCurrentValueZero -= HandlePoiseCurrentValueZero;
    }

    #endregion

    #region Other Functions

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }   

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimtionFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    private void Dead()
    {
        pauseMenu.OpenDeadMenu();
    }
    
    #endregion
    
    #region SaveFunction

    public DataDefinition GetDataID()
    {
        return GetComponent<DataDefinition>();
    }

    public void GetSaveData(Data data)
    {
        DataDefinition dataDefinition = GetDataID();
        if (dataDefinition != null)
        {
            if (data.characterPosDict.ContainsKey(GetDataID().ID))
            {
                data.characterPosDict[GetDataID().ID] = transform.position;
                data.floatSavedData[GetDataID().ID + "health"] = Stats.Health.CurrentValue;
            }
            else
            {
                data.characterPosDict.Add(GetDataID().ID, transform.position);
                data.floatSavedData.Add(GetDataID().ID + "health", Stats.Health.CurrentValue);
            }
        }
    }

    public void LoadData(Data data)
    {
        DataDefinition dataDefinition = GetDataID();
        if (dataDefinition != null)
        {
            if (data.characterPosDict.ContainsKey(GetDataID().ID))
            {
                transform.position = data.characterPosDict[GetDataID().ID];
                Stats.Health.SetValue(data.floatSavedData[GetDataID().ID + "health"]);

                //通知UI更新
                //playerStatBar.OnHealthChange(data.floatSavedData[GetDataID().ID + "health"]);
            }
        }
    }

    #endregion
}
