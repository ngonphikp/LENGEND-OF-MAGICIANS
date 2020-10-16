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

    public List<M_Guild> guilds = new List<M_Guild>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void OutGame()
    {
        if (GameManager.instance.test)
        {
            SceneManager.LoadScene("LoginGame");
            GameManager.instance.test = false;
        }
        else LoginSendUtil.sendLogout();
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

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
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

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
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
        if(GameManager.instance.taikhoan.id_guilds == -1)
        {
            SendGetGuilds(false);
        }
        else
        {
            if (GameManager.instance.test)
            {
                for(int i = 0; i < this.guilds.Count; i++)
                {
                    if(GameManager.instance.taikhoan.id_guilds == this.guilds[i].id)
                    {
                        ShowGuild(this.guilds[i]);
                        return;
                    }
                }

                M_Guild guild = new M_Guild();

                guild.id = GameManager.instance.taikhoan.id_guilds;
                guild.name = "Name" + GameManager.instance.taikhoan.id_guilds;
                guild.accounts.Add(GameManager.instance.taikhoan);
                guild.boss = GameManager.instance.taikhoan.id;
#pragma warning disable CS0618 // Type or member is obsolete
                guild.lv = Random.RandomRange(1, 5);
#pragma warning restore CS0618 // Type or member is obsolete
                guild.noti = "Noti: " + GameManager.instance.taikhoan.id_guilds;
                guild.UpdateLevel();
#pragma warning disable CS0618 // Type or member is obsolete
                guild.currentMember = Random.RandomRange((int)guild.maxMember / 2, guild.maxMember + 1);
#pragma warning restore CS0618 // Type or member is obsolete

                int size = guild.currentMember - guild.accounts.Count;
                for (int i = 1; i < size; i++)
                {
                    M_Account account = new M_Account();
                    account.id = i;
                    account.name = "Name: " + account.id;

                    guild.accounts.Add(account);
                }

                guilds.Add(guild);

                ShowGuild(guild);
                return;
            }
            UserSendUtil.sendGetGuild(GameManager.instance.taikhoan.id_guilds);
        }
    }

    public void SendGetGuilds(bool isSend = false)
    {
        Debug.Log("Get Guilds");
        if (GameManager.instance.test)
        {
            if(guilds.Count == 0 || isSend)
            {
                guilds.Clear();

                string[] names = { "As", "you", "point", "out", "it", "very", "broad", "question",
                    "but", "try", "touch", "few", "that", "should", "get", "started", "First", "you", "mention",
                "when", "loads", "You", "can", "two", "ways", "one", "simply", "place", "component",
                "that", "scene", "then", "Awake", "Start", "method", "component", "respond", "loading", "scene",
                "Alternatively", "first", "main", "your", "game", "you", "could", "object", "destroyed", "load",
                "second", "scene", "have", "script", "register", "SceneManager", "sceneLoaded", "event", "which" };

                for (int i = 0; i < 35; i++)
                {
                    M_Guild guild = new M_Guild();

                    guild.id = i;
#pragma warning disable CS0618 // Type or member is obsolete
                    guild.name = names[Random.RandomRange(0, names.Length)] + i;
#pragma warning restore CS0618 // Type or member is obsolete
                    M_Account boss = new M_Account();
                    boss.id = 0;
                    boss.name = "Name: " + boss.id;
                    guild.boss = boss.id;
                    guild.accounts.Add(boss);
#pragma warning disable CS0618 // Type or member is obsolete
                    guild.lv = Random.RandomRange(1, 5);
#pragma warning restore CS0618 // Type or member is obsolete
                    guild.noti = "Noti: " + i;
                    guild.UpdateLevel();
#pragma warning disable CS0618 // Type or member is obsolete
                    guild.currentMember = Random.RandomRange((int)guild.maxMember / 2, guild.maxMember + 1);
#pragma warning restore CS0618 // Type or member is obsolete

                    guilds.Add(guild);
                }
            }
           
            RecGuilds(guilds);
            return;
        }
        if (guilds.Count == 0 || isSend)
            UserSendUtil.sendGetGuilds();
    }

    public void RecGuilds(List<M_Guild> guilds)
    {
        this.guilds = guilds;
        Timing.RunCoroutine(findGuid._set());
    }

    public void SendCreateGuild(string name)
    {
        Debug.Log("Create Guild: " + name);

        // KT trùng tên trong ds đã có
        for (int i = 0; i < this.guilds.Count; i++)
        {
            if (this.guilds[i].name.Equals(name))
            {
                Debug.Log("Trùng tên");
                return;
            }
        }
        // KT tài nguyên

        if (GameManager.instance.test)
        {
            M_Guild guild = new M_Guild();

            guild.id = this.guilds.Count;
            guild.name = name;
            guild.lv = 1;
            guild.noti = "Noti: " + this.guilds.Count;
            guild.currentMember = 1;
            guild.accounts.Add(GameManager.instance.taikhoan);
            guild.boss = GameManager.instance.taikhoan.id;

            RecCreateGuild(guild);
            return;
        }

        UserSendUtil.sendCreateGuild(name);
    }

    public void RecCreateGuild(M_Guild guild)
    {
        this.guilds.Add(guild);
        GameManager.instance.taikhoan.id_guilds = guild.id;

        ShowGuild(guild);
    }

    public void ShowGuild(M_Guild guild)
    {        
        GameManager.instance.guild = guild;
        MainGame.instance.ShowScene("GuildScene");
    }
}
