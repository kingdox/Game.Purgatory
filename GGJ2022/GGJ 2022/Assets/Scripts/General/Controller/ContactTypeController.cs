#region
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XavHelpTo;
# endregion
[Serializable]
public class ContactTypeController<T> where T : ComponentBase
{
    #region Variables
    [Header("Actions")]
    [SerializeField] private UnityEvent callback;
    [SerializeField] private UnityEvent<T> callbackType;
    #endregion
    #region Methods
    public bool Check(in Collider collider, Action<T> _callback)
    {
        if (collider.TryGetComponent(out T c)) //Ineficiente //FIXME
        {
            //FIXME GC Alloc, cuidar que sean constantes
            callback?.Invoke();
            _callback(c);
            callbackType?.Invoke(c);
            return true;
        }
        return false;
    }
    #endregion
}
