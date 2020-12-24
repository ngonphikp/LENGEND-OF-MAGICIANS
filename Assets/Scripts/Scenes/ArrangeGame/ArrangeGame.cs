using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private GameObject btnSave = null;
    [SerializeField]
    private GameObject btnFighting = null;
    [SerializeField]
    private GameObject btnLock = null;
    [SerializeField]
    private GameObject pnlLock = null;
    [SerializeField]
    private Text txtTime = null;

    public int countActive = 0;

    public Dictionary<int, M_Character> characterDic = new Dictionary<int, M_Character>();
    private bool isLock = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        C_Util.ActiveGO(false, pnlLock);        
        if (GameManager.instance.isAttack)
        {
            C_Util.ActiveGO(false, btnSave);
            switch (GameManager.instance.battleType)
            {
                case C_Enum.BattleType.PVP:
                    LoadListHero(true);
                    isLock = false;
                    C_Util.ActiveGO(false, btnFighting);
                    C_Util.ActiveGO(true, btnLock);
                    Timing.RunCoroutine(_CDT());
                    LoadListCreep();
                    break;
                case C_Enum.BattleType.BossGuild:
                    LoadListHero();
                    C_Util.ActiveGO(true, btnFighting);
                    C_Util.ActiveGO(false, btnLock);
                    LoadListBoss();
                    break;
                case C_Enum.BattleType.Duel:
                case C_Enum.BattleType.Campaign:
                default:
                    LoadListHero();
                    C_Util.ActiveGO(true, btnFighting);
                    C_Util.ActiveGO(false, btnLock);
                    LoadListCreep();
                    break;
            }
        }
        else
        {
            LoadListHero();
            C_Util.ActiveGO(true, btnSave);
        }

        LoadAnim(true);
        
    }

    private IEnumerator<float> _CDT()
    {
        int t = 15;
        txtTime.gameObject.SetActive(true);
        while (true)
        {
            if (t <= 0) break;
            txtTime.text = t + "";
            yield return Timing.WaitForSeconds(1);
            t -= 1;
        }
        txtTime.gameObject.SetActive(false);
        Lock();
    }

    private void LoadAnim(bool v)
    {
        arrange.SetBool("isArrange", v);

        for (int i = 0; i < teamL.Length; i++) teamL[i].GetComponent<Animator>().SetBool("isArrange", v);
        for (int i = 0; i < teamR.Length; i++) teamR[i].GetComponent<Animator>().SetBool("isArrange", v);
    }

    private void LoadListHero(bool isSelec = false)
    {
        countActive = 0;
        characterDic = new Dictionary<int, M_Character>();
        List<M_Character> characters = new List<M_Character>();
        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            M_Character character = new M_Character(GameManager.instance.characters[i]);
            if (isSelec && !character.isSelec) continue;
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
            M_Character character = new M_Character(new M_Character(creeps[i]));
            character.current_ep = character.max_ep = 100;
            character.current_hp = character.max_hp;
            character.team = 1;

            if (creeps[i].idx != -1)
            {
                teamR[creeps[i].idx].set(character, canvas, false);                
            }

            if(GameManager.instance.battleType == C_Enum.BattleType.PVP) characterDic.Add(character.id, character);
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
                character.current_ep = character.max_ep = 100;
                character.team = 1;

                teamR[bosses[i].idx].set(character, canvas, false);
            }
        }
    }

    public void Change(int to, int from)
    {
        Debug.Log(to + " <=> " + from);
        characterDic[teamL[to].content.character.id].idx = from;
        if(teamL[from].content.character.idx != -1)
        {
            characterDic[teamL[from].content.character.id].idx = to;
        }
        if (GameManager.instance.battleType == C_Enum.BattleType.PVP) RequestGame.Change(to, from);
    }

    public void RecChange(int to, int from)
    {
        M_Character charTo = teamR[to].content.character;
        M_Character charFrom = teamR[from].content.character;

        characterDic[charTo.id].idx = from;
        teamR[from].content.ReLoad(characterDic[charTo.id], true);

        if(charFrom.idx != -1)
        {
            characterDic[charFrom.id].idx = to;
            teamR[to].content.ReLoad(characterDic[charFrom.id], true);
        }
        else
        {
            teamR[to].content.ReLoad(new M_Character());
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

                if (GameManager.instance.battleType == C_Enum.BattleType.PVP) RequestGame.Active(id, i);

                return;
            }
        }
    }

    public void RecActive(int id, int idx)
    {
        characterDic[id].idx = idx;
        teamR[idx].set(characterDic[id], canvas);
    }

    public void UnActive(int id)
    {
        characterDic[id].idx = -1;
        lstCharacter.UnActive(id);
        countActive--;

        if (GameManager.instance.battleType == C_Enum.BattleType.PVP) RequestGame.UnActive(id);
    }

    public void RecUnActive(int id)
    {
        teamR[characterDic[id].idx].content.ReLoad(new M_Character());

        characterDic[id].idx = -1;        
    }

    public void Back()
    {
        Timing.KillCoroutines();
        switch (GameManager.instance.battleType)
        {
            case C_Enum.BattleType.PVP:
                RequestGame.OutRoomGame();
                break;
            case C_Enum.BattleType.BossGuild:
            case C_Enum.BattleType.Duel:
            case C_Enum.BattleType.Campaign:
            default:
                SceneManager.LoadSceneAsync("MainGame");
                break;
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

    public void Lock()
    {
        if (!isLock)
        {
            isLock = true;
            C_Util.ActiveGO(true, pnlLock);
            List<M_Character> characters = new List<M_Character>();
            for (int i = 0; i < teamL.Length; i++)
                if (teamL[i].content.character.idx != -1) characters.Add(teamL[i].content.character);

            GameManager.instance.lockChars = characters;
            RequestGame.Lock(characters);
        }
    }

    private void iSave()
    {
        Timing.KillCoroutines();
        List<M_Character> characters = new List<M_Character>();
        foreach (M_Character item in characterDic.Values) if(item.team == 0) characters.Add(item);
        GameManager.instance.characters = characters;
        RequestCharacter.Arrange(characters);
    }

    public IEnumerator<float> _RecArrange()
    {
        LoadAnim(false);
        yield return Timing.WaitForSeconds(1f);
        if (GameManager.instance.isAttack) PlayGame.instance.ShowScene(true);
        else SceneManager.LoadSceneAsync("MainGame");
    }

    public void RecLock(List<M_Character> lstCharacter)
    {
        GameManager.instance.milestone.lstCharacter = lstCharacter;
    }

    public IEnumerator<float> _StartGame()
    {
        LoadAnim(false);
        yield return Timing.WaitForSeconds(1f);
        if (GameManager.instance.isAttack) PlayGame.instance.ShowScene(true);
        else SceneManager.LoadSceneAsync("MainGame");
    }
}
