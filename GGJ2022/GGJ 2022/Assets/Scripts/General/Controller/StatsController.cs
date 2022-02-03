using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;

[Serializable]
public class StatsController 
{
    #region Variables
    private bool isDead = false;
    [SerializeField] private Vector2Int _life = default;
    [SerializeField] private float _speedMovement = default;
    [SerializeField] private Vector2Int _ammo = default;
    [SerializeField] private int _speedBullet = default;
    [SerializeField] private int _damage = default;
    [SerializeField] private TypeData _type = default;

    public int Damage { get => _damage; }
    public float SpeedMovement { get => _speedMovement; }
    public float SpeedBullet { get => _speedBullet; }

    public Vector2Int Life { get => _life;}

    public TypeData Type { get => _type; set {
            _type = value;
            OnTypeChange?.Invoke(_type);
    } }
    public Vector2Int Ammo { get => _ammo; set
        {
            _ammo.y = value.y;
            _ammo.x = value.x.Min(0).Max(_ammo.y);
            OnChangeAmmo?.Invoke(_ammo);
        }
    }


    public Action<TypeData> OnTypeChange;
    public Action<Vector2Int>OnChangeAmmo;
    public Action<Vector2Int>OnLife;
    public Action OnDead;
    #endregion
    #region Methods
    public bool TryShot()
    {
        bool canShot = _ammo.x > 0;
        Vector2Int newAmmo = Ammo;
        newAmmo.x--;
        Ammo = newAmmo;
        return canShot;
    }
    public bool ChangeLife(int amount){
        if (isDead) return true;
        _life.x = (_life.x + amount).Min(0).Max(_life.y);
        isDead = _life.x.Equals(0);
        OnLife?.Invoke(_life);
        if (isDead) OnDead?.Invoke();
        return isDead;
    }
    #endregion

}
