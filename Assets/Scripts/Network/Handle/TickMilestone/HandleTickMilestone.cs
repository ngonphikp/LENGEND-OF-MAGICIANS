using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleTickMilestone
{
    public static void OnResponse(SFSObject sfsObject)
    {
        short cmdid = (short)sfsObject.GetInt(CmdDefine.CMDID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.ENDGAME:
                HandleEndGame(sfsObject);
                break;
            default:

                break;
        }
    }


    public static void HandleEndGame(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE END GAME\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            FightingGame.instance.RecEndGame();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
