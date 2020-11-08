﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame : MonoBehaviour
{    
    public static LoginGame instance = null;

    [SerializeField]
    private AudioClip audioClip = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        SoundManager.instance.PlayLoop(audioClip);
    }

    public void RecLogin(M_Account ac)
    {
        Debug.Log("====================RecLogin: " + ac.id);

        GameManager.instance.account = ac;

        if(!GameManager.instance.test) RequestAccount.GetInfo(ac.id);
    }

    public void RecRegister(M_Account ac)
    {
        Debug.Log("====================RecRegister: " + ac.id);

        GameManager.instance.account = ac;

        if (!GameManager.instance.test) RequestAccount.GetInfo(ac.id);
    }

    public void RecInfo(List<M_Character> lstCharacter, List<M_Tick_Milestone> lstTick_milestones)
    {
        Debug.Log("====================RecInfo");

        //lstCharacter.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));
        //lstTick_milestones.ForEach(x => Debug.Log(x.id + " / " + x.star));

        GameManager.instance.tick_milestones = lstTick_milestones;
        GameManager.instance.UpdateTickMS();

        GameManager.instance.characters = lstCharacter;
        ScenesManager.instance.ChangeScene("MainGame");

        SoundManager.instance.PlayLoop();
    }

    public void TestPlay()
    {
        GameManager.instance.TestPlay();
    }

    public void QuitGame()
    {
        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
        Application.Quit();
    }
}
