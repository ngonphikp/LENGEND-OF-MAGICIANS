using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionGame : MonoBehaviour
{
    public static SelectionGame instance = null;

    [Header("Vào Game")]
    [SerializeField]
    private InputField ipfTenNhanVat = null;
    [SerializeField]
    private Text txtNoti = null;

    [Header("Chọn Magician")]
    [SerializeField]
    private string[] idHeros = new string[5];
    [SerializeField]
    private Transform posMagician = null;
    [SerializeField]
    private Image[] imgSkills = null;

    private int idxActive = 0;
    private C_Character hero = null;
    private string nameAc = "";

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    private void Start()
    {
        LoadHero(idHeros[idxActive]);
    }

    private void LoadHero(string id)
    {
        foreach (Transform child in posMagician)
        {
            Destroy(child.gameObject);
        }
        GameObject heroAs = Resources.Load("Prefabs/Character/" + id, typeof(GameObject)) as GameObject;

        if (heroAs != null)
        {
            GameObject heroObj = Instantiate(heroAs, posMagician);
            hero = heroObj.GetComponent<C_Character>();
            M_Character character = new M_Character();

            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;
            character.id_cfg = id;
            character.UpdateById();
            character.lv = 1;
            hero.Set(character);
        }

        for(int i = 0; i < GameManager.instance.herosDic[id].skills.Count; i++)
        {
            imgSkills[i].sprite = Resources.Load<Sprite>("Sprites/Skill/" + GameManager.instance.herosDic[id].skills[i]);
        }
    }


    public void VaoGame()
    {
        nameAc = ipfTenNhanVat.text;
        txtNoti.text = "Vào game thành công!";

        RequestAccount.Selection(nameAc, idHeros[idxActive]);
    }

    public void RecSelection(List<M_Character> lstCharacters)
    {
        if (lstCharacters.Count > 0)
        {
            GameManager.instance.characters = lstCharacters;

            GameManager.instance.account.name = nameAc;

            MainGame.instance.ShowScene("HomeScene");
        }
    }

    public void ChangeHero(int idx)
    {
        if (idx == idxActive) return;
        idxActive = idx;
        LoadHero(idHeros[idxActive]);
    }

    public void SkillHero(int anim)
    {
        hero.Play(anim);
    }
}
