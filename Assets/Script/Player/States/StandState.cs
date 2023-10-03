using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public StandState(Player player) : base(player)
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
        if(CanJump() 
            || CanRun() 
            || CanAir())
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
    }

    public override void OnExitState()
    {
    }

    private bool CanJump()
    {
        if((player.input.buttonsDown & InputData.JUMPBUTTON) == InputData.JUMPBUTTON)
        {
            player.stateMachine.ChangeState(StateName.Jump);
            return true;
        }
        return false;
    }

    private bool CanRun()
    {
        if (player.input.directionX != 0)
        {
            player.stateMachine.ChangeState(StateName.Run);
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
