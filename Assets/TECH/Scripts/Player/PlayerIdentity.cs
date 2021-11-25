using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    private PlayerInputs _playerInputs = null;
    private PlayerSpells _playerSpells = null;

    private int _teamIndex = 0;
    private bool _getFlag = false;

    private void Awake()
    {
        _playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        _playerSpells = this.gameObject.GetComponent<PlayerSpells>();
    }

    public void SetCharacterPlayable(bool value)
    {
        _playerInputs.enabled = value;
        _playerSpells._playerUIOnFloor.SetActive(value);
    }

    public void SetTeamIndex (int value)
    {
        _teamIndex = value;
    }

    public int GetTeamIndex()
    {
        return _teamIndex;
    }

    public void PlayerCatchFlag()
    {
        _getFlag = true;
    }
}
