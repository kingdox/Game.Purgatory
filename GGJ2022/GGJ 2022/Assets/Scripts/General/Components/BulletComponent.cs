#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Change;
using XavHelpTo.Know;
# endregion

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public sealed class BulletComponent : ComponentBase
{
    #region  Variables
    [Header("Controllers")]
    [Space]
    [SerializeField] private PhysicController ctrl_physic = default;
    [SerializeField] private StatsController ctrl_stat = default;
    [SerializeField] private SelfDestroyDistanceController ctrl_selfDestroyDistance;

    [Header("Rendering")]
    [Space]
    [SerializeField] private ColorTypeController ctrl_colorType;
    #endregion
    #region Events
    private void Start()
    {
        //ctrl_stat.Type = ctrl_stat.Type;
    }
    private void FixedUpdate()
    {
        ctrl_physic.MoveInDirection(ctrl_stat.SpeedBullet,transform.forward);
        ctrl_selfDestroyDistance.CheckDestroy();
    }
    #endregion
    #region Methods
    protected override void Suscribe(bool condition)
    {
        //if (condition)
        //{
        //    ctrl_stat.OnTypeChange += ctrl_colorType.CheckColor;
        //}
        //else
        //{
        //    ctrl_stat.OnTypeChange -= ctrl_colorType.CheckColor;
        //}
    }

    public void TakeDamageByContact(StatsController _stat) {
        bool isSameType = ctrl_stat.Type.Equals(_stat.Type);
        _stat.ChangeLife(ctrl_stat.Damage * (isSameType ? 1 : -1));
        Destroy(gameObject);
    }
    public void SetStatController(StatsController stat)
    {
        ctrl_stat = stat;
        ctrl_colorType.CheckColor(stat.Type);
    }
    #endregion
}