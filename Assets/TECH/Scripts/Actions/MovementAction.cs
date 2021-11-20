using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : ActionClass
{
    public MovementAction(ActionTypes newActionType, float timing, Vector3 mousePos)
    {
        ActionType = newActionType;
        Timing = timing;
        MousePos = mousePos;
    }
}
