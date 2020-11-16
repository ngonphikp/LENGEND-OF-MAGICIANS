using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildGame : MonoBehaviour
{
    public static GuildGame instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    private void OnEnable()
    {
        Debug.Log("Guid: " + GameManager.instance.guild.id + " / " + GameManager.instance.guild.name + " / " + GameManager.instance.guild.master + " / " + GameManager.instance.guild.accounts.Count);
        GameManager.instance.guild.accounts.ForEach(x => Debug.Log(x.id + " / " + x.name));
    }

    public void OutGuild()
    {
        Debug.Log("Out Guild");        

        RequestGuild.OutGuild();
    }

    public void RecOutGuild()
    {
        GameManager.instance.account.id_guild = -1;
    }

    public void ChangeMaster(int master)
    {
        RequestGuild.ChangeMaster(master);
    }
}
