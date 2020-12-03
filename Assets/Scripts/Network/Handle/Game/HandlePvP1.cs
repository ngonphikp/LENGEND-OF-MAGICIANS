using MEC;
using Sfs2X.Entities.Data;
using UnityEngine;

public class HandlePvP
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.START_PVP:
                HandleStart(sfsObject);
                break;
            case CmdDefine.CMD.CANCLE_PVP:
                HandleCancle(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void HandleStart(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE START\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            PvP.instance.RecStart();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleCancle(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE CANCLE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            PvP.instance.RecCancle();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }
}
