#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Change;
using XavHelpTo.Set;
#endregion
[Serializable]
public class RotationController
{
    [SerializeField] private enum Axis
    {
        X,
        Y,
        Z
    }
    #region Variables
    private static readonly Vector3[] axisOpt =
    {
        Vector3.right,
        Vector3.up,
        Vector3.forward
    };
    [Header("Requirements")]
    [Space]
    [SerializeField] private Transform target;
    [Space]
    [SerializeField] private float intensity;
    [SerializeField] private Axis axis;
    #endregion
    #region Methods
    public void Rotate() => target.Rotate(axisOpt[axis.ToInt()], intensity * Time.deltaTime);
    #endregion
}
