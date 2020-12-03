using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleCampaign
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GET_TICKS_CAMPAIGN:
                HandleGetTicks(sfsObject);
                break;
            case CmdDefine.CMD.END_GAME_CAMPAIGN:
                HandleEndGame(sfsObject);
                break;
            default:

                break;
        }
    }

    public static void HandleGetTicks(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET TISCKS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            List<M_Tick_Campaign> ticks = new List<M_Tick_Campaign>();
            ISFSArray arr = packet.GetSFSArray(CmdDefine.ModuleTickCampaign.TICKS);
            for (int i = 0; i < arr.Count; i++)
            {
                ticks.Add(new M_Tick_Campaign(arr.GetSFSObject(i)));
            }
            CampaignGame.instance.RecTicks(ticks);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
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
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }
}
