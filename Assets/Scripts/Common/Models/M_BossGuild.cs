using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_BossGuild
{
    public int id;
    public int id_guild;
    public int id_boss;
    public C_Enum.StatusBossG status;
    public int cur_hp;

    public M_BossGuild()
    {

    }

    public M_BossGuild(ISFSObject obj)
    {
        id = obj.GetInt(CmdDefine.ModuleBossGuild.ID);
        id_guild = obj.GetInt(CmdDefine.ModuleGuild.ID);
        id_boss = obj.GetInt(CmdDefine.ModuleBossGuild.ID_BOSS);
        status = (C_Enum.StatusBossG) obj.GetInt(CmdDefine.ModuleBossGuild.STATUS);
        cur_hp = obj.GetInt(CmdDefine.ModuleBossGuild.CUR_HP);
    }
}
