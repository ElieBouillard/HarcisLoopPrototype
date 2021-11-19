using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _spellRangeDestroy = 2f;

    private Rigidbody _rigidBody = null;
    private Vector3 _startPos = new Vector3();

    private void Start()
    {
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _rigidBody.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        float distToPlayer = (this.transform.position - _startPos).sqrMagnitude;
        if (distToPlayer > _spellRangeDestroy * _spellRangeDestroy)
        {
            SelfDestroy();
        }
    }

    public void SetPos(Vector3 newPos)
    {
        _startPos = newPos;
        this.transform.position = newPos;
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
