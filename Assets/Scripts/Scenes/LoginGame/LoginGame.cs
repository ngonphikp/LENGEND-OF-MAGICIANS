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

    public void RecLogin(M_Account tk)
    {
        Debug.Log("====================RecLogin: " + tk.id);

        GameManager.instance.taikhoan = tk;

        if(!GameManager.instance.test) RequestUser.GetInfo(tk.id);
    }

    public void RecRegister(M_Account tk)
    {
        Debug.Log("====================RecRegister: " + tk.id);

        GameManager.instance.taikhoan = tk;

        if (!GameManager.instance.test) RequestUser.GetInfo(tk.id);
    }

    public void RecInfo(List<M_Character> lstNhanVat, List<M_Milestone> tick_milestones)
    {
        Debug.Log("====================RecInfo");

        //lstNhanVat.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.id_tk + " / " + x.lv));
        //tick_milestones.ForEach(x => Debug.Log(x.id + " / " + x.star));

        GameManager.instance.tick_milestones = tick_milestones;
        GameManager.instance.UpdateTickMS();

        GameManager.instance.nhanVats = lstNhanVat;
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
