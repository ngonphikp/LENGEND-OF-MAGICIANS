using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAndFriend : MonoBehaviour
{
    public static ChatAndFriend instance = null;

    [SerializeField]
    private C_LstProfileAcc profileAccs = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Global()
    {
        RequestCF.GetAccountGlobal();
    }

    public void RecAccountGlobal(List<M_Account> accounts)
    {
        profileAccs.set(accounts);
    }
}
