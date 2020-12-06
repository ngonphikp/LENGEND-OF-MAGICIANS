using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Guild
{
    public int id;
    public string name;
    public int lv;
    public int asset;
    public int rank;
    public string noti;
    public int maxMember = 20;

    public List<M_Account> accounts = new List<M_Account>();

    public List<M_EventGuild> events = new List<M_EventGuild>();

    public M_Guild()
    {

    }

    public M_Guild(ISFSObject obj)
    {
        if (obj == null) return;

        ISFSObject guild = obj.GetSFSObject(CmdDefine.ModuleGuild.GUILD);
        {
            this.id = guild.GetInt(CmdDefine.ModuleGuild.ID);
            this.name = guild.GetUtfString(CmdDefine.ModuleGuild.NAME);
            this.noti = guild.GetUtfString(CmdDefine.ModuleGuild.NOTI);
            this.lv = guild.GetInt(CmdDefine.ModuleGuild.LV);
        }

        this.UpdateLevel();

        List<M_Account> accounts = new List<M_Account>();
        ISFSArray arr = obj.GetSFSArray(CmdDefine.ModuleAccount.ACCOUNTS);
        for (int i = 0; i < arr.Count; i++)
        {
            accounts.Add(new M_Account(arr.GetSFSObject(i)));
        }
        this.accounts = accounts;
    }

    public M_Account GetMaster()
    {
        foreach (M_Account  acc in accounts)
        {
            if (acc.job == C_Enum.JobGuild.Master) return acc;
        }
        return null;
    }

    public void UpdateLevel()
    {
        for (int i = 1; i < lv; i++) UpLevel();
    }

    public void UpLevel()
    {
        maxMember += 5;

        if (maxMember > 50) maxMember = 50;
    }
}
