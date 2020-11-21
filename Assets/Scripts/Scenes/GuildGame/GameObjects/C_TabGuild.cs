using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_TabGuild : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tabs = new GameObject[5];
    [SerializeField]
    private InputField ipfNoti = null;
    [SerializeField]
    private C_LstMemberG lstMember = null;

    [SerializeField]
    private ScrollRect scEvent = null;
    [SerializeField]
    private Text txtEvent = null;

    private int idxAc = -1;

    public void ShowTab(int idx)
    {
        if (idx == idxAc) return;
        this.idxAc = idx;

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[idx].SetActive(true);
    }

    private void OnEnable()
    {
        ShowTab(0);
    }

    public void SetAccounts(List<M_Account> accounts)
    {
        lstMember.set(accounts);
    }

    public IEnumerator<float> _SetEvent(List<M_EventGuild> events, bool isReset = false)
    {
        if (isReset) txtEvent.text = "";
        for (int i = 0; i < events.Count; i++)
        {
            if (txtEvent.text != "") txtEvent.text += "\n\n";
            txtEvent.text += new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(events[i].time)) + " : " + events[i].content;
        }

        yield return Timing.WaitForOneFrame;
        scEvent.verticalNormalizedPosition = 0;
    }

    public void SetNoti(string noti)
    {
        ipfNoti.text = noti;
    }

    public void FixNoti()
    {
        RequestGuild.FixNoti(ipfNoti.text);
    }
}
