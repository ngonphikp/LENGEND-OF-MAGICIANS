using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Profile : MonoBehaviour
{
    [SerializeField]
    private Image imgEl = null;
    [SerializeField]
    private Text txtName = null;

    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtHp = null;
    [SerializeField]
    private Text txtAtk = null;
    [SerializeField]
    private Text txtDef = null;

    [SerializeField]
    private Text txtHpUp = null;
    [SerializeField]
    private Text txtAtkUp = null;
    [SerializeField]
    private Text txtDefUp = null;

    public void set(M_Character character)
    {
        imgEl.sprite = Resources.Load<Sprite>("Sprites/Element/" + character.element);
        txtName.text = character.name + "";

        txtLv.text = character.lv + "";
        txtHp.text = character.hp + "";
        txtAtk.text = character.atk + "";
        txtDef.text = character.def + "";

        txtHpUp.text = " + " + Mathf.RoundToInt(character.hp * (C_Params.coeUpLv - 1));
        txtAtkUp.text = " + " + Mathf.RoundToInt(character.atk * (C_Params.coeUpLv - 1));
        txtDefUp.text = " + " + Mathf.RoundToInt(character.def * (C_Params.coeUpLv - 1));
    }
}
