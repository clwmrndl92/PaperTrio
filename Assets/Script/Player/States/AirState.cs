using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public AirState(Player player) : base(player)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        ;
    }

    public override void OnUpdateState()
    {
        if( 
            CanJump()
            || CanRun()
            || CanStand()
            )
        {
            return;
        }

    }

    public override void OnFixedUpdateState()
    {
        player.gameObject.transform.Translate(player.input.directionX * player.airVeclocity * Time.fixedDeltaTime * Vector3.right);
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
    private bool CanRun()
    {
        if (player.ground.GetOnGround() 
            && player.input.directionX != 0)
        {
            player.stateMachine.ChangeState(StateName.Run);
            return true;
        }
        return false;
    }

    private bool CanStand()
    {
        if (player.ground.GetOnGround() )
        {
            player.stateMachine.ChangeState(StateName.Stand);
            return true;
        }
        return false;
    }

}
