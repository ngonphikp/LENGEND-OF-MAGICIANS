using Sfs2X.Entities.Data;

[System.Serializable]
public class M_Account
{
    public int id;
    public string usename;
    public string password = "";
    public string name = "";

    public int lv = 1;
    public int power = 0;

    public int id_guild = -1;

    public C_Enum.JobGuild job = C_Enum.JobGuild.None;
    public int dediTotal = 0;
    public int dediWeek = 0;

    public M_Account()
    {

    }

    public M_Account(ISFSObject obj)
    {
        if (obj == null) return;

        this.id = obj.GetInt(CmdDefine.ModuleAccount.ID);
        this.usename = obj.GetUtfString(CmdDefine.ModuleAccount.USERNAME);
        this.password = obj.GetUtfString(CmdDefine.ModuleAccount.PASSWORD);
        this.name = obj.GetUtfString(CmdDefine.ModuleAccount.NAME);
        this.lv = obj.GetInt(CmdDefine.ModuleAccount.LV);
        this.power = obj.GetInt(CmdDefine.ModuleAccount.POWER);
        this.job = (C_Enum.JobGuild) obj.GetInt(CmdDefine.ModuleAccount.JOB);
        this.dediTotal = obj.GetInt(CmdDefine.ModuleAccount.DEDITOTAL);
        this.dediWeek = obj.GetInt(CmdDefine.ModuleAccount.DEDIWEEK);
    }

    public void SetJob(C_Enum.JobGuild job)
    {
        this.job = job;
        dediTotal = 0;
        dediWeek = 0;

        if (job == C_Enum.JobGuild.None) id_guild = -1;
    }
}
