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
            if(!isStopped){
                isStopped = true;
                currentSpeed = -currentSpeed;
                StartCoroutine(ChangeSpeed());
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if ( other.gameObject.CompareTag("Box")){
            if(!isStopped)
                other.transform.Translate(currentSpeed*Time.deltaTime, 0, 0);
        }
    }
    private void OnEnable() {
        isStopped = false;
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
        if(!isStopped)
            transform.Translate(currentSpeed*Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        Move();
    }

    IEnumerator ChangeSpeed(){
        yield return new WaitForSeconds(stopTime);
        isStopped = false;
    }

}
