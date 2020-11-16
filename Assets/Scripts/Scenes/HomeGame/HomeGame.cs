﻿using MEC;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeGame : MonoBehaviour
{
    public static HomeGame instance = null;

    [Header("Bag Hero")]
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;
    [SerializeField]
    private Transform bagHero = null;
    [SerializeField]
    private GameObject bagEl = null;

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

        Timing.RunCoroutine(_FilterListHero());
        Timing.RunCoroutine(_LoadBagHero());
    }

    private IEnumerator<float> _FilterListHero()
    {
        foreach (Transform child in listHeroAc)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            if (GameManager.instance.characters[i].idx != -1)
            {
                C_CharacterAcEl heroAc = Instantiate(heroAcEl, listHeroAc).GetComponent<C_CharacterAcEl>();
                Timing.RunCoroutine(heroAc._set(i));
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<float> _LoadBagHero()
    {
        foreach (Transform child in bagHero)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.characters.Count; i++)
        {
            C_BagEl heroBag = Instantiate(bagEl, bagHero).GetComponent<C_BagEl>();
            Timing.RunCoroutine(heroBag._set(i));
        }

        yield return Timing.WaitForOneFrame;
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
            RequestGuild.GetGuild(GameManager.instance.account.id_guild);
        }
    }

    public void SendGetGuilds(bool isSend = false)
    {
        Debug.Log("Get Guilds");        
        if (dicGuid.Count == 0 || isSend)
            RequestGuild.GetGuilds();
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

        ShowGuild(guild);
    }

    public void RecPleaseGuild(M_Guild guild)
    {
        dicGuid[guild.id] = guild;
        GameManager.instance.account.id_guild = guild.id;

        ShowGuild(guild);
    }

    public void ShowGuild(M_Guild guild)
    {        
        GameManager.instance.guild = guild;
        MainGame.instance.ShowScene("GuildScene");
    }
}
