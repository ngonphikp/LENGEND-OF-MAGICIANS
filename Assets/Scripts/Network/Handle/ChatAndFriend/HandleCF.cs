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
            List<M_Account> accounts = new List<M_Account>();
            ISFSArray arrOn = packet.GetSFSArray(CmdDefine.MouduleCF.ACCOUNTS_ONLINE);
            for (int i = 0; i < arrOn.Count; i++)
            {
                accounts.Add(new M_Account(arrOn.GetSFSObject(i)));
            }

            ISFSArray arrOff = packet.GetSFSArray(CmdDefine.MouduleCF.ACCOUNTS_OFFLINE);
            for (int i = 0; i < arrOff.Count; i++)
            {
                accounts.Add(new M_Account(arrOff.GetSFSObject(i)));
            }

            ChatAndFriend.instance.RecAccountGlobal(accounts);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
