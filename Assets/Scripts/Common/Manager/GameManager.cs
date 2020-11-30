using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public string host = "192.168.1.46";

    public Dictionary<string, string> scenes = new Dictionary<string, string>(); // Key: Tên scene, Value: Tên scene cha (PreviousScene - Khi back)

    // Data Config
    public List<M_Character> heros = new List<M_Character>();
    public Dictionary<string, M_Character> herosDic = new Dictionary<string, M_Character>();

    public List<M_Character> creeps = new List<M_Character>();
    public Dictionary<string, M_Character> creepsDic = new Dictionary<string, M_Character>();

    public List<M_Character> bosses = new List<M_Character>();
    public Dictionary<string, M_Character> bossesDic = new Dictionary<string, M_Character>();

    public List<M_Milestone> milestones = new List<M_Milestone>();
    public Dictionary<int, M_Milestone> milestonesDic = new Dictionary<int, M_Milestone>();

    public List<M_BossG> bossGs = new List<M_BossG>();
    public Dictionary<int, M_BossG> bossGsDic = new Dictionary<int, M_BossG>();

    public List<M_Skill> skills = new List<M_Skill>();
    public Dictionary<string, M_Skill> skillsDic = new Dictionary<string, M_Skill>();

    // Data User
    public M_Account account = new M_Account();
    public List<M_Character> characters = new List<M_Character>();
    public List<M_Tick_Milestone> tick_milestones = new List<M_Tick_Milestone>();
    public Dictionary<int, M_Tick_Milestone> tick_milestonesDic = new Dictionary<int, M_Tick_Milestone>();    

    // Arrange & Fighting
    public bool isAttack = false;
    public int idMilestone = 0;

    private int idxTimeScale = 0;
    public int IdxTimeScale { 
        get => idxTimeScale;
        set {
            idxTimeScale = value;
            PlayerPrefs.SetInt("idxTimeScale", idxTimeScale);
        } 
    }

    // Infor
    public int idxCharacter = 0;

    // Guild
    public M_Guild guild;

    private void Awake()
    {
        MakeSingleInstance();
        LoadConfigJson();
        LoadLocalStorage();
    }

    private void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LoadLocalStorage()
    {
        idxTimeScale = PlayerPrefs.GetInt("idxTimeScale", 0);
    }

    private void LoadConfigJson()
    {
        LoadListHero();
        LoadListCreep();
        LoadListBoss();
        LoadListMilestone();
        LoadListBossG();
        LoadListSkill();
    }

    private void LoadListSkill()
    {
        JSonConvert convert = new JSonConvert();
        skills = convert.GetListSkill().ToList<M_Skill>();

        skillsDic = new Dictionary<string, M_Skill>(skills.Count);
        skills.ForEach(x => skillsDic.Add(x.id_cfg, x));
    }

    private void LoadListHero()
    {
        JSonConvert convert = new JSonConvert();
        heros = convert.GetListHero().ToList<M_Character>();

        herosDic = new Dictionary<string, M_Character>(heros.Count);
        heros.ForEach(x => herosDic.Add(x.id_cfg, x));
    }

    private void LoadListCreep()
    {
        JSonConvert convert = new JSonConvert();
        creeps = convert.GetListCreep().ToList<M_Character>();

        creepsDic = new Dictionary<string, M_Character>(creeps.Count);
        creeps.ForEach(x => creepsDic.Add(x.id_cfg, x));
    }

    private void LoadListBoss()
    {
        JSonConvert convert = new JSonConvert();
        bosses = convert.GetListBoss().ToList<M_Character>();

        bossesDic = new Dictionary<string, M_Character>(bosses.Count);
        bosses.ForEach(x => bossesDic.Add(x.id_cfg, x));
    }

    private void LoadListMilestone()
    {
        JSonConvert convert = new JSonConvert();
        milestones = convert.GetListMilestone().ToList<M_Milestone>();

        milestonesDic = new Dictionary<int, M_Milestone>(milestones.Count);
        milestones.ForEach(x => milestonesDic.Add(x.id, x));
    }

    private void LoadListBossG()
    {
        JSonConvert convert = new JSonConvert();
        bossGs = convert.GetListBossG().ToList<M_BossG>();

        bossGsDic = new Dictionary<int, M_BossG>(bossGs.Count);
        bossGs.ForEach(x => bossGsDic.Add(x.id, x));
    }

    public void UpdateTickMS()
    {
        tick_milestonesDic = new Dictionary<int, M_Tick_Milestone>(tick_milestonesDic.Count);
        tick_milestones.ForEach(x => tick_milestonesDic.Add(x.id_ml, x));
    }

    private void Update()
    {
        SmartFoxConnection.ListenerEvent();
    }

    public void OnApplicationQuit()
    {
        RequestLogin.Logout();
        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
    }

}
