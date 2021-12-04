using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFlag : MonoBehaviour
{
    [SerializeField] private Transform _startPos = null;

    #region OnEnable / OnDisable
    private void OnEnable()
    {
        PlayerObjectiveGetter.PlayerCatchFlag += GetCatched;
    }

    private void OnDisable()
    {
        PlayerObjectiveGetter.PlayerCatchFlag -= GetCatched;
    }
    #endregion

    private void GetCatched()
    {
        this.gameObject.SetActive(false);
    }

    public void GetDroped(Vector3 newWorldPos)
    {
        this.gameObject.SetActive(true);
        transform.position = newWorldPos;
    }

    public void ResetStartPos()
    {
        this.gameObject.SetActive(true);
        transform.position = _startPos.position;
    }
}
