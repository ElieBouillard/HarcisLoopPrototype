using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWriter : MonoBehaviour
{
    [SerializeField] private List<ActionClass> _actions = new List<ActionClass>();

#region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerInputs.MovementClickPress += SaveMovement;
        PlayerInputs.CastSpellKeyPress += SaveCastSpell;
        PlayerInputs.CastWallKeyRelease += SaveCastWall;
        PlayerInputs.StopPlayerMovementKeyPress += StopMovement;
    }

    private void OnDisable()
    {
        PlayerInputs.MovementClickPress -= SaveMovement;
        PlayerInputs.CastSpellKeyPress -= SaveCastSpell;
        PlayerInputs.CastWallKeyRelease -= SaveCastWall;
        PlayerInputs.StopPlayerMovementKeyPress -= StopMovement;
    }
    #endregion

    float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void SaveCastSpell(Vector3 dir)
    {
        ActionClass newAction = new SpellAction(ActionTypes.spell,time, dir);
        _actions.Add(newAction);
    }

    private void SaveCastWall(Vector3 dir)
    {
        ActionClass newAction = new WallAction(ActionTypes.wall, time, dir);
        _actions.Add(newAction);
    }

    private void SaveMovement(Vector3 mousePos)
    {
        ActionClass newAction = new MovementAction(ActionTypes.movement, time, mousePos);
        _actions.Add(newAction);
    }

    private void StopMovement()
    {
        ActionClass newAction = new MovementAction(ActionTypes.stopMovement, time, Vector3.zero);
        _actions.Add(newAction);
    }
}
