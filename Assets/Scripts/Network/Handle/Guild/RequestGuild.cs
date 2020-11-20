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
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_GUILDS);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void CreateGuild(string name)
    {
        Debug.Log("=========================== Create Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.CREATE_GUILD);

        isFSObject.PutUtfString(CmdDefine.ModuleGuild.NAME, name);
        isFSObject.PutInt(CmdDefine.ModuleGuild.MASTER, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetGuild(int id)
    {
        Debug.Log("=========================== Get Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void PleaseGuild(int id)
    {
        Debug.Log("=========================== Please Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.PLEASE_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, id);
        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void OutGuild()
    {
        Debug.Log("=========================== Out Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.OUT_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);
        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, GameManager.instance.account.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void ChangeMaster(int master)
    {
        Debug.Log("=========================== Change Master");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.FIX_MASTER_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);
        isFSObject.PutInt(CmdDefine.ModuleGuild.MASTER, master);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetNoti()
    {
        Debug.Log("=========================== Get Noti");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_NOTI_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void FixNoti(string noti)
    {
        Debug.Log("=========================== Fix Noti");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.FIX_NOTI_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);
        isFSObject.PutUtfString(CmdDefine.ModuleGuild.NOTI, noti);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetEvent()
    {
        Debug.Log("=========================== Get Event");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_EVENT_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetMember()
    {
        Debug.Log("=========================== Get Member");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_MEMBER_GUID);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, GameManager.instance.guild.id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
