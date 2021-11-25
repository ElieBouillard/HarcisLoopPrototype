using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentity : MonoBehaviour
{
    private int _teamIndex = 0;
    private bool _getFlag = false;

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
