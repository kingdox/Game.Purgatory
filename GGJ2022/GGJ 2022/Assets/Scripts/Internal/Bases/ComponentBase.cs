using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentBase : MonoBehaviour
{
    #region Variables

    #endregion
    #region Events
    protected virtual void OnEnable() {
        Suscribe(true);
    }
    protected virtual void OnDisable() {
        Suscribe(false);
    }
    #endregion
    #region Methods
    protected abstract void Suscribe(bool condition);
    #endregion
}
