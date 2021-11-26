using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamColor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer = null;

    public void ChangeCharacterColor(Color newColor)
    {
        _renderer.material.color = newColor;
    }
}
