using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")] public float movementVelocity = 10f;

    [Header("Jump State")] public float jumpVelocity = 15f;
    public int amountOfJumps = 2;

    [Header("Wall Jump State")] public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("InAir State")] public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")] public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")] public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")] public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Crouch State")] public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.6f;
}

