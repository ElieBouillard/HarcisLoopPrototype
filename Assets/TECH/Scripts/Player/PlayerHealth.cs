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
        _currHealth = _initialHealth;
        _healthBarImg.fillAmount = _currHealth / _initialHealth;
    }
    public void TakeDamage(float damage)
    {
        _currHealth -= damage;
        _healthBarImg.fillAmount = _currHealth / _initialHealth;

        if(_currHealth <= 0)
        {
            GameManager.instance.CharacterKilled(this.gameObject.GetComponent<PlayerIdentity>());
            if (this.gameObject.GetComponent<PlayerIdentity>().GetPlayFlagStatus())
            {
                GameManager.instance.EndRound();
            }
            this.gameObject.SetActive(false);
        }
    }
}
