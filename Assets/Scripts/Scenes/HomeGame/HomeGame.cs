﻿using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeGame : MonoBehaviour
{
    public static HomeGame instance = null;

    [SerializeField]
    private C_ProfileAcc profileAcc = null;

    [SerializeField]
    private C_LstHeroAc lstHeroAc = null;

    [Header("Music")]
    [SerializeField]
    private Scrollbar sbVolume = null;
    [SerializeField]
    private Text txtVolume = null;
    [SerializeField]
    private GameObject btnMuteOn = null;
    [SerializeField]
    private GameObject btnMuteOff = null;

    [Header("Find Guild")]
    [SerializeField]
    private C_FindGuid findGuid = null;

    public Dictionary<int, M_Guild> dicGuid = new Dictionary<int, M_Guild>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void OutGame()
    {
        RequestLogin.Logout();
    }

    private void OnEnable()
    {
        sbVolume.value = SoundManager.instance.volume;
        txtVolume.text = (int)(SoundManager.instance.volume * 100) + "";
        btnMuteOn.SetActive(!SoundManager.instance.isMute);
        btnMuteOff.SetActive(SoundManager.instance.isMute);

        Timing.RunCoroutine(lstHeroAc._set());

        profileAcc.set(GameManager.instance.account);
    }

    public void ChangeVolume()
    {
        txtVolume.text = (int)(sbVolume.value * 100) + "";

        SoundManager.instance.ChangeVolume(sbVolume.value);
    }

    public void ChangeMute()
    {
        SoundManager.instance.ChangeMute();
    }

    public void GetGuild()
    {
        if(GameManager.instance.account.id_guild == -1)
        {
            SendGetGuilds(false);
        }
        else
        {            
            RequestGuild.GetGuild();
        }
    }

    public void SendGetGuilds(bool isSend = false)
    {
        Debug.Log("Get Guilds");
        if (dicGuid.Count == 0 || isSend)
            RequestGuild.GetGuilds();
        else findGuid.Open();
    }

    public void RecGuilds(List<M_Guild> guilds)
    {
        dicGuid.Clear();
        guilds.ForEach(x => { dicGuid.Add(x.id, x); });

        Timing.RunCoroutine(findGuid._set());
    }

    public void SendCreateGuild(string name)
    {
        Debug.Log("Create Guild: " + name);
        RequestGuild.CreateGuild(name);
    }

    public void RecCreateGuild(M_Guild guild)
    {
        dicGuid.Add(guild.id, guild);
        GameManager.instance.account.id_guild = guild.id;
        GameManager.instance.account.SetJob(C_Enum.JobGuild.Master);

        ShowGuild(guild);
    }

    public void RecPleaseGuild(M_Guild guild)
    {
        dicGuid[guild.id] = guild;
        GameManager.instance.account.id_guild = guild.id;
        GameManager.instance.account.SetJob(C_Enum.JobGuild.Normal);

        ShowGuild(guild);
    }

    public void ShowGuild(M_Guild guild)
    {        
        GameManager.instance.guild = guild;
        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        MainGame.instance.ShowScene(C_Enum.MainGame.GuildScene);
    }

    public void Arrange()
    {
        GameManager.instance.isAttack = false;
        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        SceneManager.LoadSceneAsync("PlayGame");
    }

    public void Tavern()
    {
        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        MainGame.instance.ShowScene(C_Enum.MainGame.TarvenScene);
    }

    public void Campaign()
    {
        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        MainGame.instance.ShowScene(C_Enum.MainGame.CampaignScene);
    }
}
