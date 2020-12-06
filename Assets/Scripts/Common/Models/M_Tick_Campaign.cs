using Sfs2X.Entities.Data;
using System.Data;

[System.Serializable]
public class M_Tick_Campaign
{
    public int id;
    public int star;

    public M_Tick_Campaign()
    {
            
    }

    public M_Tick_Campaign(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt(CmdDefine.ModuleTickCampaign.ID);
        this.star = obj.GetInt(CmdDefine.ModuleTickCampaign.STAR);
    }
}
