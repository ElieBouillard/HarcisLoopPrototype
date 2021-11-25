using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWriter : MonoBehaviour
{
    [SerializeField] private List<ActionClass> _actions = new List<ActionClass>();

    private PlayerInputs _playerInputs = null;

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        _playerInputs.MovementClickPress += SaveMovement;
        _playerInputs.CastSpellKeyPress += SaveCastSpell;
        _playerInputs.CastWallKeyRelease += SaveCastWall;
        _playerInputs.StopPlayerMovementKeyPress += StopMovement;
    }

    private void OnDisable()
    {
        _playerInputs.MovementClickPress -= SaveMovement;
        _playerInputs.CastSpellKeyPress -= SaveCastSpell;
        _playerInputs.CastWallKeyRelease -= SaveCastWall;
        _playerInputs.StopPlayerMovementKeyPress -= StopMovement;
    }
    #endregion

    private void Awake()
    {
        _playerInputs = this.gameObject.GetComponent<PlayerInputs>();
    }

    float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
    }

    public ActionClass[] GetActions()
    {
        return _actions.ToArray();
    }

    private void SaveCastSpell(Vector3 dir)
    {
        ActionClass newAction = new ActionClass(ActionTypes.spell,time, dir);
        _actions.Add(newAction);
    }

    private void SaveCastWall(Vector3 dir)
    {
        ActionClass newAction = new ActionClass(ActionTypes.wall, time, dir);
        _actions.Add(newAction);
    }

    private void SaveMovement(Vector3 mousePos)
    {
        ActionClass newAction = new ActionClass(ActionTypes.movement, time, mousePos);
        _actions.Add(newAction);
    }

    private void StopMovement()
    {
        ActionClass newAction = new ActionClass(ActionTypes.stopMovement, time, Vector3.zero);
        _actions.Add(newAction);
    }
}
