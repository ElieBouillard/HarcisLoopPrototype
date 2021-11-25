using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private PlayerIdentity[] _allCharactersPlayers = new PlayerIdentity[0];
    private PlayerIdentity[] _charactersTeam0 = new PlayerIdentity[0];
    private PlayerIdentity[] _charactersTeam1 = new PlayerIdentity[0];

    private void InitializeRound()
    {
        foreach (PlayerIdentity character in _allCharactersPlayers)
        {

        }
    }
}
