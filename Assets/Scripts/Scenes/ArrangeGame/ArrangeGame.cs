﻿using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeGame : MonoBehaviour
{
    public static ArrangeGame instance = null;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private Transform listHeroAv = null;
    [SerializeField]
    private GameObject heroAvEl = null;

    [SerializeField]
    private Animator arrange = null;
    [SerializeField]
    private C_CellAT[] teamL = null;
    [SerializeField]
    private C_CellAT[] teamR = null;

    [SerializeField]
    private Button btnSave = null;
    [SerializeField]
    private Button btnFighting = null;

    private List<M_Character> nhanVats = new List<M_Character>();
    public int countActive = 0;

    public Dictionary<int, C_CharacterAvEl> Objs = new Dictionary<int, C_CharacterAvEl>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        btnFighting.gameObject.SetActive(GameManager.instance.isAttack);
        btnSave.gameObject.SetActive(!GameManager.instance.isAttack);

        Timing.RunCoroutine(_LoadListHero());

        if(GameManager.instance.isAttack) Timing.RunCoroutine(_LoadListCreep());

        LoadAnim(true);
    }

    private void LoadAnim(bool v)
    {
        arrange.SetBool("isArrange", v);

        for (int i = 0; i < teamL.Length; i++) teamL[i].GetComponent<Animator>().SetBool("isArrange", v);
        for (int i = 0; i < teamR.Length; i++) teamR[i].GetComponent<Animator>().SetBool("isArrange", v);
    }

    private IEnumerator<float> _LoadListHero()
    {
        Objs.Clear();

        foreach (Transform child in listHeroAv)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            M_Character nhanVat = new M_Character(GameManager.instance.nhanVats[i]);
            nhanVat.Current_ep = nhanVat.max_ep = 100;
            nhanVat.Current_hp = nhanVat.max_hp = nhanVat.hp;
            nhanVat.team = 0;

            C_CharacterAvEl heroAv = Instantiate(heroAvEl, listHeroAv).GetComponent<C_CharacterAvEl>();
            heroAv.set(nhanVat);

            nhanVats.Add(nhanVat);

            if (nhanVat.idx != -1)
            {
                if (countActive >= C_Params.maxActive) break;

                heroAv.Active();                

                teamL[nhanVat.idx].set(nhanVat, canvas);

                countActive++;
            }

            Objs.Add(nhanVat.id_nv, heroAv);
        }

        Debug.Log("Count: " + Objs.Keys.Count);

        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<float> _LoadListCreep()
    {
        List<M_Character> creeps = GameManager.instance.milestones[GameManager.instance.idxMilestone].listCreep;

        for (int i = 0; i < creeps.Count; i++)
        {
            if (creeps[i].idx != -1)
            {
                M_Character nhanVat = creeps[i];
                nhanVat.Current_ep = nhanVat.max_ep = 100;
                nhanVat.Current_hp = nhanVat.max_hp = nhanVat.hp;
                nhanVat.team = 1;

                teamR[creeps[i].idx].set(nhanVat, canvas, false);
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    public void Active(M_Character nhanVat)
    {
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.nhanVat.idx == -1)
            {
                nhanVat.idx = i;
                teamL[i].set(nhanVat, canvas);
                countActive++;

                return;
            }
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

    private void iSave()
    {
        List<M_Character> nhanVats = new List<M_Character>();

        // Update index hero in teamL
        for (int i = 0; i < teamL.Length; i++)
        {
            if (teamL[i].content.nhanVat.idx != -1)
            {
                teamL[i].content.nhanVat.idx = i;

                nhanVats.Add(teamL[i].content.nhanVat);
            }
        }

        // Update index hero in list
        foreach (C_CharacterAvEl el in Objs.Values)
        {
            if (!el.isActive)
            {
                el.nhanVat.idx = -1;

                nhanVats.Add(el.nhanVat);
            }
        }

        //heros.ForEach(x => Debug.Log(x.id_nv + " / " + x.id_cfg + " / " + x.idx));        
        
        GameManager.instance.nhanVats = nhanVats;

        if (GameManager.instance.test) Timing.RunCoroutine(_RecArrange());
        else CharacterSendUtil.sendArrange(nhanVats);
    }

    public IEnumerator<float> _RecArrange()
    {
        LoadAnim(false);

        yield return Timing.WaitForSeconds(1f);
        if (GameManager.instance.isAttack) PlayGame.instance.ShowScene(true);
        else ScenesManager.instance.ChangeScene("MainGame");
    }
}
