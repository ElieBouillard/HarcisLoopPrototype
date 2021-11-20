using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{
    public bool playerAction = false;
    public ActionClass[] _actionsSaved = new ActionClass[0];
    public bool[] _actionsDone = new bool[0];

    float time = 0f;

    private void Start()
    {
        _actionsDone = new bool[_actionsSaved.Length];
    }

    private void Update()
    {
        if(!playerAction) { return; }

        time += Time.deltaTime;

        for (int i = 0; i < _actionsSaved.Length; i++)
        {
            if (_actionsSaved[i].Timing < time + 0.5f && _actionsSaved[i].Timing > time - 0.5f)
            {
                if(_actionsDone[i] == false)
                {
                    switch (_actionsSaved[i].ActionType)
                    {
                        case ActionTypes.movement:
                            PlayerMovements.instance.MoveAgent(_actionsSaved[i].MousePos);
                            Debug.Log("dd");
                            break;
                        case ActionTypes.stopMovement:
                            PlayerMovements.instance.StopAgentMovement();
                            break;
                        case ActionTypes.spell:
                            PlayerSpells.instance.CastQSpell(_actionsSaved[i].MousePos);
                            PlayerAnim.instance.TriggerCastSpell(_actionsSaved[i].MousePos);
                            break;
                        case ActionTypes.wall:
                            PlayerSpells.instance.InstantiateWall(_actionsSaved[i].MousePos);
                            PlayerAnim.instance.TriggerCastSpell(_actionsSaved[i].MousePos);
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
