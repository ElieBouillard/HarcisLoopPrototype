using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFlag : MonoBehaviour
{
    #region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerObjectiveGetter.PlayerCatchFlag += SelfDestroy;
    }

    private void OnDisable()
    {
        PlayerObjectiveGetter.PlayerCatchFlag -= SelfDestroy;
    }
    #endregion

    private void SelfDestroy()
    {
        this.gameObject.SetActive(false);
    }
}
