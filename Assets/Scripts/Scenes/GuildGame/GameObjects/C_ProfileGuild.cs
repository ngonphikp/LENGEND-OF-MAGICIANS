using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ProfileGuild : MonoBehaviour
{
    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtName = null;
    [SerializeField]
    private Text txtMaster = null;
    [SerializeField]
    private Text txtMember = null;
    [SerializeField]
    private Text txtRank = null;
    [SerializeField]
    private Text txtAsset = null;

    public void set(M_Guild guild)
    {
        txtLv.text = guild.lv + "";
        txtName.text = guild.name;
        txtMaster.text = guild.GetMaster().name + "";
        txtMember.text = guild.accounts.Count + " / " + guild.maxMember;
        txtAsset.text = guild.asset + "";
        txtRank.text = guild.rank + "";
    }
}
