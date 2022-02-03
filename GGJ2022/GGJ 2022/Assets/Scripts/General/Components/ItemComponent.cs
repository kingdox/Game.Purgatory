#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
# endregion
[RequireComponent(typeof(Collider))]
public sealed class ItemComponent : ComponentBase
{
    #region Variables
    [Header("Controllers")]
    [Space]
    [SerializeField] private StatsController ctrl_stat;

    [Header("Targets Y")]
    [Space]
    [SerializeField] private RotationController ctrl_rotation_y;

    [Header("Rendering")]
    [Space]
    [SerializeField] private ColorTypeController ctrl_colorType;
    [SerializeField] private ColorTController<ParticleSystem> ctrl_colorT_particle = default;


    public TypeData Type => ctrl_stat.Type;
    #endregion
    #region Events
    private void Start(){
        ctrl_stat.Type = ctrl_stat.Type;
    }
    private void Update()
    {
        ctrl_rotation_y.Rotate();
    }
    #endregion
    #region Methods
    protected override void Suscribe(bool condition)
    {
        if (condition)
        {
            ctrl_stat.OnTypeChange += ctrl_colorType.CheckColor;
        }
        else
        {
            ctrl_stat.OnTypeChange -= ctrl_colorType.CheckColor;
        }
    }

    public void Contact(StatsController stat){
        bool isSameType = stat.Type.Equals(ctrl_stat.Type);
        if (isSameType){
            //Recuperamos Munición
            Vector2Int changeAmmo = stat.Ammo;
            changeAmmo.x = changeAmmo.x + ctrl_stat.Ammo.x;
            stat.Ammo = changeAmmo;

            ////Recuperamos vida
            //stat.ChangeLife(ctrl_stat.Life.x);
        }
        else
        {
            //Cambiamos de color
            stat.Type = ctrl_stat.Type;
        }

        Destroy(gameObject);
    }
    public void ChangeType(TypeData t)
    {
        ctrl_stat.Type = t;
        ctrl_colorT_particle.ChangeParticle(t);
    }
    #endregion
}
