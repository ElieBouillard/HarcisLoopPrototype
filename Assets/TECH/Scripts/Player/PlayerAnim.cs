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
    [SerializeField] private GameObject _clickOnFloorFXPrefab = null;

    private PlayerInputs _playerInputs = null; 

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        _playerInputs.CastSpellKeyPress += TriggerCastSpell;
        _playerInputs.CastWallKeyRelease += TriggerCastSpell;
        _playerInputs.MovementClickPress += CastClickOnFloorFX;
    }

    private void OnDisable()
    {
        _playerInputs.CastSpellKeyPress -= TriggerCastSpell;
        _playerInputs.CastWallKeyRelease -= TriggerCastSpell;
        _playerInputs.MovementClickPress -= CastClickOnFloorFX;
    }
    #endregion

    private void Awake()
    {
        _playerInputs = this.gameObject.GetComponent<PlayerInputs>();
    }

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

    public void TriggerCastSpell(Vector3 pos)
    {
        _playerAnimator.SetTrigger("CastSpell");
    }

    GameObject currFx = null;
    private void CastClickOnFloorFX(Vector3 pos)
    {
        if(currFx != null) { Destroy(currFx); }

        currFx = Instantiate(_clickOnFloorFXPrefab, pos + new Vector3(0f, 0.01f, 0f), Quaternion.identity);

        if(currFx != null) { Destroy(currFx, 1.5f); }
    }
}
