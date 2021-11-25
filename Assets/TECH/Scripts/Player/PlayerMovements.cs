using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInputs _playerInputs;

    private NavMeshAgent _agent = null;
    private float _stopDurationClock = 0f;

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        _playerInputs.MovementClickPress += MoveAgent;
        _playerInputs.StopPlayerMovementKeyPress += StopAgentMovement;
    }

    private void OnDisable()
    {
        _playerInputs.MovementClickPress -= MoveAgent;
        _playerInputs.StopPlayerMovementKeyPress -= StopAgentMovement;
    }
    #endregion

    private void Awake()
    {
        _playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_stopDurationClock > 0 && _stopDurationClock != -1)
        {
            _stopDurationClock -= Time.deltaTime;
        }
        else
        {
            _agent.isStopped = false;
            _stopDurationClock = -1;
        }
    }

    public void MoveAgent(Vector3 newPos)
    {
        _agent.SetDestination(newPos);
    }

    public  void StopAgentMovement()
    {
        _agent.ResetPath();
    }

    public void StopAgent(float duration)
    {
        _stopDurationClock = duration;
        _agent.isStopped = true;
    }
}
