using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] List<Wall> walls = new();
    [SerializeField] private bool isSwitch;

    private void moveWall()
    {
        if (!isSwitch)
        {
            foreach (Wall wall in walls)
            {
                if (wall.transform.parent.gameObject.activeSelf)
                {
                    wall.ReverseMove();
                }
            }
            return;
        }
            

        foreach(Wall wall in walls)
        {
            if (wall.transform.parent.gameObject.activeSelf)
            {
                wall.Move();
            }
        }
    }

    private void Update()
    {
        moveWall();
    }
}
