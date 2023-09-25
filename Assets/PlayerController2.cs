using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController2 : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    [SerializeField]
    float gravity = -10f;

    private CapsuleCollider2D _capsuleCollider;

    private Vector2 _velocity;

    private PlayerInputs _input;

    /// <summary>
    /// Set to true when the character intersects a collider beneath
    /// them in the previous frame.
    /// </summary>
    private bool _grounded;

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _input = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        // Use GetAxisRaw to ensure our input is either 0, 1 or -1.
        float moveInput = _input.move.x;

        if (_grounded)
        {
            _velocity.y = 0;

            if (_input.jump)
            {
                // Calculate the velocity required to achieve the target jump height.
                _velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravity));
                _input.jump = false;
            }
        }
        else
        {
            _input.jump = false;
        }

        float acceleration = _grounded ? walkAcceleration : airAcceleration;
        float deceleration = _grounded ? groundDeceleration : 0;

        if (moveInput != 0)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, deceleration * Time.deltaTime);
        }

        _velocity.y += gravity * Time.deltaTime;

        transform.Translate(_velocity * Time.deltaTime);

        _grounded = false;

        // Retrieve all colliders we have intersected after velocity has been applied.
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _capsuleCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // Ignore our own collider.
            if (hit == _capsuleCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(_capsuleCollider);

            // Ensure that we are still overlapping this collider.
            // The overlap may no longer exist due to another intersected collider
            // pushing us out of this one.
            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                // If we intersect an object beneath us, set grounded to true. 
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _velocity.y < 0)
                {
                    _grounded = true;
                }
            }
        }
    }

}
