using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class HandleCF
{
    public static void OnResponse(SFSObject sfsObject)
    {
        int cmdid = (short)sfsObject.GetInt(CmdDefine.CMD_ID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.CMD.GET_ACCOUNT_GLOBAL:
            case CmdDefine.CMD.GET_ACCOUNT_FRIEND:
            case CmdDefine.CMD.GET_ACCOUNT_GUILD:
            case CmdDefine.CMD.FIND_ACCOUNT_GLOBAL:
                HandleGetAccount(sfsObject);
                break;
            case CmdDefine.CMD.SEND_MESSAGE_GLOBAL:
                HandleMessageGlobal(sfsObject);
                break;
            case CmdDefine.CMD.SEND_MESSAGE_GUILD:
                HandleMessageGuild(sfsObject);
                break;
            case CmdDefine.CMD.GET_DETAILS:
                HandleGetDetails(sfsObject);
                break;
            case CmdDefine.CMD.MAKE_FRIEND:
                HandleMakeFriend(sfsObject);
                break;
            case CmdDefine.CMD.REMOVE_FRIEND:
                HandleRemoveFriend(sfsObject);
                break;
            case CmdDefine.CMD.SEND_MESSAGE_PRIVATE:
                HandleMessagePrivate(sfsObject);
                break;
            default:
                break;
        }
    }

    public static void HandleGetAccount(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET ACCOUNT\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ISFSArray idOnls = packet.GetSFSArray(CmdDefine.MouduleCF.ID_ONLINES);

            List<M_Account> accounts = new List<M_Account>();
            ISFSArray arr = packet.GetSFSArray(CmdDefine.MouduleCF.ACCOUNTS);
            for (int i = 0; i < arr.Count; i++)
            {
                M_Account account = new M_Account(arr.GetSFSObject(i));
                if (account.id != GameManager.instance.account.id)
                {
                    account.status = (idOnls.Contains(account.id)) ? C_Enum.StatusAccount.On : C_Enum.StatusAccount.Off;

                    accounts.Add(account);
                }
            }

            ChatAndFriend.instance.RecAccount(accounts);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleMessageGlobal(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SEND MESSAGE GLOBAL\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.MouduleCF.ACCOUNT));
            string message = packet.GetUtfString(CmdDefine.MouduleCF.MESSAGE);

            if(ChatAndFriend.instance) ChatAndFriend.instance.RecMessage(C_Enum.CFType.Global, account, message);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleMessageGuild(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SEND MESSAGE GUILD\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.MouduleCF.ACCOUNT));
            string message = packet.GetUtfString(CmdDefine.MouduleCF.MESSAGE);

            if (ChatAndFriend.instance) ChatAndFriend.instance.RecMessage(C_Enum.CFType.Guild, account, message);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleGetDetails(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET DETAILS\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.ModuleAccount.ACCOUNT));
            List<M_Character> lstCharacter = new List<M_Character>();
            ISFSArray characters = packet.GetSFSArray(CmdDefine.ModuleAccount.CHARACTERS);
            for (int i = 0; i < characters.Size(); i++)
            {
                M_Character character = new M_Character(characters.GetSFSObject(i), C_Enum.ReadType.SERVER);
                character.UpdateById();
                character.UpdateLevel();
                character.current_hp = character.max_hp;
                character.type = C_Enum.CharacterType.Hero;
                lstCharacter.Add(character);
            }

            int id_guild = -1;
            string name_guild = "";

            if (account.job != C_Enum.JobGuild.None)
            {
                id_guild = packet.GetInt(CmdDefine.ModuleGuild.ID);
                name_guild = packet.GetUtfString(CmdDefine.ModuleGuild.NAME);
            }

            bool isFriend = packet.GetBool(CmdDefine.MouduleCF.IS_FRIEND);
            M_Details details = new M_Details();

            details.account = account;
            details.characters = lstCharacter;
            details.is_friend = isFriend;
            details.id_guild = id_guild;
            details.name_guild = name_guild;


            ChatAndFriend.instance.RecDetails(details);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleMakeFriend(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE MAKE FRIEND\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ChatAndFriend.instance.RecMakeFriend();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleRemoveFriend(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE REMOVE FRIEND\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            ChatAndFriend.instance.RecRemoveFriend();
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
        }
    }

    public static void HandleMessagePrivate(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SEND MESSAGE PRIVATE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == CmdDefine.ErrorCode.SUCCESS)
        {
            M_Account account = new M_Account(packet.GetSFSObject(CmdDefine.MouduleCF.ACCOUNT));
            string message = packet.GetUtfString(CmdDefine.MouduleCF.MESSAGE);

            if (ChatAndFriend.instance) ChatAndFriend.instance.RecMessage(C_Enum.CFType.Private, account, message);
        }
        else
        {
            Debug.Log(CmdDefine.ErrorCode.Errors.ContainsKey(ec) ? CmdDefine.ErrorCode.Errors[ec] : ("Error Code" + ec));
            if(ec == CmdDefine.ErrorCode.UNFRIENDED) ChatAndFriend.instance.RecRemoveFriend();
        }
    }
}
