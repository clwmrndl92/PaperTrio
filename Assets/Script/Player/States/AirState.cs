using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public AirState(PlayerController controller) : base(controller)
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
        Controller.rigidData.runDirection = Controller.input.directionX;
        Controller.rigidData.runVelocity = Controller.rigidData.airMoving 
            * Controller.rigidData.blockSize 
            * Controller.rigidData.runDirection 
            * Controller.rigidData.airMoving 
            * Vector2.right;
    }
    public override void OnExitState()
    {
        Controller.ChangeBodyColor(Color.white);
    }

    private bool CanJump()
    {
        //if ((Controller.input.buttonsDown & InputData.JUMPBUTTON) == InputData.JUMPBUTTON
        //    && !Controller.rigidData.isAirJumping)
        //{
        //    Controller.player.stateMachine.ChangeState(StateName.Jump);
        //    return true;
        //}
        return false;
    }


    private bool CanRun()
    {
        if (Controller.ground.GetOnGround() 
            && Controller.input.directionX != 0)
        {
            Controller.player.stateMachine.ChangeState(StateName.Run);
            return true;
        }
        return false;
    }

    private bool CanStand()
    {
        if (Controller.ground.GetOnGround() 
            && Controller.rigid.velocity.y <= 0f)
        {
            Controller.player.stateMachine.ChangeState(StateName.Stand);
            return true;
        }
        return false;
    }

}
