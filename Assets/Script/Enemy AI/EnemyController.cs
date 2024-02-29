using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controls the spawn of different types of enemies
 */
public class EnemyController : MonoBehaviour
{
    void Start()
    {
        Enemy basicEnemy = new Enemy();
        basicEnemy.SetState(new PatrolState());

        RangedEnemy rangedEnemy = new RangedEnemy();
        rangedEnemy.SetState(new RangedPatrolState());
    }
}
