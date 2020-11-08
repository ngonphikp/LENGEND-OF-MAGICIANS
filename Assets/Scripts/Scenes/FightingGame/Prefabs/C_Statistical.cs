using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Statistical : MonoBehaviour
{
    [SerializeField]
    private Image imgAv = null;
    [SerializeField]
    private Image imgFr = null;
    [SerializeField]
    private Image imgBg = null;
    [SerializeField]
    private Image imgDame = null;
    [SerializeField]
    private Image imgTank = null;
    [SerializeField]
    private Text txtDame = null;
    [SerializeField]
    private Text txtTank = null;

    public void set(M_Character character, float perDame, float perTank)
    {
        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + character.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + character.star);

        Sprite sprite = Resources.Load<Sprite>("Sprites/Avatar/" + character.id_cfg);

        if (sprite != null) imgAv.sprite = sprite;

        imgDame.fillAmount = perDame;
        txtDame.text = Math.Round(perDame * 100, 2) + " %";

        imgTank.fillAmount = perTank;
        txtTank.text = Math.Round(perDame * 100, 2) + " %";
    }
}
