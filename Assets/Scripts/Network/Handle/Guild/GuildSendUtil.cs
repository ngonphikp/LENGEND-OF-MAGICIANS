using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class GuildSendUtil
{
    private static string MODULE = CmdDefine.MODULE_GUILD;

    public static void sendGetGuilds()
    {
        Debug.Log("=========================== Get Guilds");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.GETGUILDS);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void sendCreateGuild(string name)
    {
        Debug.Log("=========================== Create Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.CREATEGUILD);
        isFSObject.PutUtfString("name", name);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void sendGetGuild(int id)
    {
        Debug.Log("=========================== Get Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.GETGUILD);
        isFSObject.PutInt("id", id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }

    public static void sendOutGuild()
    {
        Debug.Log("=========================== Out Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.OUTGUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }
}
