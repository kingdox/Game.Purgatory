#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
#endregion
[Serializable]
public class MeshTypeController 
{
    [Serializable]
    private struct MeshOption
    {
        [SerializeField] private TypeData type;
        [SerializeField] private GameObject pref;
        public GameObject Compare(TypeData t) => type.Equals(t) ? pref : null;
    }
    #region Variables
    [Header("Requirements")]
    [SerializeField] private Transform tr_parent;
    [SerializeField] private MeshOption[] options;

    #endregion
    #region Methods
    /// <summary>
    /// Check if changes, returns true if it was changed
    /// </summary>
    public bool Check(TypeData t){
        int max = options.Length;
        for (int i = 0; i < max; i++)
        {
            GameObject obj = options[i].Compare(t);
            if (obj){
                tr_parent.ClearChilds();
                UnityEngine.Object.Instantiate(
                    obj,
                    tr_parent
                );
                return true;
            }
        }
        return false;
    }
    #endregion
}
