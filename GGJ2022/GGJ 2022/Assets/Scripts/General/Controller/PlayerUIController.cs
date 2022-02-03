using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo.Set;
using XavHelpTo.Change;
using XavHelpTo;
using XavHelpTo.Know;

[Serializable]
public class PlayerUIController
{
    [Header("Requirements")]
    [Space]
    [SerializeField] private Text text_ammo;
    [SerializeField] private RectTransform rectTr_life;
    [SerializeField] private Image img_life;


    public void ChangeOnAmmo(Vector2Int _ammo)
    {
        text_ammo.text = _ammo.x.ToString();
        text_ammo.color = _ammo.x.Equals(0)
            ? Color.red
            : Color.white
        ;
    }

    public void ChangeOnLife(Vector2Int life){
        Vector2 _life = life;
        float percent = _life.x.PercentOf(_life.y,true);
        rectTr_life.anchorMax = (Vector2.right * percent) + Vector2.up;
        img_life.color = Color.Lerp(Color.red, Color.white, percent);
    }

    public void ChangeOnDead()
    {
        text_ammo.text = "!";
        text_ammo.color = Color.red;
        rectTr_life.gameObject.SetActive(false);

    }

}
