using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;
    private float currentSpeed;
    bool isStopped = false;

    private void Start()
    {
        currentSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            currentSpeed = 0;
            // isStopped = true;
            Invoke("ChangeSpeed", stopTime);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
    private void Move()
    {
        transform.Translate(currentSpeed*Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        Move();
    }

    void ChangeSpeed(){
        speed *= -1;
        currentSpeed = speed;
        // isStopped = false;
    }

}
