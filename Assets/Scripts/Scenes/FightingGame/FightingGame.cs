using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MEC;

public class FightingGame : MonoBehaviour
{
    public static FightingGame instance = null;

    [Header("Posstion")]
    [SerializeField]
    private Transform[] posTeamL = null;
    [SerializeField]
    private Transform[] posTeamR = null;

    [Header("Data")]
    [SerializeField]
    private List<M_Character> dataTeamL = new List<M_Character>();
    [SerializeField]
    private List<M_Character> dataTeamR = new List<M_Character>();    

    [Header("Turn")]
    [SerializeField]
    private Text txtTime = null;
    [SerializeField]
    private Text txtTurn = null;

    [Header("Time Scale")]
    [SerializeField]
    private Text txtTimeScale = null;

    public float myTimeScale = 1.0f;
    [SerializeField]
    public float[] arrTimeScale = { 1, 2, 4 };

    [Header("End Game")]
    [SerializeField]
    private GameObject popupEndGame = null;
    [SerializeField]
    private Text txtResult = null;
    [SerializeField]
    private Image[] imgResultStars = new Image[3];
    [SerializeField]
    private Sprite spStar = null;
    [SerializeField]
    private AudioClip acFighting = null;
    [SerializeField]
    private AudioClip acEndGame = null;    

    private C_Enum.EndGame isEndGame = C_Enum.EndGame.NOT;
    private int starEndGame = 0;
    private int pointEndGame = 0;

    private List<C_Character> lstObj = new List<C_Character>();
    public Dictionary<int, C_Character> Objs = new Dictionary<int, C_Character>();

    // Data Combat
    public List<int> idTargets = new List<int>();
    public List<C_Character> targets = new List<C_Character>();
    public int Beaten = 0;

    private List<M_Action> actions = new List<M_Action>();
    private M_Milestone milestone = null;

    // Check Combat
    public bool Turn { get => turn; }
    private bool turn = true;
    private int check = 0;
    private bool getAction = false;

    private bool isSkip = false;

    // Statistical
    [SerializeField]
    private Dictionary<int, M_Statistical> statistical = new Dictionary<int, M_Statistical>();
    [SerializeField]
    private GameObject statisticalPrb = null;
    [SerializeField]
    private Transform lstStatisticalL = null;
    [SerializeField]
    private Transform lstStatisticalR = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }    

    [Obsolete]
    private void OnEnable()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Application.runInBackground = true;
        }

        if(SoundManager.instance) SoundManager.instance.PlayLoop(acFighting);

        LoadData();
        Scenario();
        Timing.RunCoroutine(_Combat());
    }

    // Load Data
    private void LoadData()
    {
        Debug.Log("LoadData");
        milestone = GameManager.instance.milestone;
        txtTurn.text = "0 / " + milestone.maxTurn;

        int count = 0;
        dataTeamL.Clear();
        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            if (GameManager.instance.characters[i].idx != -1)
            {
                M_Character character = new M_Character(GameManager.instance.characters[i]);
                character.team = 0;
                character.max_ep = 100;
                character.Current_ep = 0;

                character.id = count++;
                dataTeamL.Add(new M_Character(character));
            }
        }
        dataTeamR.Clear();
        for (int i = 0; i < milestone.lstCharacter.Count; i++)
        {
            M_Character character = new M_Character(milestone.lstCharacter[i]);
            character.team = 1;
            character.max_ep = 100;
            character.Current_ep = 0;

            character.id = count++;
            character.UpdateLevel();
            dataTeamR.Add(new M_Character(character));
        }

        Init();

        myTimeScale = arrTimeScale[GameManager.instance.IdxTimeScale];
        txtTimeScale.text = "X" + myTimeScale;
    }

    private void Init()
    {
        statistical.Clear();
        lstObj.ForEach(x => Destroy(x.gameObject));

        lstObj.Clear();
        Objs.Clear();

        isEndGame = C_Enum.EndGame.NOT;
        starEndGame = 0;
        pointEndGame = 0;

        idTargets.Clear();
        targets.Clear();
        Beaten = 0;

        actions.Clear();
        turn = true;
        check = 0;
        getAction = false;

        isSkip = false;

        InitTeam(dataTeamL, posTeamL);
        InitTeam(dataTeamR, posTeamR);
    }

    private void InitTeam(List<M_Character> datas, Transform[] poses)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            GameObject nvAs = Resources.Load("Prefabs/Character/" + datas[i].id_cfg, typeof(GameObject)) as GameObject;

            if (nvAs == null)
            {
                switch (datas[i].type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = Resources.Load("Prefabs/Character/M1000", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Boss:
                        nvAs = Resources.Load("Prefabs/Character/B1000", typeof(GameObject)) as GameObject;
                        break;
                    default:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                }
            }

            if (nvAs != null)
            {
                GameObject obj = Instantiate(nvAs, poses[datas[i].idx]);
                C_Character c_obj = obj.GetComponent<C_Character>();
                c_obj.Set(new M_Character(datas[i]));
                c_obj.isCombat = true;
                c_obj.character.isDie = false;

                Objs.Add(datas[i].id, c_obj);
                lstObj.Add(c_obj);

                statistical.Add(datas[i].id, new M_Statistical());
            }
        }
    }

    // Scenario

    [Obsolete]
    private void Scenario()
    {
        Debug.Log("Scenario");

        int turnS = 1;
        while (true)
        {
            if (CheckEndGame(turnS)) break;

            CountTurn(turnS++);

            Attack(dataTeamL, dataTeamR);

            Attack(dataTeamR, dataTeamL);
        }        
    }

    private bool CheckEndGame(int turn) {
        List<int> idxs = new List<int>();
        switch (GameManager.instance.battleType)
        {
            case C_Enum.BattleType.None:
                break;
            case C_Enum.BattleType.Campain:
                if (turn > milestone.maxTurn || FindTargetNotDie(dataTeamL).Count == 0)
                {
                    isEndGame = C_Enum.EndGame.LOSE;
                    starEndGame = 0;

                    Debug.Log("=========================== Scenario End Game: " + isEndGame.ToString() + ": " + starEndGame + " Star");

                    return true;
                }

                if (FindTargetNotDie(dataTeamR).Count == 0)
                {
                    isEndGame = C_Enum.EndGame.WIN;

                    bool isFull = true;
                    bool is3Star = true;
                    for (int j = 0; j < dataTeamL.Count; j++)
                    {
                        if (dataTeamL[j].isDie) isFull = false;
                        else
                        {
                            if (dataTeamL[j].Current_hp <= (dataTeamL[j].max_hp * 0.2)) is3Star = false;
                        }
                    }

                    // Nếu còn đủ team
                    if (isFull)
                    {
                        starEndGame = (is3Star) ? 3 : 2;
                    }
                    else starEndGame = 1;

                    Debug.Log("=========================== Scenario End Game: " + isEndGame.ToString() + ": " + starEndGame + " Star");

                    return true;
                }
                break;
            case C_Enum.BattleType.BossGuild:
                if (FindTargetNotDie(dataTeamL).Count == 0 || FindTargetNotDie(dataTeamR).Count == 0 || turn >= milestone.maxTurn)
                {
                    isEndGame = C_Enum.EndGame.WIN;
                    starEndGame = 3;

                    foreach (var item in statistical)
                    {
                        if (Objs[item.Key].character.team == 0)
                        {
                            pointEndGame += item.Value.dame;
                        }
                    }

                    Debug.Log("=========================== Scenario End Game: " + isEndGame.ToString() + ": Point: " + pointEndGame);

                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    private void CountTurn(int turn)
    {
        M_Action action = new M_Action();
        action.type = C_Enum.ActionType.TURN;

        action.prop = new M_Prop();
        action.prop.turn = turn;

        actions.Add(action);
        C_Util.GetDumpObject(action);
    }

    [Obsolete]
    private void Attack(List<M_Character> TeamAttack, List<M_Character> TeamAttacked)
    {       
        for (int i = 0; i < TeamAttack.Count; i++)
        {
            M_Character actor = TeamAttack[i];

            if (actor.isDie) continue;

            int idSkill = (actor.Current_ep >= actor.max_ep) ? 5 : 3;

            int find = 1;

            List<int> idxs = FindTargetNotDie(TeamAttacked);
            if (idxs.Count > find)
            {                
                ShuffleArray(ref idxs);                
            }

            for (int j = 0; j < find && j < idxs.Count; j++)
            {
                List<M_Character> targets = new List<M_Character>();

                targets.Add(TeamAttacked[idxs[j]]);

                actions.Add(Action(actor, idSkill, targets));
            }
        }
    }

    [Obsolete]
    private void ShuffleArray(ref List<int> idxs)
    {
        for(int i = 0; i < idxs.Count - 1; i++)
        {
            int random = UnityEngine.Random.RandomRange(i, idxs.Count);
            Swap(ref idxs, i, random);
        }
    }

    private void Swap(ref List<int> idxs, int a, int b)
    {
        int temp = idxs[a];
        idxs[a] = idxs[b];
        idxs[b] = temp;
    }

    private List<int> FindTargetNotDie(List<M_Character> TeamAttacked)
    {
        List<int> rs = new List<int>();
        for (int i = 0; i < TeamAttacked.Count; i++)
        {
            if (!TeamAttacked[i].isDie) rs.Add(i);
        }
        return rs;
    }

    [Obsolete]
    private M_Action Action(M_Character actor, int idSkill, List<M_Character> targets)
    {
        M_Action action = new M_Action();

        action.idActor = actor.id;
        action.type = C_Enum.ActionType.SKILL;
        action.prop = new M_Prop();
        action.prop.idSkill = idSkill;

        action.prop.idTargets.Clear();
        for (int i = 0; i < targets.Count; i++)
        {
            action.prop.idTargets.Add(targets[i].id);
        }

        action.actions = new List<M_Action>();

        // Nếu dùng ulti
        if (idSkill == 5)
        {
            // Actor tăng -100 ep
            {
                M_Action actionC = new M_Action();
                actionC.idActor = actor.id;
                actionC.type = C_Enum.ActionType.CHANGE_EP;

                actionC.prop = new M_Prop();
                actionC.prop.epChange = -100;

                actor.Current_ep += -100;
                action.actions.Add(actionC);
            }
        }
        else
        {
            // Actor tăng 20 ep
            {
                M_Action actionC = new M_Action();
                actionC.idActor = actor.id;
                actionC.type = C_Enum.ActionType.CHANGE_EP;

                actionC.prop = new M_Prop();
                actionC.prop.epChange = 20;

                actor.Current_ep += 20;
                action.actions.Add(actionC);
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            // Tính dame mặc định
            int dame = ((actor.atk - targets[i].def) > 0) ? (actor.atk - targets[i].def) : 1;

            // Thêm hệ tương sinh tương khắc
            if (C_Params.SystemCorrelation[actor.element] == targets[i].element) dame = (int)(dame * C_Params.ratioSC);

            // Nếu dame có crit
            bool isCrit = (UnityEngine.Random.RandomRange(0.0f, 100.0f) <= actor.crit);
            if(isCrit) dame = (int)(dame * C_Params.ratioCrit);

            // Nếu target né dame
            bool isDodge = (UnityEngine.Random.RandomRange(0.0f, 100.0f) <= targets[i].dodge);

            if (isDodge)
            {
                // target né đòn
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id;
                    actionC.type = C_Enum.ActionType.DODGE;

                    action.actions.Add(actionC);
                }
            }
            else
            {
                // Nếu dame chết
                bool isDie = dame >= targets[i].Current_hp;
                if (isDie)
                {
                    // target chết
                    {
                        M_Action actionC = new M_Action();
                        actionC.idActor = targets[i].id;
                        actionC.type = C_Enum.ActionType.DIE;

                        targets[i].isDie = true;
                        action.actions.Add(actionC);
                    }
                }
                else
                {
                    // target trúng đòn
                    {
                        M_Action actionC = new M_Action();
                        actionC.idActor = targets[i].id;
                        actionC.type = C_Enum.ActionType.BEATEN;

                        action.actions.Add(actionC);
                    }

                    // target tăng 10 ep        
                    {
                        M_Action actionC = new M_Action();
                        actionC.idActor = targets[i].id;
                        actionC.type = C_Enum.ActionType.CHANGE_EP;

                        actionC.prop = new M_Prop();
                        actionC.prop.epChange = 10;

                        targets[i].Current_ep += 10;
                        action.actions.Add(actionC);
                    }
                }

                // target giảm hp
                {
                    M_Action actionC = new M_Action();
                    actionC.idActor = targets[i].id;
                    actionC.type = C_Enum.ActionType.CHANGE_HP;

                    actionC.prop = new M_Prop();

                    actionC.prop.hpChange = -dame;
                    actionC.prop.hpChangeCrit = isCrit;

                    int hp = targets[i].Current_hp;

                    targets[i].Current_hp += -dame;                    

                    action.actions.Add(actionC);

                    if (isDie) dame = hp;
                }

                // statistical
                {
                    statistical[actor.id].dame += (isDodge) ? 0 : dame;
                    statistical[targets[i].id].tank += (isDodge) ? 0 : dame;
                }
            }
        }

        C_Util.GetDumpObject(action);
        return action;
    }

    // Combat

    private IEnumerator<float> _Combat()
    {
        int t = 3;
        txtTime.gameObject.SetActive(true);
        while (true)
        {
            if (t <= 0 || isSkip) break;
            Debug.Log("Đếm ngược: " + t);
            txtTime.text = t + " ";
            yield return Timing.WaitForSeconds(1 / myTimeScale);
            t -= 1;
        }
        txtTime.gameObject.SetActive(false);

        Debug.Log("Combat");

        while (true)
        {
            if (isSkip)
            {
                CheckTurn();

                if (turn)
                {
                    EndGame();
                    break;
                }
            }

            if (!getAction && Beaten == 0)
            {
                CheckTurn();

                if (turn)
                {
                    M_Action action = actions[0];
                    getAction = true;

                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(_Play(action)));

                    actions.RemoveAt(0);
                    getAction = false;
                }

                if (actions.Count == 0)
                {
                    yield return Timing.WaitForSeconds(2 / myTimeScale);
                    EndGame();
                    isSkip = true;
                    break;
                }
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    public void SendEndGame()
    {
        switch (GameManager.instance.battleType)
        {
            case C_Enum.BattleType.None:
                break;
            case C_Enum.BattleType.Campain:
                RequestCampain.EndGame(milestone.id, starEndGame, true);
                break;
            case C_Enum.BattleType.BossGuild:
                RequestGuild.EndGameBoss(milestone.id, pointEndGame);
                break;
            default:
                break;
        }        
    }

    public void RecEndGame()
    {
        //GameManager.instance.tick_milestonesDic[milestone.id].star = Math.Max(GameManager.instance.tick_milestonesDic[milestone.id].star, starEndGame);

        //if(isEndGame == C_Enum.EndGame.WIN && GameManager.instance.milestone.id == GameManager.instance.tick_milestones.Count)
        //{
        //    GameManager.instance.tick_milestones.Add(new M_Tick_Milestone(milestone.id + 1, GameManager.instance.account.id, milestone.id + 1, 0));

        //    GameManager.instance.UpdateTickMS();

        //    SceneManager.LoadScene("MainGame");
        //}
        //else
        //{
        //    PlayGame.instance.ShowScene(false);
        //}

        SoundManager.instance.PlayLoop();
    }

    private void EndGame()
    {
        Timing.RunCoroutine(SoundManager.instance._PlayOneShotAs(acEndGame));

        popupEndGame.SetActive(true);
        txtResult.text = isEndGame.ToString();        

        for(int i = 0; i < starEndGame; i++)
        {
            imgResultStars[i].sprite = spStar;
        }

        int sumDameL = 0;
        int sumTankL = 0;
        int sumDameR = 0;
        int sumTankR = 0;

        foreach (var item in statistical)
        {           
            if(Objs[item.Key].character.team == 0)
            {
                sumDameL += item.Value.dame;
                sumTankL += item.Value.tank;
            }
            else if(Objs[item.Key].character.team == 1)
            {
                sumDameR += item.Value.dame;
                sumTankR += item.Value.tank;
            }
        }

        foreach (var item in statistical)
        {
            M_Character character = new M_Character(Objs[item.Key].character);

            if (character.team == 0)
            {
                float perDame = (sumDameL != 0) ? (item.Value.dame * 1.0f / sumDameL) : 0;
                float perTank = (sumTankL != 0) ? (item.Value.tank * 1.0f / sumTankL) : 0;
                Debug.Log(item.Key + ": " + perDame + " / " + perTank);

                C_Statistical c_Statistical = Instantiate(statisticalPrb, lstStatisticalL).GetComponent<C_Statistical>();
                c_Statistical.set(character, perDame, perTank);
            }
            else if (character.team == 1)
            {
                float perDame = (sumDameR != 0) ? (item.Value.dame * 1.0f / sumDameR) : 0;
                float perTank = (sumTankR != 0) ? (item.Value.tank * 1.0f / sumTankR) : 0;
                Debug.Log(item.Key + ": " + perDame + " / " + perTank);

                C_Statistical c_Statistical = Instantiate(statisticalPrb, lstStatisticalR).GetComponent<C_Statistical>();
                c_Statistical.set(character, perDame, perTank);
            }
        }
    }

    private void CheckTurn()
    {
        foreach (C_Character obj in lstObj)
        {
            if (obj != null && obj.IsPlay()) check++;
            else break;
        }

        turn = (check >= lstObj.Count);
        check = 0;
    }

    private IEnumerator<float> _Play(M_Action action)
    {
        int idActor = action.idActor;
        C_Character actor = Objs[idActor];

        switch (action.type)
        {
            case C_Enum.ActionType.TURN:
                Debug.Log("=========================== TURN: " + action.prop.turn);
                txtTurn.text = action.prop.turn + " / " + milestone.maxTurn;
                break;
            case C_Enum.ActionType.SKILL:
                // Debug.Log("=========================== SKILLING: " + idActor);
                Timing.RunCoroutine(_SKILLING(actor, action));
                break;
            case C_Enum.ActionType.CHANGE_HP:
                Debug.LogWarning("=========================== CHANGE_HP: " + idActor);
                actor.PushChangeHp(action.prop);
                actor.ChangeHp();
                break;
            case C_Enum.ActionType.CHANGE_EP:
                Debug.LogWarning("=========================== CHANGE_EP: " + idActor);
                actor.PushChangeEp(action.prop);
                actor.ChangeEp();
                break;
            case C_Enum.ActionType.DIE:
                Debug.LogWarning("=========================== DIE: " + idActor);
                actor.character.isDie = true;

                while (true)
                {
                    if (actor.isAnim1()) break;
                    Debug.Log("=========================== Loop " + actor.character.id + " Anim1");
                    yield return Timing.WaitForSeconds(0.01f);
                }
                actor.Play(6);
                break;
        }

        yield return Timing.WaitForSeconds(0.01f);

        if (action.actions.Count > 0)
        {
            Debug.LogWarning("=========================== Chưa diễn hết");
            action.actions.ForEach(x => C_Util.GetDumpObject(x));
        }
    }

    private IEnumerator<float> _SKILLING(C_Character actor, M_Action action)
    {       
        this.idTargets.Clear();
        this.targets.Clear();

        this.idTargets = action.prop.idTargets;
        this.Beaten = idTargets.Count;

        // Tìm Action của target
        FindActionOfTaget(action.actions);

        // Tìm Action của actor
        FindActionOfActor(actor, action.actions);

        // Diễn Skill khi có target
        if (this.targets.Count > 0)
        {
            while (true)
            {
                if (actor.isAnim1()) break;
                Debug.Log("=========================== Loop " + actor.character.id + " Anim1");
                yield return Timing.WaitForSeconds(0.01f);
            }
            actor.Play(action.prop.idSkill);
        }
        actor.ChangeEp();
        actor.ChangeHp();

        if (actor.character.isDie)
        {
            while (true)
            {
                if (actor.isAnim1()) break;
                Debug.Log("=========================== Loop " + actor.character.id + " Anim1");
                yield return Timing.WaitForSeconds(0.01f);
            }
            actor.Play(6);
        }
    }

    private void FindActionOfTaget(List<M_Action> actionCs)
    {
        for (int i = 0; i < actionCs.Count; i++)
        {
            M_Action actionC = actionCs[i];
            int idTargetC = actionC.idActor;

            if (this.idTargets.Contains(idTargetC))
            {
                C_Character target = Objs[idTargetC];

                switch (actionC.type)
                {
                    case C_Enum.ActionType.DODGE:
                        target.isHit = false;
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.BEATEN:
                        target.isHit = true;
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_HP:
                        target.PushChangeHp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_EP:
                        target.PushChangeEp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.DIE:
                        target.character.isDie = true;
                        actionCs.RemoveAt(i--);
                        break;
                }

                // Nếu tồn tại trong mảng targets
                if (idxTarget(idTargetC) != -1)
                {
                    this.targets[idxTarget(idTargetC)] = target;
                }
                else
                {
                    this.targets.Add(target);
                }
            }            
        }
    }

    private int idxTarget(int id_target)
    {
        for (int i = 0; i < this.targets.Count; i++)
        {
            if (this.targets[i].character.id == id_target) return i;
        }
        return -1;
    }

    private void FindActionOfActor(C_Character actor, List<M_Action> actionCs)
    {
        for (int i = 0; i < actionCs.Count; i++)
        {
            M_Action actionC = actionCs[i];
            int idActorC = actionC.idActor;

            if (actor.character.id == idActorC)
            {
                switch (actionC.type)
                {
                    case C_Enum.ActionType.CHANGE_HP:
                        actor.PushChangeHp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.CHANGE_EP:
                        actor.PushChangeEp(actionC.prop);
                        actionCs.RemoveAt(i--);
                        break;
                    case C_Enum.ActionType.DIE:
                        actor.character.isDie = true;
                        actionCs.RemoveAt(i--);
                        break;
                }
            }
        }
    }

    public void ScaleTime()
    {
        GameManager.instance.IdxTimeScale++;
        if (GameManager.instance.IdxTimeScale >= arrTimeScale.Length) GameManager.instance.IdxTimeScale = 0;

        myTimeScale = arrTimeScale[GameManager.instance.IdxTimeScale];
        txtTimeScale.text = "X" + myTimeScale;
    }

    public void Skip()
    {
        if (isSkip) return;

        isSkip = true;
    }

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 40;
        myStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 100, 20), "Get Action: " + getAction, myStyle);
        GUI.Label(new Rect(20, 70, 100, 20), "Turn: " + turn, myStyle);
        GUI.Label(new Rect(20, 120, 100, 20), "Beaten: " + Beaten, myStyle);
    }
}
