#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
# endregion
[Serializable]
public class BackgroundCameraTypeController 
{
    [Serializable]
    private struct Background
    {
        [SerializeField] private Color color;
        [SerializeField] private TypeData type;
        public bool Check(TypeData t, ref Color c ){
            bool isType = type.Equals(t);
            if (isType) c = color;
            return isType;
        }
    }
    #region Variable
    [SerializeField] private Camera cam;
    [SerializeField] private Background[] bgs;
    #endregion
    #region Methods
    public void CheckBackground(TypeData t){
        Color c = cam.backgroundColor;
        int max = bgs.Length;
        for (int i = 0; i < max; i++) 
        {
            if (bgs[i].Check(t, ref c)){
                cam.backgroundColor = c;
                return;
            }
        }
    }
    #endregion
}
