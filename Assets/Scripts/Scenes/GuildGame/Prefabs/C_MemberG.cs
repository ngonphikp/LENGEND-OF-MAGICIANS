using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_MemberG : MonoBehaviour
{
    [SerializeField]
    private Text txtName = null;
    [SerializeField]
    private Text txtJob = null;
    [SerializeField]
    private Text txtLv = null;
    [SerializeField]
    private Text txtDediTotal = null;
    [SerializeField]
    private Text txtDediWeek = null;
    [SerializeField]
    private Text txtPower = null;

    public void set(M_Account account)
    {
        txtName.text = account.name;

        switch (account.job)
        {
            case C_Enum.JobGuild.None:
                txtJob.text = "";
                break;
            case C_Enum.JobGuild.Normal:
                txtJob.text = "Thành viên";
                break;
            case C_Enum.JobGuild.Master:
                txtJob.text = "Hội trưởng";
                break;
            default:
                break;
        }
        
        txtLv.text = account.lv + "";
        txtDediTotal.text = account.dediTotal + "";
        txtDediWeek.text = account.dediWeek + "";
        txtPower.text = account.power + "";
    }
}
