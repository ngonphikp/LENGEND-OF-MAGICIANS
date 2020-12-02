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

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetGuild()
    {
        Debug.Log("=========================== Get Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void PleaseGuild(int id)
    {
        Debug.Log("=========================== Please Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.PLEASE_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.ID, id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void OutGuild()
    {
        Debug.Log("=========================== Out Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.OUT_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void ChangeMaster(int master)
    {
        Debug.Log("=========================== Change Master");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.FIX_MASTER_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleGuild.MASTER, master);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetNoti()
    {
        Debug.Log("=========================== Get Noti");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_NOTI_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void FixNoti(string noti)
    {
        Debug.Log("=========================== Fix Noti");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.FIX_NOTI_GUILD);

        isFSObject.PutUtfString(CmdDefine.ModuleGuild.NOTI, noti);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetEvent()
    {
        Debug.Log("=========================== Get Event");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_EVENT_GUILD);

        isFSObject.PutInt(CmdDefine.ModuleEventGuild.COUNT, GameManager.instance.guild.events.Count);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetMember()
    {
        Debug.Log("=========================== Get Member");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_MEMBER_GUID);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetBosses()
    {
        Debug.Log("=========================== Get Tick Bosses");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_BOSSES_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetTickBoss(int id)
    {
        Debug.Log("=========================== Get Tick Boss");
        ISFSObject isFSObject = new SFSObject();

        isFSObject.PutInt(CmdDefine.ModuleBossGuild.ID, id);
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_TICK_BOSS_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void UnLockBoss(int id)
    {
        Debug.Log("=========================== Get Un Lock");
        ISFSObject isFSObject = new SFSObject();

        isFSObject.PutInt(CmdDefine.ModuleBossGuild.ID, id);
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.UNLOCK_BOSS_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void EndGameBoss(int id, int point)
    {
        Debug.Log("=========================== End Game Boss");
        ISFSObject isFSObject = new SFSObject();

        isFSObject.PutInt(CmdDefine.ModuleBossGuild.ID, id);
        isFSObject.PutInt(CmdDefine.ModuleTickBossGuild.POINT, point);
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.END_GAME_BOSS_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void RewardBoss(int id)
    {
        Debug.Log("=========================== Reward Boss");
        ISFSObject isFSObject = new SFSObject();

        isFSObject.PutInt(CmdDefine.ModuleTickBossGuild.ID, id);
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.REWARD_BOSS_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
