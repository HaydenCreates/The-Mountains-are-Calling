using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * interface to create enemy states 
 */
public interface IEnemyState
{
    //goes into a certain state
    void EnterState(Enemy enemy);
    //Updates the state to do something
    void UpdateState(Enemy enemy);

    //Leaves the state
    void ExitState(Enemy enemy);
}