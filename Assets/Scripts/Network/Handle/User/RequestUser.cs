using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestUser
{
    private static string MODULE = CmdDefine.MODULE_USER;

    public static void GetInfo(int id)
    {
        Debug.Log("=========================== Get Info: " + id);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.GETINFO);
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

    public static void Selection(string tennhanvat, string id_cfg)
    {
        Debug.Log("=========================== Selection");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.SELECTION);
        isFSObject.PutInt("id", GameManager.instance.taikhoan.id);
        isFSObject.PutUtfString("name", tennhanvat);
        isFSObject.PutUtfString("id_cfg", id_cfg);
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
        isFSObject.PutInt(CmdDefine.CMDID, CmdDefine.TAVERN);

        isFSObject.PutInt("type_tavern", (int)type);
        isFSObject.PutInt("id", GameManager.instance.taikhoan.id);
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
