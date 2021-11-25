using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private Transform _launchProjectileTransform = null;
    [SerializeField] private GameObject _wallPreviewParent = null;
    [SerializeField] private LayerMask _floorMask = new LayerMask();
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField] private GameObject _wallPrefab = null;
    [SerializeField] private GameObject _playerUIOnFloor = null;

    private PlayerInputs _playerInputs = null;
    private PlayerMovements _playerMovement = null;
    
    private Vector3 _mouseWolrdPos = new Vector3();

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        _playerInputs.CastSpellKeyPress += CastQSpell;
        _playerInputs.CastWallKeyPress += CastWallPreview;
        _playerInputs.CastWallKeyRelease += InstantiateWall;
    }

    private void OnDisable()
    {
        _playerInputs.CastSpellKeyPress -= CastQSpell;
        _playerInputs.CastWallKeyPress -= CastWallPreview;
        _playerInputs.CastWallKeyRelease -= InstantiateWall;
    }
    #endregion

    private void Awake()
    {
        _playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        _playerMovement = this.gameObject.GetComponent<PlayerMovements>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floorMask)) { return; }
        _mouseWolrdPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        Vector3 mouseDir = (_mouseWolrdPos - transform.position).normalized;

        if (_wallPreviewParent.activeSelf)
        {
            _wallPreviewParent.transform.forward = mouseDir;
        }

        if (_playerUIOnFloor == null || !_playerUIOnFloor.activeSelf) { return; }
        _playerUIOnFloor.transform.forward = mouseDir;
    }

    public void CastQSpell(Vector3 dir)
    {
        _playerMovement.StopAgent(0.35f);
        transform.forward = dir;
        GameObject projectileInstance = Instantiate(_projectilePrefab, _launchProjectileTransform.position, Quaternion.LookRotation(dir));
        projectileInstance.GetComponent<Projectile>().SetPos(_launchProjectileTransform.position);
    }

    private void CastWallPreview()
    {
        _wallPreviewParent.SetActive(true);
    }

    public void InstantiateWall(Vector3 dir)
    {
        transform.forward = dir;
        Vector3 pos = transform.position + new Vector3(0f,0.75f,0f);
        GameObject wallInstance = Instantiate(_wallPrefab, pos, Quaternion.identity);
        wallInstance.transform.forward = dir;
        wallInstance.transform.position += wallInstance.transform.forward * 2f;
        _playerMovement.StopAgent(0.35f);
        _wallPreviewParent.SetActive(false);
    }
}
