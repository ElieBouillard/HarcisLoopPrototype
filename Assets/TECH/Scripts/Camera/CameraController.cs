using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _player;

    [Header("Camera Parameters")]
    [Space(20)]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 0f;
    [SerializeField] private bool cameraLockOnPlayer = false;
    //[SerializeField] private Vector2 screenXLimits = Vector2.zero;
    //[SerializeField] private Vector2 screenZLimits = Vector2.zero;

    private PlayerInputs _playerInputs = null;

#region OnEnable/OnDisable
    private void OnEnable()
    {
        _playerInputs.RecenterCameraKeyPress += CameraOnPlayerPos;
        _playerInputs.LockCameraKeyPress += CameraLock;
    }

    private void OnDisable()
    {
        _playerInputs.RecenterCameraKeyPress -= CameraOnPlayerPos;
        _playerInputs.LockCameraKeyPress -= CameraLock;
    }
    #endregion

    private void Awake()
    {
        _playerInputs = _player.gameObject.GetComponent<PlayerInputs>();
    }

    private void Update()
    {

        if (!Application.isFocused) { return; }

        if (cameraLockOnPlayer)
        {
            transform.position = new Vector3(_player.transform.position.x + 5f, 8f, _player.transform.position.z - 5f);
            return;
        }

        Vector3 pos = transform.position;
        Vector3 cursorMovement = Vector3.zero;
        Vector2 cursorPosition = Input.mousePosition;

        if (cursorPosition.y >= Screen.height - screenBorderThickness)
        {
            cursorMovement.x -= 1;
            cursorMovement.z += 1;
        }
        else if (cursorPosition.y <= screenBorderThickness)
        {
            cursorMovement.x += 1;
            cursorMovement.z -= 1;
        }
        if (cursorPosition.x >= Screen.width - screenBorderThickness)
        {
            cursorMovement.x += 1;
            cursorMovement.z += 1;
        }
        else if (cursorPosition.x <= screenBorderThickness)
        {
            cursorMovement.x -= 1;
            cursorMovement.z -= 1;
        }

        pos += cursorMovement.normalized * speed * Time.deltaTime;

        //pos.x = Mathf.Clamp(pos.x, screenXLimits.x + transform.position.x, screenXLimits.y + transform.position.x);
        //pos.z = Mathf.Clamp(pos.z, screenZLimits.x + transform.position.z, screenZLimits.y + transform.position.z);

        transform.position = pos;
    }

    private void CameraLock()
    {
        cameraLockOnPlayer = !cameraLockOnPlayer;
    }

    private void CameraOnPlayerPos(bool value)
    {
        cameraLockOnPlayer = value;
    }

    public void SetCharacterOnCamera(GameObject character)
    {
        _player = character;
    }
}
