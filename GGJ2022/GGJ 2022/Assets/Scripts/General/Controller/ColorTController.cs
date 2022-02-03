using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
[Serializable]
public class ColorTController<T> where T : Component
{
    private T[] renders =  new T[0];
    [SerializeField] private Transform tr_container = default;

    public void CheckColor(Action<T> callback){
        if (renders.Length.Equals(0)) renders = tr_container.GetComponentsInChildren<T>();
        int max = renders.Length;
        for (int i = 0; i < max; i++) callback.Invoke(renders[i]);
    }
}
public static class ColorTControllerUtils
{
    public static void ChangeTrail(this ColorTController<TrailRenderer> tr, TypeData t)
    {
        tr.CheckColor(r => {
            r.material = DataSystem.GetLocal.GetParticle(t);
        });
    }

    public static void ChangeParticle(this ColorTController<ParticleSystem> tr, TypeData t)
    {
        tr.CheckColor(r => {
            r.GetComponent<ParticleSystemRenderer>().material = DataSystem.GetLocal.GetParticle(t);
        });
    }
}
