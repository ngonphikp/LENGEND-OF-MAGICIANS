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

    private M_Guild guild = new M_Guild();

    public IEnumerator<float> _set(M_Guild guild)
    {
        this.guild = guild;
        txtLv.text = guild.lv + "";
        txtName.text = guild.name;
        txtBoss.text = guild.boss;
        txtNoti.text = guild.noti;
        txtMember.text = guild.currentMember + " / " + guild.maxMember;

        if (guild.currentMember >= guild.maxMember) btnPlease.interactable = false;

        yield return Timing.WaitForOneFrame;
    }

    public void Please()
    {
        Debug.Log("Please Guid: " + guild.id);
    }
}
