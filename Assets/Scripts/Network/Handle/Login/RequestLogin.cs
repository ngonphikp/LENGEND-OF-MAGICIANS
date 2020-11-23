using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestLogin
{
    public static void Login(string username, string password)
    {
        Debug.Log("=========================== Login");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.LOGIN);

        isFSObject.PutUtfString(CmdDefine.ModuleAccount.USERNAME, username);
        isFSObject.PutUtfString(CmdDefine.ModuleAccount.PASSWORD, password);

        var packet = new LoginRequest("", "", ConfigConnection.Zone, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void Register(string username, string password)
    {
        Debug.Log("=========================== Registry");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.REGISTER);

        isFSObject.PutUtfString(CmdDefine.ModuleAccount.USERNAME, username);
        isFSObject.PutUtfString(CmdDefine.ModuleAccount.PASSWORD, password);

        var packet = new LoginRequest("", "", ConfigConnection.Zone, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void Logout()
    {
        Debug.Log("=========================== Logout");
        SmartFoxConnection.send(new LogoutRequest());
    }
}
