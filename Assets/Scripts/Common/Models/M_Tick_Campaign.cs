using Sfs2X.Entities.Data;
using System.Data;

[System.Serializable]
public class M_Tick_Campaign
{
    public int id_ac;
    public int id_ml;
    public int star;

    public M_Tick_Campaign()
    {
            
    }

    public M_Tick_Campaign(ISFSObject obj)
    {
        if (obj == null) return;

        this.id_ac = obj.GetInt(CmdDefine.ModuleAccount.ID);
        this.id_ml = obj.GetInt(CmdDefine.ModuleTickCampaign.ID_CAMPAIGN);
        this.star = obj.GetInt(CmdDefine.ModuleTickCampaign.STAR);
    }
}
