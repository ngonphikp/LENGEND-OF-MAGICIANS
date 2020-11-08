using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Avatar : MonoBehaviour
{
    [SerializeField]
    private Image imgBg = null;
    [SerializeField]
    private Image imgFr = null;
    [SerializeField]
    private Image imgAv = null;
    [SerializeField]
    private Image imgEl = null;
    [SerializeField]
    private Text txtLv = null;

    public void set(M_Character character)
    {
        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + character.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + character.star);

        Sprite sprite = Resources.Load<Sprite>("Sprites/Avatar/" + character.id_cfg);

        if (sprite != null) imgAv.sprite = sprite;

        imgEl.sprite = Resources.Load<Sprite>("Sprites/Element/" + character.element);
        txtLv.text = character.lv + "";
    }
}
