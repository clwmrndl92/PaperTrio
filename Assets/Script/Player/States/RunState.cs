using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public RunState(Player player) : base(player)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        player.rigid.velocity = Vector2.zero;
        player.isJumping = false;
    }

    public override void OnUpdateState()
    {
        if (CanJump()
            || CanStand()
            || CanAir())
        {
            return;
        }
    }


    public override void OnFixedUpdateState()
    {
        player.rigid.velocity = player.input.directionX * player.runVeclocity * Vector2.right;
    }
    public override void OnExitState()
    {
    }

    private bool CanJump()
    {
        if((player.input.buttonsDown & InputData.JUMPBUTTON) == InputData.JUMPBUTTON && !player.isJumping)
        {
            player.stateMachine.ChangeState(StateName.Jump);
            return true;
        }
        return false;
    }

    private bool CanStand()
    {
        if (player.ground.GetOnGround()
            && player.input.directionX == 0)
        {
            player.stateMachine.ChangeState(StateName.Stand);
            return true;
        }
        return false;
    }

    private bool CanAir()
    {
        if (!player.ground.GetOnGround())
        {
            player.stateMachine.ChangeState(StateName.Air);
            return true;
        }
        return false;
    }

}