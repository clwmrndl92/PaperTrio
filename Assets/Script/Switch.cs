using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isSwitch { get; set; }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")|| collision.CompareTag("Box")|| collision.CompareTag("Laser"))
        {
            isSwitch = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box") || collision.CompareTag("Laser"))
        {
            isSwitch = false;
        }
    }

    private void OnDisable()
    {
        Debug.Log("disable");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 1.0f); 

        foreach (Collider2D collision in collisions)
        {
            if (collision.CompareTag("Box")) // 다른 Collider2D가 "Player" 태그를 가진 경우
            {
                isSwitch = true;
                break;
            }
        }
    }

}
