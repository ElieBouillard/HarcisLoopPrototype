using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAction : ActionClass
{
    public WallAction(ActionTypes newActionType, float timing, Vector3 mousePos)
    {
        ActionType = newActionType;
        Timing = timing;
        MousePos = mousePos;
    }
}
