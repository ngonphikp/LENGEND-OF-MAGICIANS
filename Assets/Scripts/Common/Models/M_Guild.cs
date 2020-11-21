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
    public int master;
    public int maxMember = 20;

    public List<M_Account> accounts = new List<M_Account>();

    public List<M_EventGuild> events = new List<M_EventGuild>();

    public M_Guild()
    {

    }

    public M_Guild(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt(CmdDefine.ModuleGuild.ID);
        this.name = obj.GetUtfString(CmdDefine.ModuleGuild.NAME);
        this.noti = obj.GetUtfString(CmdDefine.ModuleGuild.NOTI);
        this.lv = obj.GetInt(CmdDefine.ModuleGuild.LV);
        this.master = obj.GetInt(CmdDefine.ModuleGuild.MASTER);

        ISFSArray arr = obj.GetSFSArray(CmdDefine.ModuleGuild.ACCOUNTS);
        for (int i = 0; i < arr.Count; i++)
        {
            accounts.Add(new M_Account(arr.GetSFSObject(i)));
        }
    }

    public M_Account GetMaster()
    {
        foreach (M_Account  acc in accounts)
        {
            if (acc.id == master) return acc;
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
    }
}
