using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform[] patrolPoints; // Patrol points to follows

    private int currentPatrolIndex = 0;
    private float patrolSpeed = 2f;

    private void Update()
    {
        // If patrol points are assigned, move between them
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPatrolIndex];

        // Move towards the next patrol point
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Switch to the next patrol point once the current one is reached
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    public void SetPatrolPoints(Transform[] points)
    {
        patrolPoints = points;
        currentPatrolIndex = 0; // reset to start at the first point
    }
}
