#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using XavHelpTo;
using XavHelpTo.Get;
# endregion

public class GameManager : MonoBehaviour
{
    #region Variable
    [Header("Targets Y")]
    [Space]
    [SerializeField] private RotationController ctrl_rotation_y;

    [Header("Targets -Y")]
    [Space]
    [SerializeField] private RotationController ctrl_rotation_y_minus;

    [Header("Item Spawn")]
    [Space]
    [SerializeField] private GeneratorController<ItemComponent> ctrl_generator_items;
    [SerializeField] private TimerController ctrl_timer_generator;

    [Header("Map Changes")]
    [Space]
    [SerializeField] private MeshTypeController ctrl_meshType_map;

    [SerializeField] private TargetComponent[] targets; 
    [SerializeField] private UnityEvent OnGameOver;

    #endregion
    #region Event
    private void Awake() {
        Time.timeScale = 1;
    }
    private void Start()
    {
        AudioSystem.PlayMusic(GeneralMusic.GAME_1);
    }
    private void Update() {
        ctrl_rotation_y.Rotate();
        ctrl_rotation_y_minus.Rotate();
        if (ctrl_timer_generator.Timer()) HandleGeneratedItem(ctrl_generator_items.Generate());
    }
    #endregion
    #region Methods
    public void HandleGeneratedItem(ItemComponent item) => item.ChangeType((TypeData)Supply.Lenght<TypeData>().ZeroMax());

    public void CheckWorld(ItemComponent item) {
        TypeData typeData = item.Type;
        if (ctrl_meshType_map.Check(typeData)) {
            "Mapa cambiado".Print("green");
        }
    }
    //public void Resume() => Time.timeScale = 1;
    public void GameOver()
    {
        Time.timeScale = 0;
        OnGameOver.Invoke();
    }
    public void Restart() => SceneManager.LoadScene("Game");
    public void GoToMenu() => SceneManager.LoadScene("Menu");

    public void CheckTargets(){
        "Target Muerto".Print("red");
        int max = targets.Length;
        for (int i = 0; i < max; i++){
            if (targets[i] != null) return;
        }

        GameOver();
    }
    #endregion
}
