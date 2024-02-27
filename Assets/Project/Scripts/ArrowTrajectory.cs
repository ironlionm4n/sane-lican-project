using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrajectory : MonoBehaviour
{
    public GameObject arrowPrefab; // Assign in inspector
    public Transform target; // Assign the player's transform
    public float fireRate = 2.0f; // Seconds between shots
    public float arrowSpeed = 10.0f; // Speed of the fired arrow
    public float aimAngle = 45.0f; // Angle of aim above the horizontal

    private float lastFireTime = 0;

    void Update()
    {
        if(Time.time > lastFireTime + fireRate)
        {
            FireArrow();
            lastFireTime = Time.time;
        }
    }

    void FireArrow()
    {
        Vector2 directionToTarget = target.position - transform.position;
        directionToTarget.Normalize();

        // Calculate aim direction with angle
        float angleRad = aimAngle * Mathf.Deg2Rad;
        Vector2 aimDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        // Rotate aimDirection towards the player
        float angleBetween = Vector2.SignedAngle(Vector2.right, directionToTarget);
        Quaternion rotation = Quaternion.Euler(0, 0, angleBetween);
        Vector2 finalDirection = rotation * aimDirection;

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = finalDirection * arrowSpeed;
    }
}
