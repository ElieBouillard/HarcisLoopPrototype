using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CameraController _cameraController = null;
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private GameObject _team0SpawnPos = null;
    [SerializeField] private GameObject _team1SpawnPos = null;
    [SerializeField] private GameObject _objectifFlag = null;

    private List<PlayerIdentity> _allCharactersPlayers = new List<PlayerIdentity>();
    private List<PlayerIdentity> _charactersTeam0 = new List<PlayerIdentity>();
    private List<PlayerIdentity> _charactersTeam1 = new List<PlayerIdentity>();

    private int teamIndex = 0;

    private GameObject currCharacter = null;
    private PlayerIdentity currCharacterIdentity = null;

    private void Awake()
    {
        instance = this;
        InitializeRound();
    }

    private void InitializeRound()
    {
        _objectifFlag.SetActive(true);

        foreach (PlayerIdentity character in _allCharactersPlayers)
        {
            character.SetCharacterPlayable(false);
            if (character.GetPlayFlagStatus()) { character.SetPlayerCatchFlag(false); }
            character._actionReader.StartRound();
            if(character.GetTeamIndex() == 0) { character.gameObject.transform.position = _team0SpawnPos.transform.position; }
            else { character.gameObject.transform.position = _team1SpawnPos.transform.position; }
        }

        Vector3 spawnPoint = Vector3.zero;

        if (teamIndex == 0) { spawnPoint = _team0SpawnPos.transform.position; }
        else { spawnPoint = _team1SpawnPos.transform.position; }

        currCharacter = Instantiate(_playerPrefab, spawnPoint, Quaternion.identity);
        currCharacterIdentity = currCharacter.GetComponent<PlayerIdentity>();
        _allCharactersPlayers.Add(currCharacterIdentity);
        _cameraController.SetCharacterOnCamera(currCharacter);
        currCharacterIdentity.SetTeamIndex(teamIndex);
    }

    public void EndRound()
    {
        foreach (PlayerIdentity character in _allCharactersPlayers)
        {
            character._actionReader.EndRound();
        }
        currCharacterIdentity._actionReader.SaveActions(currCharacterIdentity._actionWriter.GetActions());
        if(teamIndex == 0) { teamIndex = 1; }
        else { teamIndex = 0; }
        InitializeRound();
    }
}
