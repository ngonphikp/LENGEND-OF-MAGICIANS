using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ChatCF : MonoBehaviour
{
    [SerializeField]
    private Text txtTitle = null;

    [SerializeField]
    private Transform posTxtMessage = null;
    [SerializeField]
    private GameObject txtMessagePfb = null;

    [SerializeField]
    private InputField ipfMessage = null;

    private C_Enum.CFType type = C_Enum.CFType.None;
    private int id = -1;

    private Dictionary<int, C_TextMessageCF> dicTxtMessage = new Dictionary<int, C_TextMessageCF>();

    public void set(C_Enum.CFType type, int id = -1, string name = "")
    {
        if ((this.type == type && this.type != C_Enum.CFType.Private) || (type == C_Enum.CFType.Private && this.id == id)) return;

        this.type = type;
        this.id = id;

        switch (type)
        {
            case C_Enum.CFType.Global:
                txtTitle.text = "Thế giới";

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);

                if (!dicTxtMessage.ContainsKey(-1))
                {
                    GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
                    txtMessage.name = "Global";
                    dicTxtMessage.Add(-1, txtMessage.GetComponent<C_TextMessageCF>());
                }
                dicTxtMessage[-1].gameObject.SetActive(true);

                break;
            case C_Enum.CFType.Guild:
                txtTitle.text = "Hội: " + GameManager.instance.guild.name;

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);

                if (!dicTxtMessage.ContainsKey(-2))
                {
                    GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
                    txtMessage.name = "Guild";
                    dicTxtMessage.Add(-2, txtMessage.GetComponent<C_TextMessageCF>());
                }
                dicTxtMessage[-2].gameObject.SetActive(true);

                break;
            case C_Enum.CFType.Private:
                txtTitle.text = "Bạn: " + name;

                foreach (C_TextMessageCF item in dicTxtMessage.Values) item.gameObject.SetActive(false);

                if (!dicTxtMessage.ContainsKey(id))
                {
                    GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
                    txtMessage.name = "Private " + id;
                    dicTxtMessage.Add(id, txtMessage.GetComponent<C_TextMessageCF>());                    
                }
                dicTxtMessage[id].gameObject.SetActive(true);

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
                dicTxtMessage[-1].add(Message(type, account, message));
                break;
            case C_Enum.CFType.Guild:
                dicTxtMessage[-2].add(Message(type, account, message));
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
                        txtMessage.name = "Private " + account.id;
                        txtMessage.SetActive(false);
                        dicTxtMessage.Add(account.id, txtMessage.GetComponent<C_TextMessageCF>());
                    }
                    dicTxtMessage[account.id].add(Message(type, account, message));
                }
                break;
            default:
                break;
        }
    }

    public void RemoveMessage(int id)
    {
        if (dicTxtMessage.ContainsKey(id))
        {
            Destroy(dicTxtMessage[id]);
            dicTxtMessage.Remove(id);

            if (this.id == id)
            {
                ChatAndFriend.instance.Global();
                this.id = -1;
            }
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
        if (!dicTxtMessage.ContainsKey(-2))
        {
            GameObject txtMessage = Instantiate(txtMessagePfb, posTxtMessage);
            txtMessage.name = "Guild";
            dicTxtMessage.Add(-2, txtMessage.GetComponent<C_TextMessageCF>());
        }
        dicTxtMessage[-2].set(str);
    }
}
