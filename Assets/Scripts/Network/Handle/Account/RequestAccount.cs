using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestAccount
{
    private static string MODULE = CmdDefine.Module.MODULE_ACCOUNT;

    public static void GetInfo(int id)
    {
        Debug.Log("=========================== Get Info: " + id);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GETINFO);

        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, id);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void Selection(string name, string id_cfg)
    {
        Debug.Log("=========================== Selection");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.SELECTION);

        isFSObject.PutUtfString(CmdDefine.ModuleAccount.NAME, name);
        isFSObject.PutUtfString(CmdDefine.ModuleCharacter.ID_CFG, id_cfg);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void Tavern(C_Enum.CardType type)
    {
        Debug.Log("=========================== Tavern");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.TAVERN);

        isFSObject.PutInt(CmdDefine.ModuleAccount.TYPE_TAVERN, (int)type);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
