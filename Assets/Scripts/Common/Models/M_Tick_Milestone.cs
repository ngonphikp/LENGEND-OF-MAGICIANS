using Sfs2X.Entities.Data;
using System.Data;

[System.Serializable]
public class M_Tick_Milestone
{
    public int id_ac;
    public int id_ml;
    public int star;

    public M_Tick_Milestone()
    {
            
    }

    public M_Tick_Milestone(int id, int id_ac, int id_ml, int star)
    {
        this.id_ac = id_ac;
        this.id_ml = id_ml;
        this.star = star;
    }

    public M_Tick_Milestone(ISFSObject obj)
    {
        if (obj == null) return;

        this.id_ac = obj.GetInt(CmdDefine.ModuleTickMilestone.ID_AC);
        this.id_ml = obj.GetInt(CmdDefine.ModuleTickMilestone.ID_ML);
        this.star = obj.GetInt(CmdDefine.ModuleTickMilestone.STAR);
    }
}
