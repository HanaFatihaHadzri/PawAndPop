using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolManager : MonoBehaviour
{
    public Transform[] patrolPoints; //assign in inspector

    public void AssignPatrolPoints(PatrolEnemy patrolEnemy)
    {
        patrolEnemy.patrolPoints = patrolPoints;
    }
}
