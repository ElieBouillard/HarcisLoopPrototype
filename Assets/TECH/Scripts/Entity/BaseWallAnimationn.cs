using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallAnimationn : MonoBehaviour
{
    [SerializeField] private Vector3 _speedScroll = Vector3.zero;
    private Material _material = null;

    private void Start()
    {
        _material = this.gameObject.GetComponent<Renderer>().material;
    }
    Vector3 scroll = Vector3.zero;
    private void Update()
    {
        scroll += _speedScroll;
        _material.SetVector("_AdvancedDissolveCutoutStandardMap1Scroll", scroll);
    }
}
