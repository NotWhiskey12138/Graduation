using System;
using System.Collections;
using System.Collections.Generic;
using Whiskey.CoreSystem;
using Whiskey.FSM;
using Whiskey.Weapons;
using UnityEngine;
using UnityEngine.Rendering;
using Whiskey.CoreSystem.StatsSystem;
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
    public PlayerWallJumpState WallJumpState { get;private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
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
    public BoxCollider2D MovementCollider { get; private set; }
    
    public PlayerStatBar playerStatBar { get; private set; }
    
    public Stats Stats { get; private set; }
    
    public InteractableDetector InteractableDetector { get; private set; }
    
    #endregion
    
    #region Other Variables
    
    private Vector2 workspace;

    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;

    private bool interactiveInput; //使用输入检测

    private IInteractable targetItem; //当前获取的可交互物体

    [SerializeField] private Stat stat; //角色当前状态

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        
        primaryWeapon.SetCore(Core);
        secondaryWeapon.SetCore(Core);
        
        StateMachine = new PlayerStateMachine();
        
        Stats = Core.GetCoreComponent<Stats>();
        InteractableDetector = Core.GetCoreComponent<InteractableDetector>();

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
        MovementCollider = GetComponent<BoxCollider2D>();
        playerStatBar = GetComponentInChildren<PlayerStatBar>();
        
        Stats.Poise.OnCurrentValueZero += HandlePoiseCurrentValueZero;

        StateMachine.Initialize(IdleState);
        
    }
    
    private void HandlePoiseCurrentValueZero()
    {
        StateMachine.ChangeState(PlayerStunState);
    }

    private void OnEnable()
    {
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    private void OnDisable()
    {
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        
        interactiveInput = InputHandler.InteractiveInput;
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        
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

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") && interactiveInput) 
        {
            targetItem = other.GetComponent<IInteractable>();
            targetItem.Interact();
            interactiveInput = false;
        }
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
                data.floatSavedData[GetDataID().ID + "health"] = stat.GetCurrentHealth();
            }
            else
            {
                data.characterPosDict.Add(GetDataID().ID, transform.position);
                data.floatSavedData.Add(GetDataID().ID + "health", stat.GetCurrentHealth());
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
                stat.SetCurrentHealth(data.floatSavedData[GetDataID().ID + "health"]);

                //通知UI更新
                //playerStatBar.OnHealthChange(data.floatSavedData[GetDataID().ID + "health"]);
            }
        }
    }

    #endregion
   
}