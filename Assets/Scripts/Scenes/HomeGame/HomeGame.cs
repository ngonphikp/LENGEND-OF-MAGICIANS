﻿using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeGame : MonoBehaviour
{
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;
    [SerializeField]
    private Transform bagHero = null;
    [SerializeField]
    private GameObject bagEl = null;
    [SerializeField]
    private Scrollbar sbVolume = null;
    [SerializeField]
    private Text txtVolume = null;
    [SerializeField]
    private GameObject btnMuteOn = null;
    [SerializeField]
    private GameObject btnMuteOff = null;

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
                heroAc.set(i);
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
            heroBag.set(i);
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
}
