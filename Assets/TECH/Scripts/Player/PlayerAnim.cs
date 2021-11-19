using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator = null;
    [SerializeField] private NavMeshAgent _playerAgent = null;
    [Range(0f, 10f)]
    [SerializeField] private float speedRun = 0.5f;

    private void Update()
    {
        if (_playerAgent.velocity.sqrMagnitude < speedRun * speedRun) { SetRun(false); return; }

        if (_playerAnimator.GetBool("Run") == true) { return; }

        SetRun(true);
    }

    private void SetRun(bool value)
    {
        _playerAnimator.SetBool("Run", value);
    }
}
