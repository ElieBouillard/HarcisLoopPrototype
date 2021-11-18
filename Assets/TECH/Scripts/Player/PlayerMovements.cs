using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovements : MonoBehaviour
{
    private NavMeshAgent _agent = null;

#region OnEnable/OnDisable
    private void OnEnable()
    {
        PlayerInputs.MouseClick0 += MoveAgent;
    }

    private void OnDisable()
    {
        PlayerInputs.MouseClick0 -= MoveAgent;
    }
#endregion

    private void Start()
    {
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void MoveAgent(Vector3 newPos)
    {
        _agent.SetDestination(newPos);
    }
}
