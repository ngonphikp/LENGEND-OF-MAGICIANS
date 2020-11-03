using MEC;
using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleChacracter
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.ARRANGE:
                HandleArrange(sfsObject);
                break;
            case CmdDefine.CMD.UPLEVEL:
                HandleUplevel(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void HandleUplevel(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE UP LEVEL\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            InforGame.instance.RecUpLevel();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void HandleArrange(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE ARRANGE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            Timing.RunCoroutine(ArrangeGame.instance._RecArrange());
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
