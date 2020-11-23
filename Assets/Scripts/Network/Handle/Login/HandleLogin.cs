using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLogin 
{
    public static void OnLoginSuccess(BaseEvent evt)
    {
        Debug.Log("Login server thành công!");
        try
        {
            SFSObject packet = (SFSObject)evt.Params["data"];

            Debug.Log(packet.GetDump());
            
            ISFSObject data = packet.GetSFSObject(CmdDefine.ModuleAccount.LOGIN_OUT_DATA);

            int cmdid = (short)data.GetInt(CmdDefine.CMD_ID);

            switch (cmdid)
            {
                case CmdDefine.CMD.LOGIN:
                    HandleLoginF(data);
                    break;
                case CmdDefine.CMD.REGISTER:
                    HandleRegister(data);
                    break;
                default:

                    break;
            }            
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public static void HandleLoginF(ISFSObject data)
    {
        Debug.Log("______________HANDLE LOGIN_____________\n" + data.GetDump());
        short ec = data.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {           
            LoginGame.instance.RecLogin(new M_Account(data.GetSFSObject(CmdDefine.ModuleAccount.ACCOUNT), C_Enum.StatusAccount.On));
        }
        else
        {
            string noti = CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec);

            if (C_Login.instance) C_Login.instance.setNoti(noti);
            if (C_Registry.instance) C_Registry.instance.setNoti(noti);

            RequestLogin.Logout();
        }        
    }

    public static void HandleRegister(ISFSObject data)
    {
        Debug.Log("______________HANDLE REGISTER_____________\n" + data.GetDump());
        short ec = data.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            LoginGame.instance.RecRegister(new M_Account(data.GetSFSObject(CmdDefine.ModuleAccount.ACCOUNT), C_Enum.StatusAccount.On));
        }
        else
        {
            string noti = CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec);
            if (C_Login.instance) C_Login.instance.setNoti(noti);
            if (C_Registry.instance) C_Registry.instance.setNoti(noti);

            RequestLogin.Logout();
        }
    }

    public static void OnLoginError(BaseEvent evt)
    {
        Debug.Log("Login server error!");

        short ec = (short)evt.Params["errorCode"];
        var message = evt.Params["errorMessage"];
        try
        {
            Debug.Log("ErrorCode: " + ec);
            Debug.Log("Message: " + message);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public static void OnLogOut(BaseEvent evt)
    {
        Debug.Log("Logout server!");

        if(SceneManager.GetActiveScene().name != "LoginGame") SceneManager.LoadScene("LoginGame");
    }
}
