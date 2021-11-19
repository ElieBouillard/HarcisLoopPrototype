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

    public static event Action CastSpellKeyPress;
    public static event Action StopPlayerMovementKeyPress;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _floorMask)) { return; }
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
            CastSpellKeyPress?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StopPlayerMovementKeyPress?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            LockCameraKeyPress?.Invoke();
        }
    }
}
