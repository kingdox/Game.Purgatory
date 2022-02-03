using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo;

[Serializable]
public class GeneratorController<T> where T: ComponentBase
{
    private Transform[] points = new Transform[0];
    [Header("Requirements")]
    [Space]
    [SerializeField] private Transform tr_parent_points;
    [SerializeField] private T t;
    public T Generate(){
        if (points.Length.Equals(0))
        {
            tr_parent_points.Components(out points);
        }
        Transform target = points.Any();
        return UnityEngine.Object.Instantiate(
            t,
            target.position,
            Quaternion.identity,
            target
        );
    }
}
