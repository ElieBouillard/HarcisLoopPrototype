using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectiveGetter : MonoBehaviour
{
    private PlayerIdentity _playerIdentity;

    public static event Action PlayerCatchFlag;

    private void Start()
    {
        _playerIdentity = this.gameObject.GetComponent<PlayerIdentity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_playerIdentity.GetTeamIndex() != 0) { return; }
        if (other.gameObject.GetComponent<ObjectiveFlag>())
        {
            PlayerCatchFlag?.Invoke();
            _playerIdentity.SetPlayerCatchFlag(true);
        }
    }
}
