using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestGuild
{
    private static string MODULE = CmdDefine.Module.MODULE_GUILD;

    public static void GetGuilds()
    {
        Debug.Log("=========================== Get Guilds");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GETGUILDS);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void CreateGuild(string name)
    {
        Debug.Log("=========================== Create Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.CREATEGUILD);

        isFSObject.PutUtfString(CmdDefine.ModuleGuild.NAME, name);
        isFSObject.PutInt(CmdDefine.ModuleGuild.MASTER, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetGuild(int id)
    {
        Debug.Log("=========================== Get Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GETGUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void PleaseGuild(int id)
    {
        Debug.Log("=========================== Please Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.PLEASEGUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, id);
        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void OutGuild()
    {
        Debug.Log("=========================== Out Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.OUTGUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);
        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void ChangeMaster(int master)
    {
        Debug.Log("=========================== Change Master");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.CHANGEMASTER);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);
        isFSObject.PutInt(CmdDefine.ModuleGuild.MASTER, master);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
