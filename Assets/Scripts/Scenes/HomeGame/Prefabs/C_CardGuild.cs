using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_CardGuild : MonoBehaviour
{
    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtName = null;
    [SerializeField]
    private Text txtBoss = null;
    [SerializeField]
    private Text txtNoti = null;
    [SerializeField]
    private Text txtMember = null;
    [SerializeField]
    private Button btnPlease = null;

    private int id = -1;

    public IEnumerator<float> _set(int id)
    {
        this.id = id;
        M_Guild guild = HomeGame.instance.dicGuid[id];

        txtLv.text = guild.lv + "";
        txtName.text = guild.name;
        txtBoss.text = guild.GetMaster().name;
        txtNoti.text = guild.noti;
        txtMember.text = guild.accounts.Count + " / " + guild.maxMember;

        if (guild.accounts.Count >= guild.maxMember) btnPlease.interactable = false;

        yield return Timing.WaitForOneFrame;
    }

    public void Please()
    {
        Debug.Log("Please Guid: " + id);
        RequestGuild.PleaseGuild(id);
    }
}
