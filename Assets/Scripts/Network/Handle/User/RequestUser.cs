using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestUser
{
    private static string MODULE = CmdDefine.Module.MODULE_USER;

    public static void GetInfo(int id)
    {
        Debug.Log("=========================== Get Info: " + id);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GETINFO);
        isFSObject.PutInt(CmdDefine.ModuleUser.ID, id);

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

    public static void Selection(string name, string id_cfg)
    {
        Debug.Log("=========================== Selection");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.SELECTION);
        isFSObject.PutInt(CmdDefine.ModuleUser.ID, GameManager.instance.account.id);
        isFSObject.PutUtfString(CmdDefine.ModuleUser.NAME, name);
        isFSObject.PutUtfString(CmdDefine.ModuleCharacter.ID_CFG, id_cfg);
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

    public static void Tavern(C_Enum.CardType type)
    {
        Debug.Log("=========================== Tavern");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.TAVERN);

        isFSObject.PutInt(CmdDefine.ModuleUser.TYPE_TAVERN, (int)type);
        isFSObject.PutInt(CmdDefine.ModuleUser.ID, GameManager.instance.account.id);
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
