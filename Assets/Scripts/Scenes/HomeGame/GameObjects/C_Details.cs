using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_Details : MonoBehaviour
{
    [SerializeField]
    private Text txtName = null;
    [SerializeField]
    private Text txtGuild = null;
    [SerializeField]
    private Text txtPower = null;
    [SerializeField]
    private Text txtID = null;
    [SerializeField]
    private Text txtLv = null;

    [SerializeField]
    private GameObject btnMakeF = null;
    [SerializeField]
    private GameObject btnRemoveF = null;
    [SerializeField]
    private Button btnChatPrivate = null;
    [SerializeField]
    private Button btnDuel = null;

    [SerializeField]
    private C_LstCharacter lstCharacter = null;

    private M_Details data;

    public void set(M_Details details)
    {
        this.data = details;
        txtName.text = data.account.name;
        txtGuild.text = data.name_guild;
        txtPower.text = data.account.power + "";
        txtID.text = "ID: " + data.account.id;
        txtLv.text = "Lv " + data.account.lv;

        lstCharacter.set(details.characters);

        C_Util.ActiveGO(data.is_friend, btnRemoveF);
        C_Util.ActiveGO(!data.is_friend, btnMakeF);

        btnChatPrivate.interactable = data.is_friend;
        btnDuel.interactable = data.is_friend;

        this.gameObject.SetActive(true);
    }

    public void MakeFriend()
    {
        RequestCF.MakeFriend(data.account.id);
    }

    public void RemoveFriend()
    {
        RequestCF.RemoveFriend(data.account.id);        
    }

    public void RecMakeFriend()
    {
        data.is_friend = true;

        C_Util.ActiveGO(data.is_friend, btnRemoveF);
        C_Util.ActiveGO(!data.is_friend, btnMakeF);

        btnChatPrivate.interactable = data.is_friend;
        btnDuel.interactable = data.is_friend;
    }

    public void RecRemoveFriend()
    {
        data.is_friend = false;

        C_Util.ActiveGO(data.is_friend, btnRemoveF);
        C_Util.ActiveGO(!data.is_friend, btnMakeF);

        btnChatPrivate.interactable = data.is_friend;
        btnDuel.interactable = data.is_friend;

        ChatAndFriend.instance.RemoveMessagePrivate(data.account.id);
    }

    public void ChatPrivate()
    {
        this.gameObject.SetActive(false);
        ChatAndFriend.instance.ChatPrivate(data.account.id, data.account.name);
    }

    public void Duel()
    {
        GameManager.instance.isAttack = true;
        GameManager.instance.battleType = C_Enum.BattleType.Duel;

        M_Milestone milestone = new M_Milestone();
        milestone.maxTurn = 20;
        milestone.name = "Khiêu chiến";
        milestone.lstCharacter = data.characters;

        GameManager.instance.milestone = milestone;

        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        SceneManager.LoadSceneAsync("PlayGame");
    }
}
