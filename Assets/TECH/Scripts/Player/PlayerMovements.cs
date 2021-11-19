using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    public static PlayerMovements instance;

    [SerializeField] private LayerMask _floorMask = new LayerMask();

    private NavMeshAgent _agent = null;
    private float _stopDurationClock = 0f;

#region OnEnable/OnDisable

    private void OnEnable()
    {
        PlayerInputs.MovementClickPress += MoveAgent;
        PlayerInputs.StopPlayerMovementKeyPress += StopAgentMovement;
    }

    private void OnDisable()
    {
        PlayerInputs.MovementClickPress -= MoveAgent;
        PlayerInputs.StopPlayerMovementKeyPress -= StopAgentMovement;
    }
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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

    private void MoveAgent(Vector3 newPos)
    {
        _agent.SetDestination(newPos);
    }

    private  void StopAgentMovement()
    {
        _agent.ResetPath();
    }

    public void StopAgent(float duration)
    {
        _stopDurationClock = duration;
        _agent.isStopped = true;
    }
}
