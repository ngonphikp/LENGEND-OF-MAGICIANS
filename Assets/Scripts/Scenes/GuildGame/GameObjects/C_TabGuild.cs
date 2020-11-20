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

    public void SetEvent(string evt)
    {

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
