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
    private C_LstCharacterAvEl lstCharacter = null;

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

    public int countActive = 0;

    public Dictionary<int, M_Character> characterDic = new Dictionary<int, M_Character>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        btnFighting.gameObject.SetActive(GameManager.instance.isAttack);
        btnSave.gameObject.SetActive(!GameManager.instance.isAttack);

        LoadListHero();

        if(GameManager.instance.isAttack)
        {
            switch (GameManager.instance.battleType)
            {
                case C_Enum.BattleType.None:
                    break;
                case C_Enum.BattleType.Campain:
                    LoadListCreep();
                    break;
                case C_Enum.BattleType.BossGuild:
                    LoadListBoss();
                    break;
                default:
                    break;
            }
        }

        LoadAnim(true);
    }

    private void LoadAnim(bool v)
    {
        arrange.SetBool("isArrange", v);

        for (int i = 0; i < teamL.Length; i++) teamL[i].GetComponent<Animator>().SetBool("isArrange", v);
        for (int i = 0; i < teamR.Length; i++) teamR[i].GetComponent<Animator>().SetBool("isArrange", v);
    }

    private void LoadListHero()
    {
        countActive = 0;
        characterDic = new Dictionary<int, M_Character>();
        List<M_Character> characters = new List<M_Character>();
        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            M_Character character = new M_Character(GameManager.instance.characters[i]);
            character.Current_ep = character.max_ep = 100;
            character.Current_hp = character.max_hp = character.hp;
            character.team = 0;

            characters.Add(character);
            characterDic.Add(character.id, character);

            if(character.idx != -1)
            {
                teamL[character.idx].set(character, canvas, true);
                countActive++;
            }
        }
        lstCharacter.set(characters);
    }

    private void LoadListCreep()
    {
        List<M_Character> creeps = GameManager.instance.milestone.lstCharacter;

        for (int i = 0; i < creeps.Count; i++)
        {
            if (creeps[i].idx != -1)
            {
                M_Character character = new M_Character(new M_Character(creeps[i]));
                character.Current_ep = character.max_ep = 100;
                character.Current_hp = character.max_hp = character.hp;
                character.team = 1;

                teamR[creeps[i].idx].set(character, canvas, false);
            }
        }
    }

    private void LoadListBoss()
    {
        List<M_Character> bosses = GameManager.instance.milestone.lstCharacter;

        for (int i = 0; i < bosses.Count; i++)
        {
            if (bosses[i].idx != -1)
            {
                M_Character character = new M_Character(bosses[i]);
                character.Current_ep = character.max_ep = 100;
                character.team = 1;

                teamR[bosses[i].idx].set(character, canvas, false);
            }
        }
    }

    public void Active(int id)
    {
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.character.idx == -1)
            {
                characterDic[id].idx = i;
                teamL[i].set(characterDic[id], canvas);
                countActive++;

                return;
            }
        }
    }

    public void UnActive(int id)
    {
        characterDic[id].idx = -1;
        lstCharacter.UnActive(id);
        countActive--;
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
        foreach (M_Character item in characterDic.Values) characters.Add(item);
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
