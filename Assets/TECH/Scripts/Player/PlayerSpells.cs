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


    private Vector3 _mouseWolrdPos = new Vector3();



    #region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerInputs.CastSpellKeyPress += CastQSpell;
        PlayerInputs.CastWallKeyPress += CastWallPreview;
        PlayerInputs.CastWallKeyRelease += InstantiateWall;
    }

    private void OnDisable()
    {
        PlayerInputs.CastSpellKeyPress -= CastQSpell;
        PlayerInputs.CastWallKeyPress -= CastWallPreview;
        PlayerInputs.CastWallKeyRelease -= InstantiateWall;
    }
    #endregion

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

        if (_playerUIOnFloor == null) { return; }
        _playerUIOnFloor.transform.forward = mouseDir;
    }

    private void CastQSpell()
    {
        Vector3 dir = (_mouseWolrdPos - transform.position).normalized;
        PlayerMovements.instance.StopAgent(0.35f);
        transform.forward = dir;
        GameObject projectileInstance = Instantiate(_projectilePrefab, _launchProjectileTransform.position, Quaternion.LookRotation(dir));
        projectileInstance.GetComponent<Projectile>().SetPos(_launchProjectileTransform.position);
    }

    private void CastWallPreview()
    {
        _wallPreviewParent.SetActive(true);
    }

    private void InstantiateWall()
    {
        Instantiate(_wallPrefab, _wallPreviewParent.transform.GetChild(0).position, _wallPreviewParent.transform.GetChild(0).rotation);
        _wallPreviewParent.SetActive(false);
        Vector3 dir = (_mouseWolrdPos - transform.position).normalized;
        PlayerMovements.instance.StopAgent(0.35f);
        transform.forward = dir;
    }
}
