using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleCF
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GET_ACCOUNT_GLOBAL:
                HandleGetAccountGlobal(sfsObject);
                break;
            case CmdDefine.CMD.SEND_MESSAGE_GLOBAL:
                HandleMessageGlobal(sfsObject);
                break;
            case CmdDefine.CMD.GET_ACCOUNT_GUILD:
                HandleGetAccountGuild(sfsObject);
                break;
            case CmdDefine.CMD.SEND_MESSAGE_GUILD:
                HandleMessageGuild(sfsObject);
                break;
            default:
                break;
        }
    }

    public static void HandleGetAccountGlobal(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET ACCOUNT GLOBAL\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ChatAndFriend.instance.RecAccount(C_Enum.CFType.Global, GetAccounts(packet));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleMessageGlobal(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SEND MESSAGE GLOBAL\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.MouduleCF.ACCOUNT));
            string message = packet.GetUtfString(CmdDefine.MouduleCF.MESSAGE);

            if(ChatAndFriend.instance) ChatAndFriend.instance.RecMessage(C_Enum.CFType.Global, account, message);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleMessageGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SEND MESSAGE GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.MouduleCF.ACCOUNT));
            string message = packet.GetUtfString(CmdDefine.MouduleCF.MESSAGE);

            if (ChatAndFriend.instance) ChatAndFriend.instance.RecMessage(C_Enum.CFType.Guild, account, message);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleGetAccountGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET ACCOUNT GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ChatAndFriend.instance.RecAccount(C_Enum.CFType.Guild, GetAccounts(packet));
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    private static List<M_Account> GetAccounts(SFSObject packet)
    {
        List<M_Account> accounts = new List<M_Account>();
        ISFSArray arrOn = packet.GetSFSArray(CmdDefine.MouduleCF.ACCOUNTS_ONLINE);
        for (int i = 0; i < arrOn.Count; i++)
        {
            accounts.Add(new M_Account(arrOn.GetSFSObject(i), C_Enum.StatusAccount.On));
        }

        ISFSArray arrOff = packet.GetSFSArray(CmdDefine.MouduleCF.ACCOUNTS_OFFLINE);
        for (int i = 0; i < arrOff.Count; i++)
        {
            accounts.Add(new M_Account(arrOff.GetSFSObject(i), C_Enum.StatusAccount.Off));
        }

        return accounts;
    }
}
