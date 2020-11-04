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
        Debug.Log("Guid: " + GameManager.instance.guild.id + " / " + GameManager.instance.guild.name + " / " + GameManager.instance.guild.boss + " / " + GameManager.instance.guild.currentMember);
        GameManager.instance.guild.accounts.ForEach(x => Debug.Log(x.id + " / " + x.name));
    }

    public void OutGuild()
    {
        Debug.Log("Out Guild");
        GameManager.instance.account.id_guilds = -1;
        HomeGame.instance.guilds.Clear();

        if (!GameManager.instance.test) RequestGuild.OutGuild();
    }
}
