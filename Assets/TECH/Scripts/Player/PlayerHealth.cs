using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _initialHealth = 100f;
    [SerializeField] private Image _healthBarImg = null;
    private float _currHealth;

    private void Start()
    {
        _currHealth = _initialHealth;
    }

    public void ResetHeal()
    {

    }
    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
        _healthBarImg.fillAmount = _currHealth / _initialHealth;

        if(_currHealth <= 0)
        {
            this.gameObject.SetActive(false);

            if (this.gameObject.GetComponent<PlayerIdentity>().GetPlayFlagStatus())
            {
                GameManager.instance.EndRound();
            }
        }
    }
}
