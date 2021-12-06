using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _dissolveSpeed = 2f;
    private Material _material = null;
    private Collider _collider = null;
    private bool _builded = false;

    private void Start()
    {
        _material = this.gameObject.GetComponent<Renderer>().material;
        _collider = this.gameObject.GetComponent<Collider>();
        _collider.enabled = false;
        _builded = false;
    }

    float currDissolveVelocity = 0f;

    private void Update()
    {
        if(_builded) { return; }
        _material.SetFloat("_AdvancedDissolveCutoutStandardClip", Mathf.SmoothDamp(_material.GetFloat("_AdvancedDissolveCutoutStandardClip"), 0f, ref currDissolveVelocity, _dissolveSpeed));
        if (_material.GetFloat("_AdvancedDissolveCutoutStandardClip") < 0.1f) { Build(); }
    }

    private void Build()
    {
        _collider.enabled = true;
        _builded = true;
    }
}
