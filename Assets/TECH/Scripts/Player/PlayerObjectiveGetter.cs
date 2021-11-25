using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectiveGetter : MonoBehaviour
{
    public static event Action<GameObject> OnPlayerGetFlag; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ObjectiveFlag>())
        {
            OnPlayerGetFlag?.Invoke(this.gameObject);
        }
    }
}
