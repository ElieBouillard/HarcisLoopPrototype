using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAction : ActionClass
{
    public SpellAction(ActionTypes newActionType, float timing, Vector3 mousePos)
    {
        ActionType = newActionType;
        Timing = timing;
        MousePos = mousePos;
    }
}
