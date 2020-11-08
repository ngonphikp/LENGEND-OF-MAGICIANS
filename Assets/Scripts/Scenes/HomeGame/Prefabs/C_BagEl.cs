using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_BagEl : MonoBehaviour
{
    [SerializeField]
    private Image imgAv = null;
    [SerializeField]
    private Image imgFr = null;
    [SerializeField]
    private Image imgBg = null;
    [SerializeField]
    private Text txtTen = null;
    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtEl = null;
    [SerializeField]
    private Text txtStar = null;

    [SerializeField]
    private GameObject active = null;

    private int idx = 0;

    public IEnumerator<float> _set(int idx)
    {
        this.idx = idx;
        M_Character character = new M_Character(GameManager.instance.characters[idx]);

        imgBg.sprite = Resources.Load<Sprite>("Sprites/Avatar/BG" + character.star);
        imgFr.sprite = Resources.Load<Sprite>("Sprites/Avatar/Frame" + character.star);

        Sprite sprite = Resources.Load<Sprite>("Sprites/Avatar/" + character.id_cfg);

        if (sprite != null) imgAv.sprite = sprite;
        
        txtTen.text = character.name + "";
        txtLv.text = character.lv + "";
        txtStar.text = character.star + "";
        txtEl.text = C_Params.Element[character.element];

        active.SetActive(character.idx != -1);

        yield return Timing.WaitForOneFrame;
    }

    public void Click()
    {
        GameManager.instance.idxCharacter = idx;
        MainGame.instance.ShowScene("InforScene");
    }
}
