using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleGuild
{
    public static void OnResponse(SFSObject sfsObject)
    {
        short cmdid = (short)sfsObject.GetInt(CmdDefine.CMDID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.GETGUILDS:
                HandleGetGuilds(sfsObject);
                break;
            case CmdDefine.CREATEGUILD:
                HandleCreateGuild(sfsObject);
                break;
            case CmdDefine.GETGUILD:
                HandleGetGuild(sfsObject);
                break;
            case CmdDefine.OUTGUILD:
                HandleOutGuild(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void HandleGetGuilds(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            List<M_Guild> guilds = new List<M_Guild>();



            HomeGame.instance.RecGuilds(guilds);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleCreateGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            M_Guild guild = new M_Guild();



            HomeGame.instance.RecCreateGuild(guild);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleGetGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            M_Guild guild = new M_Guild();



            HomeGame.instance.ShowGuild(guild);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleOutGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            HomeGame.instance.OutGuild();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
