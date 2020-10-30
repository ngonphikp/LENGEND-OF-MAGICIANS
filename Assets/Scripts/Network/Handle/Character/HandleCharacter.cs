using MEC;
using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleChacracter
{
    public static void OnResponse(SFSObject sfsObject)
    {
        short cmdid = (short)sfsObject.GetInt(CmdDefine.CMDID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.ARRANGE:
                HandleArrange(sfsObject);
                break;
            case CmdDefine.UPLEVEL:
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
        if (ec == ErrorCode.SUCCESS)
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
        if (ec == ErrorCode.SUCCESS)
        {
            Timing.RunCoroutine(ArrangeGame.instance._RecArrange());
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
