using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using UnityEngine;

public class RequestCampaign
{
    private static string MODULE = CmdDefine.Module.MODULE_CAMPAIGN;

    public static void GetTicks()
    {
        Debug.Log("=========================== Get Ticks");
        ISFSObject isFSObject = new SFSObject();

        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.GET_TICKS_CAMPAIGN);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }

    public static void EndGame(int id_campaign, int star)
    {
        Debug.Log("=========================== End Game");
        ISFSObject isFSObject = new SFSObject();
        isFSObject.PutInt(CmdDefine.CMD_ID, CmdDefine.CMD.END_GAME_CAMPAIGN);

        isFSObject.PutInt(CmdDefine.ModuleTickCampaign.ID_CAMPAIGN, id_campaign);
        isFSObject.PutInt(CmdDefine.ModuleTickCampaign.STAR, star);

        var packet = new ExtensionRequest(MODULE, isFSObject);
        SmartFoxConnection.send(packet);
    }
}
