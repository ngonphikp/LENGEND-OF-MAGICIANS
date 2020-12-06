using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleGuild
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GET_GUILDS:
                HandleGetGuilds(sfsObject);
                break;
            case CmdDefine.CMD.CREATE_GUILD:
                HandleCreateGuild(sfsObject);
                break;
            case CmdDefine.CMD.GET_GUILD:
                HandleGetGuild(sfsObject);
                break;
            case CmdDefine.CMD.OUT_GUILD:
                HandleOutGuild(sfsObject);
                break;
            case CmdDefine.CMD.PLEASE_GUILD:
                HandlePleaseGuild(sfsObject);
                break;
            case CmdDefine.CMD.GET_NOTI_GUILD:
                HandleGetNoti(sfsObject);
                break;
            case CmdDefine.CMD.GET_EVENT_GUILD:
                HandleGetEvent(sfsObject);
                break;
            case CmdDefine.CMD.GET_MEMBER_GUID:
                HandleGetMember(sfsObject);
                break;
            case CmdDefine.CMD.FIX_NOTI_GUILD:
                HandleFixNoti(sfsObject);
                break;
            case CmdDefine.CMD.GET_BOSSES_GUILD:
                HandleGetBosses(sfsObject);
                break;
            case CmdDefine.CMD.GET_TICK_BOSS_GUILD:
                HandleGetTickBoss(sfsObject);
                break;
            case CmdDefine.CMD.UNLOCK_BOSS_GUILD:
                HandleUnLockBoss(sfsObject);
                break;
            case CmdDefine.CMD.END_GAME_BOSS_GUILD:
                HandleEndGameBoss(sfsObject);
                break;
            case CmdDefine.CMD.REWARD_BOSS_GUILD:
                HandleRewardBoss(sfsObject);
                break;
            default:
                break;
        }
    }

    public static void HandleGetGuilds(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_Guild> guilds = new List<M_Guild>();
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleGuild.GUILDS);

            for (int i = 0; i < arr.Count; i++)
            {               
                guilds.Add(new M_Guild(arr.GetSFSObject(i)));
            }

            HomeGame.instance.RecGuilds(guilds);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleCreateGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE CREATE GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            HomeGame.instance.RecCreateGuild(new M_Guild(packet));
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandlePleaseGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE PLEASE GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            HomeGame.instance.RecPleaseGuild(new M_Guild(packet));
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            HomeGame.instance.ShowGuild(new M_Guild(packet));
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleOutGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE OUT GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            GuildGame.instance.RecOutGuild();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetNoti(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET NOTI\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            string noti = packet.GetUtfString(CmdDefine.ModuleGuild.NOTI);
            GuildGame.instance.RecNoti(noti);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetEvent(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET EVENT\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_EventGuild> events = new List<M_EventGuild>();
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleEventGuild.EVENTS_GUILD);
            for (int i = 0; i < arr.Count; i++)
            {
                events.Add(new M_EventGuild(arr.GetSFSObject(i)));
            }
            GuildGame.instance.RecEvent(events);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetMember(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE MEMBER\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_Account> accounts = new List<M_Account>();
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleAccount.ACCOUNTS);
            for (int i = 0; i < arr.Count; i++)
            {
                accounts.Add(new M_Account(arr.GetSFSObject(i)));
            }
            GuildGame.instance.RecMember(accounts);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleFixNoti(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE FIX NOTI\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            Debug.Log("SUCCESS");
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetBosses(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE BOSSES\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_BossGuild> lstBg = new List<M_BossGuild>();
            ISFSArray bgs = packet.GetSFSArray(CmdDefine.ModuleBossGuild.BOSSES_GUILD);
            for (int i = 0; i < bgs.Count; i++)
            {
                lstBg.Add(new M_BossGuild(bgs.GetSFSObject(i)));
            }

            GuildGame.instance.RecBosses(lstBg);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetTickBoss(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE TICK BOSS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            GuildGame.instance.RecTickBoss(new M_BossGuild(packet.GetSFSObject(CmdDefine.ModuleBossGuild.BOSS_GUILD)), new M_TickBossGuild(packet.GetSFSObject(CmdDefine.ModuleTickBossGuild.TICK_BOSS_GUILD)));
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleUnLockBoss(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE UNLOCK BOSS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            GuildGame.instance.RecUnLockBoss();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleRewardBoss(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE REWARD GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            GuildGame.instance.RecRewardBoss();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleEndGameBoss(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE END GAME\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            FightingGame.instance.RecEndGame();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }
}
