using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_TickBossGuild
{
    public int id;
    public int id_guild;
    public int id_boss;
    public int id_ac;
    public C_Enum.StatusBossG status = C_Enum.StatusBossG.Lock;
    public int cur_hp;
    public int max_hp;

    public int cut_turn;
    public int max_turn;

    public bool isReward;

    public M_TickBossGuild()
    {

    }

    public M_TickBossGuild(ISFSObject obj)
    {
        id = obj.GetInt(CmdDefine.ModuleTickBossGuild.ID);
        id_guild = obj.GetInt(CmdDefine.ModuleGuild.ID);
        id_ac = obj.GetInt(CmdDefine.ModuleAccount.ID);
        id_boss = obj.GetInt(CmdDefine.ModuleTickBossGuild.ID_BOSS);
        status = (C_Enum.StatusBossG) obj.GetInt(CmdDefine.ModuleTickBossGuild.STATUS);
        cur_hp = obj.GetInt(CmdDefine.ModuleTickBossGuild.CUR_HP);
        max_hp = obj.GetInt(CmdDefine.ModuleTickBossGuild.MAX_HP);
    }
}
