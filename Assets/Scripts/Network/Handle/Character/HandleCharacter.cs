using MEC;
using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleChacracter
{
    public static void OnMessage(SFSObject sfsObject)
    {
        short cmdid = (short)sfsObject.GetInt(CmdDefine.CMDID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.ARRANGE:
                handleArrange(sfsObject);
                break;
            case CmdDefine.UPLEVEL:
                handleUpLevel(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void handleUpLevel(SFSObject packet)
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

    public static void handleArrange(SFSObject packet)
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
