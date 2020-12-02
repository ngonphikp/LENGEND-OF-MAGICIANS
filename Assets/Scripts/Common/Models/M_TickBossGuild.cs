using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class M_TickBossGuild
{
    public int id;
    public int id_bg;
    public int id_ac;
    public int cur_turn;
    public bool is_reward;

    public M_TickBossGuild()
    {

    }

    public M_TickBossGuild(ISFSObject obj)
    {
        id = obj.GetInt(CmdDefine.ModuleTickBossGuild.ID);
        id_bg = obj.GetInt(CmdDefine.ModuleBossGuild.ID);
        id_ac = obj.GetInt(CmdDefine.ModuleAccount.ID);
        cur_turn = obj.GetInt(CmdDefine.ModuleTickBossGuild.CUR_TURN);
        is_reward = obj.GetBool(CmdDefine.ModuleTickBossGuild.IS_REWARD);
    }
}
