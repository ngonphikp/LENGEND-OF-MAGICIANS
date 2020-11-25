using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCF : MonoBehaviour
{
    private static string MODULE = CmdDefine.Module.MODULE_CHAT_AND_FRIEND;

    public static void GetAccountGlobal()
    {
        Debug.Log("=========================== Get Account Global");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_ACCOUNT_GLOBAL);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void SendMessageGlobal(string message)
    {
        Debug.Log("=========================== Send Message Global");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.SEND_MESSAGE_GLOBAL);

        isFSObject.PutUtfString(CmdDefine.MouduleCF.MESSAGE, message);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetAccountGuild()
    {
        Debug.Log("=========================== Get Account Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_ACCOUNT_GUILD);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void SendMessageGuild(string message)
    {
        Debug.Log("=========================== Send Message Guild");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.SEND_MESSAGE_GUILD);

        isFSObject.PutUtfString(CmdDefine.MouduleCF.MESSAGE, message);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
