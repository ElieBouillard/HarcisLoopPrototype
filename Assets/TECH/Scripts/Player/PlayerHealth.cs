using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _initialHealth = 100f;
    [SerializeField] private Image _healthBarImg = null;
    private float _currHealth;

    public static event Action<PlayerIdentity> PlayerDead;

    private void Start()
    {
        _currHealth = _initialHealth;
    }

    public void ResetHeal()
    {
        _currHealth = _initialHealth;
        _healthBarImg.fillAmount = _currHealth / _initialHealth;
    }
    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
        _healthBarImg.fillAmount = _currHealth / _initialHealth;

        if(_currHealth <= 0)
        {
            if (this.gameObject.TryGetComponent<PlayerIdentity>(out PlayerIdentity   playerIdentity))
            {
                PlayerDead?.Invoke(playerIdentity);

                if (playerIdentity.GetPlayFlagStatus())
                {
                    playerIdentity.DropFlag();
                }
            }

            this.gameObject.SetActive(false);
        }
    }
}
