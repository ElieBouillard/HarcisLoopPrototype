using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField] private Transform _launchProjectileTransform = null;
    [SerializeField] private LayerMask _floorMask = new LayerMask();
    [SerializeField] private GameObject _projectile = null;
    [SerializeField] private GameObject _playerUIOnFloor = null;

    private Vector3 _mouseWolrdPos = new Vector3();

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerInputs.CastSpellKeyPress += CastQSpell;
    }

    private void OnDisable()
    {
        PlayerInputs.CastSpellKeyPress -= CastQSpell;
    }
    #endregion

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floorMask)) { return; }
        _mouseWolrdPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        if(_playerUIOnFloor == null) { return; }
        _playerUIOnFloor.transform.forward = (_mouseWolrdPos - transform.position).normalized;
    }

    private void CastQSpell()
    {
        Vector3 dir = (_mouseWolrdPos - transform.position).normalized;
        PlayerMovements.instance.StopAgent(0.35f);
        transform.forward = dir;
        GameObject projectileInstance = Instantiate(_projectile, _launchProjectileTransform.position, Quaternion.LookRotation(dir));
        projectileInstance.GetComponent<Projectile>().SetPos(_launchProjectileTransform.position);
    }
}
