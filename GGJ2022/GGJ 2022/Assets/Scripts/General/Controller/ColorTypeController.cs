using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
[Serializable]
public class ColorTypeController 
{
    private MeshRenderer[] renders =  new MeshRenderer[0];
    [SerializeField] private Transform tr_container = default;
    public void CheckColor(TypeData type){
        LocalData local = DataSystem.GetLocal;
        Material materialToChange = local[type];
        if (renders.Length.Equals(0)) renders = tr_container.GetComponentsInChildren<MeshRenderer>();
        int max = renders.Length;
        for (int i = 0; i < max; i++) renders[i].material = materialToChange;
    }
}
