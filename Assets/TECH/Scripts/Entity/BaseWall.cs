using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerIdentity>(out PlayerIdentity currPlayerIdentity))
        {
            if (currPlayerIdentity.GetPlayFlagStatus())
            {
                GameManager.instance.EndRound();
            }
        }
    }
}
