using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionClass
{
    public ActionTypes ActionType = ActionTypes.nothing;
    public float Timing = 0f;
    public Vector3 MousePos;
}

public enum ActionTypes
{
    movement,
    stopMovement,
    spell,
    wall,
    nothing
}
