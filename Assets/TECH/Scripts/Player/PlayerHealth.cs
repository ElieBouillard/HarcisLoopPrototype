using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _initialHealth = 100f;
    private float _currHealth;

    private void Start()
    {
        _currHealth = _initialHealth;
    }

    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
    }
}
