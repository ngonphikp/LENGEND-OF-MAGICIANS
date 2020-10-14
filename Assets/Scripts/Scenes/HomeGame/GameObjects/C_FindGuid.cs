﻿using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_FindGuid : MonoBehaviour
{
    [SerializeField]
    private GameObject popUp = null;
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject cardGuildPrb = null;

    [SerializeField]
    private InputField ipfKeyS = null;

    private Dictionary<string, C_CardGuild> cardDic = new Dictionary<string, C_CardGuild>();

    public IEnumerator<float> _set(List<M_Guild> guilds)
    {
        cardDic.Clear();
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < guilds.Count; i++)
        {
            C_CardGuild cardGuild = Instantiate(cardGuildPrb, content).GetComponent<C_CardGuild>();
            Timing.RunCoroutine(cardGuild._set(guilds[i]));

            cardDic.Add(guilds[i].name, cardGuild);
        }

        popUp.SetActive(true);

        yield return Timing.WaitForOneFrame;
    }

    public void Find()
    {
        Debug.Log("Find: " + ipfKeyS.text);

        foreach (var item in cardDic)
        {
            if (item.Key.ToUpper().StartsWith(ipfKeyS.text.ToUpper())) item.Value.gameObject.SetActive(true);
            else item.Value.gameObject.SetActive(false);
        }
    }
}
