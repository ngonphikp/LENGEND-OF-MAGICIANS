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
                M_Guild guild = new M_Guild(arr.GetSFSObject(i));
                guild.UpdateLevel();
                guilds.Add(guild);
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
            M_Guild guild = new M_Guild(packet.GetSFSObject(CmdDefine.ModuleGuild.GUILD));
            guild.UpdateLevel();

            HomeGame.instance.RecCreateGuild(guild);
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
            M_Guild guild = new M_Guild(packet.GetSFSObject(CmdDefine.ModuleGuild.GUILD));
            guild.UpdateLevel();

            HomeGame.instance.RecPleaseGuild(guild);
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
            M_Guild guild = new M_Guild(packet.GetSFSObject(CmdDefine.ModuleGuild.GUILD));
            guild.UpdateLevel();

            HomeGame.instance.ShowGuild(guild);
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
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleGuild.EVENTS);
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
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleGuild.ACCOUNTS);
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
}
