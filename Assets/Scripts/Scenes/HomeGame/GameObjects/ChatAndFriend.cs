using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAndFriend : MonoBehaviour
{
    public static ChatAndFriend instance = null;

    [SerializeField]
    private GameObject popUp = null;
    [SerializeField]
    private C_FriendCF friendCF = null;
    [SerializeField]
    private C_ChatCF chatCF = null;

    private C_Enum.CFType type = C_Enum.CFType.None;

    private int id_guild = -5;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ShowPopup()
    {
        switch (type)
        {
            case C_Enum.CFType.None:
                Global();
                break;
            case C_Enum.CFType.Global:
                Global();
                break;
            case C_Enum.CFType.Guild:
                Guild();
                break;
            case C_Enum.CFType.Friend:
                break;
            default:
                break;
        }
        popUp.SetActive(true);
    }

    public void Global()
    {
        RequestCF.GetAccountGlobal();
    }

    public void RecAccount(C_Enum.CFType type, List<M_Account> accounts)
    {
        this.type = type;
        friendCF.set(accounts);
        chatCF.set(type);
    }

    public void RecMessage(C_Enum.CFType type, M_Account account, string message)
    {
        chatCF.AddMessage(type, account, message);
    }

    public void Guild()
    {
        if (id_guild != GameManager.instance.account.id_guild) chatCF.SetContentGuild("");

        id_guild = GameManager.instance.account.id_guild;

        if (id_guild == -1)
        {
            Debug.Log("Bạn cần vào hội!");
            return;
        }

        RequestCF.GetAccountGuild();
    }
}
