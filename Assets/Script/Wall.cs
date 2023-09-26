using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float speed;
    private float startPositionY;
    [SerializeField] private float moveDistance;
    private float endPositionY;
    [SerializeField] private float positionY = -2.1f;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
        startPositionY = transform.position.y;
        endPositionY = startPositionY - moveDistance;
    }
    public void Move()
    {
        Debug.Log(-speed * Time.deltaTime);
        transform.Translate(0, -speed*Time.deltaTime, 0);
        if (transform.position.y <endPositionY)
        {
            transform.position = new Vector3(transform.position.x, endPositionY, 0);
        }
    }


    public void ReverseMove()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if (transform.position.y > startPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, 0);
        }
    }
}
