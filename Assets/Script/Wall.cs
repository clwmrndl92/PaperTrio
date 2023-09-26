using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float speed;
    private float startPositionY;
    [SerializeField] private float moveDistance;
    private float endPositionY;

    private void Start()
    {
        startPositionY = transform.position.y;
        endPositionY = startPositionY - moveDistance;
    }
    public void Move()
    {
        transform.Translate(0, -speed, 0);
        if (transform.position.y <endPositionY)
        {
            transform.position = new Vector3(transform.position.x, endPositionY, 0);
        }
    }


    public void ReverseMove()
    {
        transform.Translate(0, speed, 0);
        if (transform.position.y > startPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, 0);
        }
    }
}
