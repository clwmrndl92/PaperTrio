using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;
    private float currentSpeed;
    bool isStopped = false;
    private Transform boxTransform;
    private void Start()
    {
        currentSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ãæµ¹");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")
             &&!collision.gameObject.CompareTag("Box"))
        {
            Debug.Log("º®");
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

        if (collision.gameObject.CompareTag("Box"))
        {
            boxTransform = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            boxTransform = null;
        }
    }

    private void OnEnable() {
        isStopped = false;
    }
    
    private void Move()
    {
        if (!isStopped)
        {
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);

            if(boxTransform!=null)
                boxTransform.Translate(currentSpeed * Time.deltaTime, 0, 0);
        }
            
    }

    private void FixedUpdate()
    {
        Move();
    }
    /*private void Update()
    {
        Move();
    }*/

    IEnumerator ChangeSpeed(){
        yield return new WaitForSeconds(stopTime);
        isStopped = false;
    }

}
