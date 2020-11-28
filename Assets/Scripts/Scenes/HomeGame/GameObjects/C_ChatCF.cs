using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ChatCF : MonoBehaviour
{
    [SerializeField]
    private Text txtTitle = null;
    [SerializeField]
    private C_TextMessageCF txtGlobal = null;       // -1
    [SerializeField]
    private C_TextMessageCF txtGuild = null;        // -2

    [SerializeField]
    private Transform posTxtMessage = null;
    [SerializeField]
    private GameObject txtMessagePfb = null;

    [SerializeField]
    private InputField ipfMessage = null;

    private C_Enum.CFType type;
    private int id = -1;

    private Dictionary<int, C_TextMessageCF> dicTxtMessage = new Dictionary<int, C_TextMessageCF>();

    private void Awake()
    {
        dicTxtMessage.Add(-1, txtGlobal);
        dicTxtMessage.Add(-2, txtGuild);
    }

    public void set(C_Enum.CFType type, int id = -1, string name = "")
    {
        if (this.type == type && this.id == id) return;

        this.type = type;
        this.id = id;

        switch (type)
        {
            case C_Enum.CFType.Global:
                txtTitle.text = "Thế giới";

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);
                dicTxtMessage[-1].gameObject.SetActive(true);
                
                break;
            case C_Enum.CFType.Guild:
                txtTitle.text = "Hội: " + GameManager.instance.guild.name;

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);
                dicTxtMessage[-2].gameObject.SetActive(true);

                break;
            case C_Enum.CFType.Private:
                txtTitle.text = "Bạn: " + name;

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);

                if (dicTxtMessage.ContainsKey(id))
                {
                    dicTxtMessage[id].gameObject.SetActive(true);
                }
                else
                {
                    GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
                    txtMessage.name = "Private " + id;
                    dicTxtMessage.Add(id, txtMessage.GetComponent<C_TextMessageCF>());
                }

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
            case C_Enum.CFType.Global:
                RequestCF.SendMessageGlobal(ipfMessage.text);
                break;
            case C_Enum.CFType.Guild:
                RequestCF.SendMessageGuild(ipfMessage.text);
                break;
            case C_Enum.CFType.Private:
                RequestCF.SendMessagePrivate(ipfMessage.text, id);
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
            case C_Enum.CFType.Global:
                txtGlobal.add(Message(type, account, message));
                break;
            case C_Enum.CFType.Guild:
                txtGuild.add(Message(type, account, message));
                break;
            case C_Enum.CFType.Private:
                //Nếu là người gửi
                if (account.id == GameManager.instance.account.id)
                {
                    dicTxtMessage[id].add(Message(type, account, message));
                }
                // Nếu là ngưười nhận
                else
                {
                    if (!dicTxtMessage.ContainsKey(account.id))
                    {
                        GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
                        txtMessage.name = "Private " + id;
                        dicTxtMessage.Add(id, txtMessage.GetComponent<C_TextMessageCF>());
                    }
                    dicTxtMessage[account.id].add(Message(type, account, message));
                }
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
