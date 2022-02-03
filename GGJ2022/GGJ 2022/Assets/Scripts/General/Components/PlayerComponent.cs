#region
using UnityEngine;
using UnityEngine.Events;
using XavHelpTo;
# endregion

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public sealed class PlayerComponent : ComponentBase
{
    #region Variables
    [Header("Data")]
    [Space]
    [SerializeField] private StatsController ctrl_stats = default;

    [Header("Actions")]
    [Space]
    [SerializeField] private CommandsController ctrl_commands = default;
    [SerializeField] private PhysicController ctrl_physic = default;
    [SerializeField] private MouseRotationController ctrl_mouseRotation = default;
    [SerializeField] private WeaponController ctrl_weapon = default;

    [Header("UI & Effects")]
    [Space]
    [SerializeField] private PlayerUIController ctrl_ui = default;

    [Header("Contacts")]
    [Space]
    [SerializeField] private ContactTypeController<BulletComponent> ctrl_contact_bullet = default;
    [SerializeField] private ContactTypeController<ItemComponent> ctrl_contact_item = default;

    [Header("Rendering")]
    [Space]
    [SerializeField] private ColorTypeController ctrl_colorType = default;
    [SerializeField] private BackgroundCameraTypeController ctrl_bg = default;
    [SerializeField] private ColorTController<TrailRenderer> ctrl_colorT_trail = default;
    [SerializeField] private ColorTController<ParticleSystem> ctrl_colorT_particle = default;

    [Space]
    [SerializeField] private UnityEvent OnDead;

    #endregion
    #region Events
    private void Awake()
    {
        
    }
    private void Start()
    {
        ctrl_stats.Type = ctrl_stats.Type;
    }
    private void Update(){
        ctrl_commands.CheckUpdate();
        ctrl_mouseRotation.Rotate();
    }
    private void FixedUpdate()
    {
        ctrl_commands.CheckFixedUpdate();
    }
    private void OnTriggerEnter(Collider other){
        ctrl_contact_bullet.Check(other,b => b.TakeDamageByContact(ctrl_stats));
        ctrl_contact_item.Check(other,i => i.Contact(ctrl_stats));
    }
    #endregion
    #region Methods
    protected override void Suscribe(bool condition)
    {
        if (condition)
        {
            ctrl_stats.OnTypeChange += ctrl_colorType.CheckColor;
            ctrl_stats.OnTypeChange += CheckColor;
            ctrl_stats.OnTypeChange += ctrl_bg.CheckBackground;
            ctrl_stats.OnChangeAmmo += ctrl_ui.ChangeOnAmmo;
            ctrl_stats.OnLife += ctrl_ui.ChangeOnLife;
            ctrl_stats.OnDead += Dead;
            ctrl_stats.OnDead += ctrl_ui.ChangeOnDead;
            
        }
        else
        {
            ctrl_stats.OnTypeChange -= CheckColor;
            ctrl_stats.OnTypeChange -= ctrl_colorType.CheckColor;
            ctrl_stats.OnTypeChange -= ctrl_bg.CheckBackground;
            ctrl_stats.OnChangeAmmo -= ctrl_ui.ChangeOnAmmo;
            ctrl_stats.OnLife -= ctrl_ui.ChangeOnLife;
            ctrl_stats.OnDead -= Dead;
            ctrl_stats.OnDead -= ctrl_ui.ChangeOnDead;
        }
    }

    public void Up() => ctrl_physic.MoveXZ(ctrl_stats.SpeedMovement, 'z');
    public void Down() => ctrl_physic.MoveXZ(-ctrl_stats.SpeedMovement, 'z');
    public void Right() => ctrl_physic.MoveXZ(ctrl_stats.SpeedMovement, 'x');
    public void Left() => ctrl_physic.MoveXZ(-ctrl_stats.SpeedMovement, 'x');
    public void Attack()
    {
        if (ctrl_stats.TryShot()){
            ctrl_weapon.Shot(ctrl_stats);
        }
        else
        {
            "No Quedan balas :(".Print("red");
        }
        
    }
    public void Dead()
    {
        "You're Dead".Print("red");
        enabled = false;
        OnDead.Invoke();
    }
    private void CheckColor(TypeData t)
    {
        ctrl_colorT_trail.ChangeTrail(t);
        ctrl_colorT_particle.ChangeParticle(t);
    }
    #endregion
}
