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
            
            ISFSObject data = packet.GetSFSObject("loginOutData");

            short cmdid = (short)data.GetInt(CmdDefine.CMDID);

            switch (cmdid)
            {
                case CmdDefine.LOGIN:
                    HandleLoginF(data);
                    break;
                case CmdDefine.REGISTER:
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
        if (ec == ErrorCode.SUCCESS)
        {           
            LoginGame.instance.RecLogin(new M_Account(data.GetSFSObject("taikhoan")));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }        
    }

    public static void HandleRegister(ISFSObject data)
    {
        Debug.Log("______________HANDLE REGISTER_____________\n" + data.GetDump());
        short ec = data.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            LoginGame.instance.RecRegister(new M_Account(data.GetSFSObject("taikhoan")));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
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

            string noti = (ErrorCode.Codes.ContainsKey(ec)) ? ErrorCode.Codes[ec] : ("ErrorCode: " + ec);

            if (C_Login.instance) C_Login.instance.setNoti(noti);

            if (C_Registry.instance) C_Registry.instance.setNoti(noti);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public static void OnLogOut(BaseEvent evt)
    {
        Debug.Log("Logout server!");

        SceneManager.LoadScene("LoginGame");
    }
}
