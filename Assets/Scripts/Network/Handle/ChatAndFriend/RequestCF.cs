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

    public static void SendMessagePrivate(string message, int id)
    {
        Debug.Log("=========================== Send Message Private: " + id);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.SEND_MESSAGE_PRIVATE);

        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, id);

        isFSObject.PutUtfString(CmdDefine.MouduleCF.MESSAGE, message);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetDetails(int id_get)
    {
        Debug.Log("=========================== GET DETAILS");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_DETAILS);

        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, id_get);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void MakeFriend(int id_make)
    {
        Debug.Log("=========================== MAKE FRIEND");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.MAKE_FRIEND);

        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, id_make);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void RemoveFriend(int id_remove)
    {
        Debug.Log("=========================== REMOVE FRIEND");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.REMOVE_FRIEND);

        isFSObject.PutInt(CmdDefine.ModuleAccount.ID, id_remove);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void GetAccountFriend()
    {
        Debug.Log("=========================== Get Account Friend");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_ACCOUNT_FRIEND);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void FindAccountGlobal(string content, bool isCheckId)
    {
        Debug.Log("=========================== Find Account Global");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.FIND_ACCOUNT_GLOBAL);

        isFSObject.PutBool(CmdDefine.MouduleCF.IS_CHECK_ID, isCheckId);
        isFSObject.PutUtfString(CmdDefine.MouduleCF.CONTENT, content);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
