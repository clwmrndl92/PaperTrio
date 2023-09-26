using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private float rayDistance;
    [SerializeField] private Vector3 rayDirection;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        lr.SetPosition(0, transform.position);
        Vector3 rayEnd = transform.position;
        rayEnd.x += rayDistance;
        lr.SetPosition(1, rayEnd);

        Vector2[] colliderPoints;
        colliderPoints = col.points;
        colliderPoints[0].x =0;
        colliderPoints[1].x = rayEnd.x;
        col.points = colliderPoints;
    }

    private void hitRay()
    {
        Vector3 rayEnd = transform.position;
        rayEnd.x += rayDistance;
        
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, rayDirection, rayDistance);
        if (hit.Length != 0)
        {
            foreach (var item in hit)
            {
                if(item.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    float colliderSizeX = item.point.x - transform.position.x;
                    rayEnd = item.point;

                    lr.SetPosition(1, rayEnd);
                    Vector2[] colliderPoints;
                    colliderPoints = col.points;
                    colliderPoints[1].x = colliderSizeX;
                    col.points = colliderPoints;
                    return;
                }
            }
           
        }
        
        lr.SetPosition(1, rayEnd);
        col.points[1] = rayEnd;
    }
    private void Update()
    {
        hitRay();
    }
}
