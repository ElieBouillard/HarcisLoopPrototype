using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private LayerMask _floorMask = new LayerMask();

    public static event Action<Vector3> MovementClickPress;
    public static event Action<bool> RecenterCameraKeyPress;
    public static event Action LockCameraKeyPress;
    public static event Action<Vector3> CastSpellKeyPress;
    public static event Action CastWallKeyPress;
    public static event Action<Vector3> CastWallKeyRelease;
    public static event Action StopPlayerMovementKeyPress;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _floorMask);
        Vector3 dir = (hit.point - transform.position).normalized;

        if (Input.GetMouseButtonDown(1))        {
            
            MovementClickPress?.Invoke(hit.point);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecenterCameraKeyPress?.Invoke(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            RecenterCameraKeyPress?.Invoke(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CastSpellKeyPress?.Invoke(dir);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StopPlayerMovementKeyPress?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            LockCameraKeyPress?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastWallKeyPress?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            CastWallKeyRelease?.Invoke(dir);
        }
    }
}
