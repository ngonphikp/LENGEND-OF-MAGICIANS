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

    private int idx = -1;

    public IEnumerator<float> _set(int idx)
    {
        this.idx = idx;
        M_Guild guild = HomeGame.instance.guilds[idx];

        txtLv.text = guild.lv + "";
        txtName.text = guild.name;
        txtBoss.text = guild.accounts[0].name;
        txtNoti.text = guild.noti;
        txtMember.text = guild.accounts.Count + " / " + guild.maxMember;

        if (guild.accounts.Count >= guild.maxMember) btnPlease.interactable = false;

        yield return Timing.WaitForOneFrame;
    }

    public void Please()
    {
        Debug.Log("Please Guid: " + HomeGame.instance.guilds[idx].id);

        if (GameManager.instance.test)
        {
            M_Guild guild = HomeGame.instance.guilds[idx];

            int size = guild.accounts.Count - guild.accounts.Count;
            for (int i = 1; i < size; i++)
            {
                M_Account account = new M_Account();
                account.id = i;
                account.name = "Name: " + account.id;

                guild.accounts.Add(account);
            }

            GameManager.instance.account.id_guilds = guild.id;
            GameManager.instance.guild = guild;
            HomeGame.instance.ShowGuild(guild);
            return;
        }
    }
}
