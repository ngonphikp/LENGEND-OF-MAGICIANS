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
            case CmdDefine.CMD.GETGUILDS:
                HandleGetGuilds(sfsObject);
                break;
            case CmdDefine.CMD.CREATEGUILD:
                HandleCreateGuild(sfsObject);
                break;
            case CmdDefine.CMD.GETGUILD:
                HandleGetGuild(sfsObject);
                break;
            case CmdDefine.CMD.OUTGUILD:
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
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleCreateGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET GUILDS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Guild guild = new M_Guild(packet.GetSFSObject(CmdDefine.ModuleGuild.GUILD));
            guild.UpdateLevel();

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
        if (ec == CmdDefine.ErrorCode.SUCCESS)
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
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            HomeGame.instance.OutGuild();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
