using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeGame : MonoBehaviour
{
    public static ArrangeGame instance = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private Transform listHeroAv = null;
    [SerializeField]
    private GameObject heroAvEl = null;

    [SerializeField]
    private Animator arrange = null;
    [SerializeField]
    private C_CellAT[] teamL = null;
    [SerializeField]
    private C_CellAT[] teamR = null;

    [SerializeField]
    private Button btnSave = null;
    [SerializeField]
    private Button btnFighting = null;

    private List<M_Character> characters = new List<M_Character>();
    public int countActive = 0;

    public Dictionary<int, C_CharacterAvEl> Objs = new Dictionary<int, C_CharacterAvEl>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        btnFighting.gameObject.SetActive(GameManager.instance.isAttack);
        btnSave.gameObject.SetActive(!GameManager.instance.isAttack);

        Timing.RunCoroutine(_LoadListHero());

        if(GameManager.instance.isAttack) Timing.RunCoroutine(_LoadListCreep());

        LoadAnim(true);
    }

    private void LoadAnim(bool v)
    {
        arrange.SetBool("isArrange", v);

        for (int i = 0; i < teamL.Length; i++) teamL[i].GetComponent<Animator>().SetBool("isArrange", v);
        for (int i = 0; i < teamR.Length; i++) teamR[i].GetComponent<Animator>().SetBool("isArrange", v);
    }

    private IEnumerator<float> _LoadListHero()
    {
        Objs.Clear();

        foreach (Transform child in listHeroAv)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            M_Character character = new M_Character(GameManager.instance.characters[i]);
            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;
            character.team = 0;

            C_CharacterAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_CharacterAvEl>();
            heroAv.set(character);

            characters.Add(character);

            if (character.idx != -1)
            {
                if (countActive >= C_Params.maxActive) break;

                heroAv.Active();                

                teamL[character.idx].set(character, canvas);

                countActive++;
            }

            Objs.Add(character.id, heroAv);
        }

        Debug.Log("Count: " + Objs.Keys.Count);

        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<float> _LoadListCreep()
    {
        List<M_Character> creeps = GameManager.instance.milestonesDic[GameManager.instance.idMilestone].listCreep;

        for (int i = 0; i < creeps.Count; i++)
        {
            if (creeps[i].idx != -1)
            {
                M_Character character = creeps[i];
                character.Current_ep = character.max_ep = 100;
                character.Current_hp = character.max_hp = character.hp;
                character.team = 1;

                teamR[creeps[i].idx].set(character, canvas, false);
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    public void Active(M_Character character)
    {
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.character.idx == -1)
            {
                character.idx = i;
                teamL[i].set(character, canvas);
                countActive++;

                return;
            }
        }
    }

    public void Fighting()
    {
        iSave();
    }

    public void Save()
    {
        iSave();            
    }

    private void iSave()
    {
        List<M_Character> characters = new List<M_Character>();

        // Update index hero in teamL
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.character.idx != -1)
            {
                teamL[i].content.character.idx = i;

                characters.Add(teamL[i].content.character);
            }
        }

        // Update index hero in list
        foreach (C_CharacterAvEl el in Objs.Values)
        {
            if (!el.isActive)
            {
                el.character.idx = -1;

                characters.Add(el.character);
            }
        }

        //heros.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.idx));        
        
        GameManager.instance.characters = characters;

        RequestCharacter.Arrange(characters);
    }

    public IEnumerator<float> _RecArrange()
    {
        LoadAnim(false);

        yield return Timing.WaitForSeconds(1f);
        if (GameManager.instance.isAttack) PlayGame.instance.ShowScene(true);
        else ScenesManager.instance.ChangeScene("MainGame");
    }
}
