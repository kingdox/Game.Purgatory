#region
using System;
using UnityEngine;
# endregion

[Serializable]
public class SelfDestroyDistanceController 
{
    #region Variables
    [SerializeField] private GameObject objToDestroy = default;
    [SerializeField] float maxDistance = default;
    #endregion
    #region Methods
    public void CheckDestroy()
    {
        if (
            Vector3.Distance(
                objToDestroy.transform.position,
                Vector3.zero
            ) > maxDistance
        ) UnityEngine.Object.Destroy(objToDestroy);
    }
    #endregion
}
