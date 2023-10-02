using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    // Animation
    //public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    //private int hashMoveAnimation;


    public JumpState(Player player) : base(player)
    {
        //hashMoveAnimation = Animator.StringToHash("Velocity");
    }

    protected float GetAnimationSyncWithMovement(float changedMoveSpeed)
    {
        return 0.0f;
    }

    public override void OnEnterState()
    {
        if (player.rigid.velocity.y <= 0)
        {  
            player.rigid.velocity = Vector2.zero;
        }

        if (!player.isJumping)
        {
            player.isJumping = true;
        }
        player.rigid.AddForce(player.jumpPower * Vector2.up, ForceMode2D.Impulse);
    }

    public override void OnUpdateState()
    {
        if(CanStand() 
            || CanAir()
            )
        {
            return;
        }
    }

    public override void OnFixedUpdateState()
    {
        player.rigid.velocity = player.rigid.velocity.y * Vector2.up + player.input.directionX * player.runVeclocity * Vector2.right;
    }
    public override void OnExitState()
    {
        player.rigid.velocity = Vector2.zero;
    }

    private bool CanAir()
    {
        if (player.rigid.velocity.y <= 0)
        {
            player.stateMachine.ChangeState(StateName.Air);
            return true;
        }
        return false;
    }
    // private bool CanJump()
    // {
    //     if ((player.input.buttonsDown & InputData.JUMPBUTTON) == InputData.JUMPBUTTON)
    //     {
    //         player.stateMachine.ChangeState(StateName.Jump);
    //         return true;
    //     }
    //     return false;
    // }


    private bool CanStand()
    {
        if (player.ground.GetOnGround() && player.rigid.velocity.y <= 0f)
        {
            player.stateMachine.ChangeState(StateName.Stand);
            return true;
        }
        return false;
    }
    
}
