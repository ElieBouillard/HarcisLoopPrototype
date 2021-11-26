using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _spellRangeDestroy = 2f;
    [SerializeField] private float _damage = 20f;

    private Rigidbody _rigidBody = null;
    private Vector3 _startPos = new Vector3();
    private int _teamIndex = -1;

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

    public void SetTeamIndex(int value)
    {
        _teamIndex = value;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerIdentity>(out PlayerIdentity playerIdentity))
        {
            if(playerIdentity.GetTeamIndex() == _teamIndex) { return; }

            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damage);
        }

        if (other.gameObject.GetComponent<Wall>())
        {
            Destroy(this.gameObject);
        }
    }
}
