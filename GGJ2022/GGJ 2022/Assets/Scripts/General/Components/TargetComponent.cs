#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XavHelpTo;
#endregion

public class TargetComponent : ComponentBase
{
    #region Variable
    [Header("Controls")]
    [Space]
    [SerializeField] private StatsController ctrl_stat = default;
    [SerializeField] private TimerController ctrl_timer = default;
    [SerializeField] private WeaponController ctrl_weapon = default;


    [Header("Contacts")]
    [Space]
    [SerializeField] private ContactTypeController<BulletComponent> ctrl_contactType = default;


    [Header("Rendering")]
    [Space]
    [SerializeField] private MeshTypeController ctrl_meshType_body;
    [SerializeField] private ColorTController<TrailRenderer> ctrl_colorT_trail = default;
    [SerializeField] private ColorTController<ParticleSystem> ctrl_colorT_particle = default;
    [Space]
    [SerializeField] private UnityEvent OnDead;
    #endregion
    #region Event
    private void Start()
    {
        ctrl_stat.Type = ctrl_stat.Type;
    }
    private void OnTriggerEnter(Collider other){
        ctrl_contactType.Check(other,b=>b.TakeDamageByContact(ctrl_stat));
    }
    private void Update(){
        CheckShot();
    }
    #endregion
    #region Method
    protected override void Suscribe(bool condition)
    {
        if (condition)
        {
            ctrl_stat.OnTypeChange += CheckColor;
            ctrl_stat.OnDead += Dead;
        }
        else
        {
            ctrl_stat.OnTypeChange -= CheckColor;
            ctrl_stat.OnDead -= Dead;
        }
    }
    public void CheckShot(){
        if (!ctrl_timer.Timer()) return;
        ctrl_weapon.Shot(ctrl_stat);
    }
    public void Dead()
    {
        OnDead.Invoke();
        Destroy(gameObject);
    }
    private void CheckColor(TypeData t)
    {
        ctrl_meshType_body.Check(t);
        ctrl_colorT_trail.ChangeTrail(t);
        ctrl_colorT_particle.ChangeParticle(t);
    }
    #endregion
}
