using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ChatCF : MonoBehaviour
{
    [SerializeField]
    private Text txtTitle = null;
    [SerializeField]
    private C_TextMessageCF txtGlobal = null;
    [SerializeField]
    private C_TextMessageCF txtGuild = null;

    [SerializeField]
    private InputField ipfMessage = null;

    private C_Enum.CFType type;

    public void set(C_Enum.CFType type)
    {
        if (this.type == type) return;

        this.type = type;

        switch (type)
        {
            case C_Enum.CFType.None:
                break;
            case C_Enum.CFType.Global:
                txtTitle.text = "Global";

                txtGuild.gameObject.SetActive(false);
                txtGlobal.gameObject.SetActive(true); 
                
                break;
            case C_Enum.CFType.Guild:
                txtTitle.text = "Guild: " + GameManager.instance.guild.name;

                txtGlobal.gameObject.SetActive(false);
                txtGuild.gameObject.SetActive(true);

                break;
            case C_Enum.CFType.Friend:
                break;
            default:
                break;
        }        
    }

    public void SendMessage()
    {
        if (ipfMessage.text == "") return; 

        switch (type)
        {
            case C_Enum.CFType.None:
                break;
            case C_Enum.CFType.Global:
                RequestCF.SendMessageGlobal(ipfMessage.text);
                break;
            case C_Enum.CFType.Guild:
                RequestCF.SendMessageGuild(ipfMessage.text);
                break;
            case C_Enum.CFType.Friend:
                break;
            default:
                break;
        }

        ipfMessage.text = "";
    }

    public void AddMessage(C_Enum.CFType type, M_Account account, string message)
    {
        switch (type)
        {
            case C_Enum.CFType.None:
                break;
            case C_Enum.CFType.Global:
                txtGlobal.add(Message(type, account, message));
                break;
            case C_Enum.CFType.Guild:
                txtGuild.add(Message(type, account, message));
                break;
            case C_Enum.CFType.Friend:
                break;
            default:
                break;
        }
    }

    private string Message(C_Enum.CFType type, M_Account account, string message)
    {
        string rs = "";
        if (account.id == GameManager.instance.account.id) rs += "Tôi: ";
        else rs += account.name + ": ";

        rs += message + "\n\n";

        return rs;
    }

    public void SetContentGuild(string str)
    {
        txtGuild.set(str);
    }
}
