using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestTickMilestone 
{
    private static string MODULE = CmdDefine.Module.MODULE_TICK_MILESTONE;

    public static void EndGame(int id_ac, int id_ml, int star, bool isSave = true)
    {
        Debug.Log("=========================== End Game: Save: " + isSave);
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.ENDGAME);

        isFSObject.PutInt(CmdDefine.ModuleTickMilestone.ID_AC, id_ac);
        isFSObject.PutInt(CmdDefine.ModuleTickMilestone.ID_ML, id_ml);
        isFSObject.PutInt(CmdDefine.ModuleTickMilestone.STAR, star);

        isFSObject.PutBool(CmdDefine.ModuleTickMilestone.IS_SAVE, isSave);
        var packet = new ExtensionRequest(MODULE, isFSObject);
        if (SmartFoxConnection.isAlready())
        {
            SmartFoxConnection.send(packet);
        }
        else
        {
            SmartFoxConnection.Init();
            SmartFoxConnection.send(packet);
        }
    }
}
