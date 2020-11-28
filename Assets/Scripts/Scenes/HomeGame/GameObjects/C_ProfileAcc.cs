using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ProfileAcc : MonoBehaviour
{
    [SerializeField]
    private Text[] txtName = null;
    [SerializeField]
    private Text[] txtPower = null;
    [SerializeField]
    private Text[] txtID = null;
    [SerializeField]
    private Text[] txtLv = null;
    [SerializeField]
    private Image[] imgStatus = null;

    private M_Account acc;

    public void set(M_Account acc)
    {
        this.acc = acc;

        foreach (Text text in txtName) text.text = acc.name;
        foreach (Text text in txtPower) text.text = acc.power + "";
        foreach (Text text in txtID) text.text = "ID: " + acc.id;
        foreach (Text text in txtLv) text.text = "Lv " + acc.lv;

        foreach (Image image in imgStatus) image.color = (acc.status == C_Enum.StatusAccount.On) ? Color.green : Color.red;
    }

    public void OnClick()
    {
        RequestCF.GetDetails(acc.id);
    }
}
