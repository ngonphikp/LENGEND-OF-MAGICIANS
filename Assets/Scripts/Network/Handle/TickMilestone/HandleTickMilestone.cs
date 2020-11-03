using Sfs2X.Entities.Data;
using UnityEngine;

public class HandleTickMilestone
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.ENDGAME:
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
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            FightingGame.instance.RecEndGame();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
