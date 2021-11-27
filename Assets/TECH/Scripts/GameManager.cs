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

    [SerializeField] private Color[] _teamColors = new Color[0];

    private List<PlayerIdentity> _allCharactersPlayers = new List<PlayerIdentity>();
    private List<PlayerIdentity> _team0InLive = new List<PlayerIdentity>();
    private List<PlayerIdentity> _team1InLive = new List<PlayerIdentity>();

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
        _team0InLive = new List<PlayerIdentity>();
        _team1InLive = new List<PlayerIdentity>();

        _objectifFlag.SetActive(true);
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject wall in walls) { Destroy(wall); }

        foreach (GameObject projectile in projectiles) { Destroy(projectile); }


        foreach (PlayerIdentity character in _allCharactersPlayers)
        {
            character.gameObject.SetActive(true);
            character.SetCharacterPlayable(false);
            character.gameObject.GetComponent<PlayerHealth>();
            character._actionReader.StartRound();

            if(character.GetTeamIndex() == 0)
            {
                character.gameObject.transform.position = _team0SpawnPos.transform.position; 
                character.transform.rotation = _team0SpawnPos.transform.rotation;
                _team0InLive.Add(character);
            }
            else 
            { 
                character.gameObject.transform.position = _team1SpawnPos.transform.position;
                character.transform.rotation = _team1SpawnPos.transform.rotation; 
                _team1InLive.Add(character);
            }

            character.GetComponent<PlayerMovements>().StopAgentMovement();
            character.GetComponent<PlayerTeamColor>().ChangeCharacterColor(_teamColors[character.GetTeamIndex()]);
            if (character.GetPlayFlagStatus()) { character.SetPlayerCatchFlag(false); character.GetComponent<PlayerTeamColor>().ChangeCharacterColor(_teamColors[2]); }
            character.GetComponent<PlayerHealth>().ResetHeal();
        }

        Vector3 spawnPoint = Vector3.zero;

        if (teamIndex == 0) { spawnPoint = _team0SpawnPos.transform.position; }
        else { spawnPoint = _team1SpawnPos.transform.position; }

        currCharacter = Instantiate(_playerPrefab, spawnPoint, Quaternion.identity);
        currCharacterIdentity = currCharacter.GetComponent<PlayerIdentity>();
        _allCharactersPlayers.Add(currCharacterIdentity);
        _cameraController.AssignNewPlayerForCamera(currCharacter);
        currCharacterIdentity.SetTeamIndex(teamIndex);
    }

    public void CharacterKilled(PlayerIdentity currPlayerIdentity)
    {
        if(_team0InLive.Contains(currPlayerIdentity)) { _team0InLive.Remove(currPlayerIdentity); }
        if(_team1InLive.Contains(currPlayerIdentity)) { _team0InLive.Remove(currPlayerIdentity); }

        if(_team0InLive.Count == 0 || _team1InLive.Count == 0) { EndRound(); }
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
