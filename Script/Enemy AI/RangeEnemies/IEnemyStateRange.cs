using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStateRange
{
    //goes into a certain state
    void EnterState(RangedEnemy enemy);
    //Updates the state to do something
    void UpdateState(RangedEnemy enemy);

    //Leaves the state
    void ExitState(RangedEnemy enemy);
}
