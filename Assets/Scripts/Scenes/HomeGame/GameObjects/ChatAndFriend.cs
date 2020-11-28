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
    [SerializeField]
    private C_Details c_details = null;
    [SerializeField]
    private GameObject FindObj = null;

    private C_Enum.CFType type = C_Enum.CFType.Global;

    private int id_guild = -5;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ShowPopup()
    {
        switch (type)
        {
            case C_Enum.CFType.Global:
                Global();
                break;
            case C_Enum.CFType.Guild:
                Guild();
                break;
            default:
                Global();
                break;
        }
        popUp.SetActive(true);
    }

    public void btnReset()
    {
        RequestCF.GetAccountGlobal();
    }

    public void Global()
    {
        this.type = C_Enum.CFType.Global;
        C_Util.ActiveGO(true, FindObj);

        RequestCF.GetAccountGlobal();
    }

    public void RecAccount(List<M_Account> accounts)
    {
        friendCF.set(accounts);
        chatCF.set(type);
    }

    public void RecMessage(C_Enum.CFType type, M_Account account, string message)
    {
        chatCF.AddMessage(type, account, message);
    }

    public void Guild()
    {
        this.type = C_Enum.CFType.Guild;

        C_Util.ActiveGO(false, FindObj);

        if (id_guild != GameManager.instance.account.id_guild) chatCF.SetContentGuild("");

        id_guild = GameManager.instance.account.id_guild;

        if (id_guild == -1)
        {
            Debug.Log("Bạn cần vào hội!");
            return;
        }

        RequestCF.GetAccountGuild();
    }

    public void Friend()
    {
        C_Util.ActiveGO(false, FindObj);

        RequestCF.GetAccountFriend();
    }

    public void RecDetails(M_Details details)
    {
        c_details.set(details);
    }

    public void RecMakeFriend()
    {
        c_details.RecMakeFriend();
    }

    public void RecRemoveFriend()
    {
        c_details.RecRemoveFriend();
    }

    public void ChatPrivate(int id, string name)
    {
        this.type = C_Enum.CFType.Private;
        chatCF.set(type, id, name);
    }
}
