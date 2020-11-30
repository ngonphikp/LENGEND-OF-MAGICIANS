using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildGame : MonoBehaviour
{
    public static GuildGame instance = null;

    [SerializeField]
    private C_ProfileGuild profile = null;    

    [SerializeField]
    private C_TabGuild tab = null;

    [SerializeField]
    private C_BossesG bossesG = null;
    [SerializeField]
    private C_ConfigBossG cfgBoss = null;

    [Header("UI Master")]
    [SerializeField]
    private GameObject[] uis = null;

    public bool isMaster = false;
    public M_Guild guild = null;

    public List<M_TickBossGuild> tick_bosses = new List<M_TickBossGuild>();
    public Dictionary<int, M_TickBossGuild> tick_bossesDic = new Dictionary<int, M_TickBossGuild>();

    private int id_active = -1;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    private void OnEnable()
    {
        guild = GameManager.instance.guild;
        isMaster = GameManager.instance.account.job == C_Enum.JobGuild.Master;

        foreach (GameObject ui in uis) ui.SetActive(isMaster);

        profile.set(guild);
        tab.SetNoti(guild.noti);
        Timing.RunCoroutine(tab._SetEvent(guild.events, true));
    }

    public void OutGuild()
    {
        if(isMaster)
        {
            Debug.Log("Bạn là hội trưởng, cần nhường chức vụ cho thành viên khác trước khi rời hội");
            return;
        }

        Debug.Log("Out Guild");

        RequestGuild.OutGuild();
    }

    public void RecOutGuild()
    {
        GameManager.instance.account.SetJob(C_Enum.JobGuild.None);
        MainGame.instance.ShowScene("HomeScene");
    }

    public void ChangeMaster(int master)
    {
        RequestGuild.ChangeMaster(master);
    }

    public void GetNoti()
    {
        RequestGuild.GetNoti();
    }

    public void RecNoti(string noti)
    {
        guild.noti = noti;
        tab.SetNoti(noti);
    }

    public void GetEvent()
    {
        RequestGuild.GetEvent();
    }

    public void RecEvent(List<M_EventGuild> events)
    {
        for (int i = 0; i < events.Count; i++)
        {
            guild.events.Add(events[i]);
        }
        Timing.RunCoroutine(tab._SetEvent(events));
    }

    public void GetMember()
    {
        RequestGuild.GetMember();
    }

    public void RecMember(List<M_Account> accounts)
    {
        guild.accounts = accounts;
        tab.SetAccounts(accounts);
        profile.set(guild);
    }    

    public void OpenBoss()
    {
        if(id_active == -1)
        {
            RequestGuild.GetTickBosses();
        }
        else
        {
            RequestGuild.GetTickBoss(id_active);
        }
    }

    public void RecTickBosses(List<M_TickBossGuild> tick_bosses)
    {
        this.tick_bosses = tick_bosses;
        tick_bossesDic = new Dictionary<int, M_TickBossGuild>(tick_bosses.Count);
        tick_bosses.ForEach(x => tick_bossesDic.Add(x.id, x));

        if(tick_bosses.Count >= 4) bossesG.show();
        if(tick_bosses.Count > 0) this.id_active = tick_bosses[0].id;
        if(id_active != -1) cfgBoss.set(id_active);
    }

    public void RecTickBoss(M_TickBossGuild tbg)
    {
        this.id_active = tbg.id;

        if (tick_bossesDic.ContainsKey(tbg.id))
        {
            tick_bossesDic[tbg.id] = tbg;
        }

        bossesG.UpdateUI(tbg.id);
        cfgBoss.set(id_active);
    }
}
