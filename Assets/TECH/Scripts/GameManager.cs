using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Parameters")]
    [SerializeField] private float _initialStartRoundCouldown = 3f;

    [Space(20)]
    [Header("References")]
    [SerializeField] private CameraController _cameraController = null;
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] public ObjectiveFlag _objectifFlag = null;
    [SerializeField] private GameObject _waitingScreen = null;
    [SerializeField] private TMP_Text _teamIndexInfoText = null;
    [SerializeField] private TMP_Text _couldownText = null;
    [Space(20)]
    [SerializeField] private Transform[] _team0StartPos = null;
    [Space(20)]
    [SerializeField] private Transform[] _team1StartPos = null;    
    [Space(20)]
    [SerializeField] private Color[] _teamColors = new Color[0];

    private List<PlayerIdentity> _allCharactersPlayers = new List<PlayerIdentity>();

    private List<PlayerIdentity> _team0InLive = new List<PlayerIdentity>();
    private List<PlayerIdentity> _team1InLive = new List<PlayerIdentity>();

    private int currRoundTeamId = 0;

    private GameObject _currCharacter = null;
    private PlayerIdentity _currCharacterIdentity = null;

    [HideInInspector] public bool _waitingForRound = false;

    private float _startRoundCouldown = 0f;

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerHealth.PlayerDead += CharacterKilled; 
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDead -= CharacterKilled;        
    }
    #endregion

    private void Awake()
    {
        instance = this;
        _waitingScreen.SetActive(false);
    }

    private void Start()
    {
        InitializeRound();
        InitializeCouldown();
    }

    private void Update()
    {
        if (_waitingForRound)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                InitializeRound();
                _waitingScreen.SetActive(false);
                _waitingForRound = false;
                InitializeCouldown();
            }
        }

        if(_startRoundCouldown == -1) { return; }

        if(_startRoundCouldown > 0)
        {
            _startRoundCouldown -= Time.deltaTime;
            double couldownSec = Math.Ceiling(_startRoundCouldown);
            _couldownText.text = couldownSec.ToString();
        }
        else
        {
            _couldownText.text = "";
            StartRound();
            _startRoundCouldown = -1;
        }
    }

    private void InitializeCouldown()
    {
        _startRoundCouldown = _initialStartRoundCouldown;
    }

    private void InitializeRound()
    {
        DestroyAllEntity();

        _team0InLive = new List<PlayerIdentity>();
        _team1InLive = new List<PlayerIdentity>();

        _objectifFlag.ResetStartPos();

        if(_allCharactersPlayers.Count > 0)
        {
            for (int i = 0; i < _allCharactersPlayers.Count; i++)
            {
                PlayerIdentity currPlayer = _allCharactersPlayers[i];

                currPlayer.gameObject.SetActive(true);
                currPlayer.SetCharacterPlayable(false);

                if (_allCharactersPlayers[i].GetTeamIndex() == 0)
                {
                    _team0InLive.Add(currPlayer);
                    currPlayer.gameObject.transform.position = _team0StartPos[_team0InLive.Count - 1].position;
                    currPlayer.transform.rotation = _team0StartPos[_team0InLive.Count - 1].rotation;
                }
                else
                {
                    _team1InLive.Add(currPlayer);
                    currPlayer.gameObject.transform.position = _team1StartPos[_team1InLive.Count - 1].transform.position;
                    currPlayer.transform.rotation = _team1StartPos[_team1InLive.Count - 1].transform.rotation;
                }

                currPlayer.GetComponent<PlayerMovements>().StopAgentMovement();

                currPlayer.GetComponent<PlayerTeamColor>().ChangeCharacterColor(_teamColors[_allCharactersPlayers[i].GetTeamIndex()]);

                if (currPlayer.GetPlayFlagStatus()) { currPlayer.SetPlayerCatchFlag(false); currPlayer.GetComponent<PlayerTeamColor>().ChangeCharacterColor(_teamColors[2]); }

                currPlayer.GetComponent<PlayerHealth>().ResetHeal();
            }
        }

        InitializeNewCharacter();
    }

    private void StartRound()
    {
        if(_allCharactersPlayers.Count > 0)
        {
            for (int i = 0; i < _allCharactersPlayers.Count; i++)
            {
                PlayerIdentity currPlayer = _allCharactersPlayers[i];
                currPlayer._actionReader.StartRound();
            }
        }
        _currCharacterIdentity.SetCharacterPlayable(true);
        _currCharacterIdentity._actionWriter.StartWrite();
    }

    public void InitializeNewCharacter()
    {
        Vector3 spawnPoint = Vector3.zero;

        Quaternion rotation = new Quaternion();

        if (currRoundTeamId == 0)
        {
            spawnPoint = _team0StartPos[_team0InLive.Count].transform.position; rotation = Quaternion.identity; 
        }
        else if (currRoundTeamId == 1)
        {
            spawnPoint = _team1StartPos[_team1InLive.Count].transform.position; rotation = Quaternion.Euler(0, 180, 0);
        }

        _currCharacter = Instantiate(_playerPrefab, spawnPoint, rotation);
        _currCharacterIdentity = _currCharacter.GetComponent<PlayerIdentity>();
        _cameraController.AssignNewPlayerForCamera(_currCharacter);
        _currCharacterIdentity.SetTeamIndex(currRoundTeamId);

        _allCharactersPlayers.Add(_currCharacterIdentity);

        if (currRoundTeamId == 0) { _team0InLive.Add(_currCharacterIdentity); }
        else if (currRoundTeamId == 1) { _team1InLive.Add(_currCharacterIdentity); }

        _currCharacterIdentity.SetCharacterPlayable(false);
    }

    public void CharacterKilled(PlayerIdentity currPlayerIdentity)
    {
        if(_team0InLive.Contains(currPlayerIdentity)) 
        { 
            _team0InLive.Remove(currPlayerIdentity); 
        }
        if(_team1InLive.Contains(currPlayerIdentity))
        { 
            _team1InLive.Remove(currPlayerIdentity);
        }

        if(_team0InLive.Count == 0 || _team1InLive.Count == 0) { EndRound(); }
    }

    private void DestroyAllEntity()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject wall in walls) { Destroy(wall); }
        foreach (GameObject projectile in projectiles) { Destroy(projectile); }
    }

    public void EndRound()
    {
        foreach (PlayerIdentity character in _allCharactersPlayers)
        {
            character._actionReader.EndRound();
            character.gameObject.SetActive(false);
        }
        _currCharacterIdentity._actionReader.SaveActions(_currCharacterIdentity._actionWriter.GetActions());

        if(currRoundTeamId == 0) { currRoundTeamId = 1; }
        else { currRoundTeamId = 0; }

        _teamIndexInfoText.text = $"Team {currRoundTeamId}";
        _waitingScreen.SetActive(true);

        _waitingForRound = true;
    }
}
