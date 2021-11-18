using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private LayerMask _floorMask = new LayerMask();

    public static event Action<Vector3> MouseClick0;
    public static event Action<bool> SpacePress;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, _floorMask)) { return; }
            MouseClick0?.Invoke(hit.point);
        }

        if (Input.GetButtonDown("Jump"))
        {
            SpacePress?.Invoke(true);
        }
        if (Input.GetButtonUp("Jump"))
        {
            SpacePress?.Invoke(false);
        }
    }
}
