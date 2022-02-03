#region Access
using System;
using UnityEngine;
# endregion
[Serializable]
public class WeaponController 
{
    #region Variables
    [Header("Requirements")]
    [Space]
    [SerializeField] private Transform targetAim;
    [SerializeField] private BulletComponent prefab_bullet;
    #endregion
    #region Methods

    /// <summary>
    /// Shots a bullet in the specified direction
    /// </summary>
    public void Shot(StatsController stat){
        Vector3 _instancePos = targetAim.position + (targetAim.forward);
        Quaternion _instanceRotation = targetAim.rotation;

        BulletComponent bulletComponent = UnityEngine.Object.Instantiate(
            prefab_bullet,
            _instancePos,
            _instanceRotation,
            DataSystem.GetLocal.tr_parent_bullet
        );

        bulletComponent.SetStatController(stat);
    }
    #endregion
}
