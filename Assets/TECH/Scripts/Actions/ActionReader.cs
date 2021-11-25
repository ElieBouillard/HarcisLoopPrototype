using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReader : MonoBehaviour
{
    public bool playerAction = false;
    public ActionClass[] _actionsSaved = new ActionClass[0];
    public bool[] _actionsDone = new bool[0];

    private PlayerMovements _playerMovements = null;
    private PlayerSpells _playerSpells = null;
    private PlayerAnim _playerAnim = null;

    float time = 0f;

    private void Awake()
    {
        _playerMovements = this.gameObject.GetComponent<PlayerMovements>();
        _playerSpells = this.gameObject.GetComponent<PlayerSpells>();
        _playerAnim = this.gameObject.GetComponent<PlayerAnim>();
        _actionsDone = new bool[_actionsSaved.Length];
    }

    private void Update()
    {
        if(!playerAction) { return; }

        time += Time.deltaTime;

        for (int i = 0; i < _actionsSaved.Length; i++)
        {
            if (_actionsSaved[i].Timing < time + 0.1f && _actionsSaved[i].Timing > time - 0.1f)
            {
                if(_actionsDone[i] == false)
                {
                    switch (_actionsSaved[i].ActionType)
                    {
                        case ActionTypes.movement:
                            _playerMovements.MoveAgent(_actionsSaved[i].MousePos);
                            break;
                        case ActionTypes.stopMovement:
                            _playerMovements.StopAgentMovement();
                            break;
                        case ActionTypes.spell:
                            _playerSpells.CastQSpell(_actionsSaved[i].MousePos);
                            _playerAnim.TriggerCastSpell(_actionsSaved[i].MousePos);
                            break;
                        case ActionTypes.wall:
                            _playerSpells.InstantiateWall(_actionsSaved[i].MousePos);
                            _playerAnim.TriggerCastSpell(_actionsSaved[i].MousePos);
                            break;
                        case ActionTypes.nothing:
                            break;
                    }
                    _actionsDone[i] = true;
                }
            }
        }
    }
}
