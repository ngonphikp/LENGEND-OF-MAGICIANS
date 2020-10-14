using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeGame : MonoBehaviour
{
    public static HomeGame instance = null;

    [Header("Bag Hero")]
    [SerializeField]
    private Transform listHeroAc = null;
    [SerializeField]
    private GameObject heroAcEl = null;
    [SerializeField]
    private Transform bagHero = null;
    [SerializeField]
    private GameObject bagEl = null;

    [Header("Music")]
    [SerializeField]
    private Scrollbar sbVolume = null;
    [SerializeField]
    private Text txtVolume = null;
    [SerializeField]
    private GameObject btnMuteOn = null;
    [SerializeField]
    private GameObject btnMuteOff = null;

    [Header("Find Guild")]
    [SerializeField]
    private C_FindGuid findGuid = null;    

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void OutGame()
    {
        if (GameManager.instance.test)
        {
            SceneManager.LoadScene("LoginGame");
            GameManager.instance.test = false;
        }
        else LoginSendUtil.sendLogout();
    }

    private void OnEnable()
    {
        sbVolume.value = SoundManager.instance.volume;
        txtVolume.text = (int)(SoundManager.instance.volume * 100) + "";
        btnMuteOn.SetActive(!SoundManager.instance.isMute);
        btnMuteOff.SetActive(SoundManager.instance.isMute);

        Timing.RunCoroutine(_FilterListHero());
        Timing.RunCoroutine(_LoadBagHero());
    }

    private IEnumerator<float> _FilterListHero()
    {
        foreach (Transform child in listHeroAc)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            if (GameManager.instance.nhanVats[i].idx != -1)
            {
                C_CharacterAcEl heroAc = Instantiate(heroAcEl, listHeroAc).GetComponent<C_CharacterAcEl>();
                Timing.RunCoroutine(heroAc._set(i));
            }
        }

        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<float> _LoadBagHero()
    {
        foreach (Transform child in bagHero)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.instance.nhanVats.Count; i++)
        {
            C_BagEl heroBag = Instantiate(bagEl, bagHero).GetComponent<C_BagEl>();
            Timing.RunCoroutine(heroBag._set(i));
        }

        yield return Timing.WaitForOneFrame;
    }

    public void ChangeVolume()
    {
        txtVolume.text = (int)(sbVolume.value * 100) + "";

        SoundManager.instance.ChangeVolume(sbVolume.value);
    }

    public void ChangeMute()
    {
        SoundManager.instance.ChangeMute();
    }

    [System.Obsolete]
    public void SendGetGuilds()
    {
        Debug.Log("Get Guilds");
        if (GameManager.instance.test)
        {
            List<M_Guild> guilds = new List<M_Guild>();

            string[] names = { "As", "you", "point", "out", "it", "very", "broad", "question",
                    "but", "try", "touch", "few", "that", "should", "get", "started", "First", "you", "mention",
                "when", "loads", "You", "can", "two", "ways", "one", "simply", "place", "component",
                "that", "scene", "then", "Awake", "Start", "method", "component", "respond", "loading", "scene",
                "Alternatively", "first", "main", "your", "game", "you", "could", "object", "destroyed", "load",
                "second", "scene", "have", "script", "register", "SceneManager", "sceneLoaded", "event", "which" };

            for (int i = 0; i < 35; i++)
            {
                M_Guild guild = new M_Guild();

                guild.id = i;
                guild.name = names[Random.RandomRange(0, names.Length)] + i;
                guild.boss = "Boss: " + i;
                guild.lv = Random.RandomRange(1, 5);
                guild.noti = "Noti: " + i;
                guild.UpdateLevel();
                guild.currentMember = Random.RandomRange((int) guild.maxMember / 2, guild.maxMember + 1);

                guilds.Add(guild);
            }
            RecGuilds(guilds);
            return;
        }

        UserSendUtil.sendGetGuilds();
    }

    public void RecGuilds(List<M_Guild> guilds)
    {
        Timing.RunCoroutine(findGuid._set(guilds));
    }
}
