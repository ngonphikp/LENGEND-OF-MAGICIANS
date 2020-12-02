using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame : MonoBehaviour
{    
    public static LoginGame instance = null;

    [SerializeField]
    private AudioClip audioClip = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        SoundManager.instance.PlayLoop(audioClip);
    }

    public void RecLogin(M_Account ac)
    {
        GameManager.instance.account = ac;

        RequestAccount.GetInfo(ac.id);
    }

    public void RecRegister(M_Account ac)
    {
        GameManager.instance.account = ac;

        RequestAccount.GetInfo(ac.id);
    }

    public void RecInfo(int id_guild, List<M_Character> lstCharacter, List<M_Tick_Milestone> lstTick_milestones)
    {
        GameManager.instance.account.id_guild = id_guild;

        GameManager.instance.characters = lstCharacter;
        ScenesManager.instance.ChangeScene("MainGame");

        SoundManager.instance.PlayLoop();
    }

    public void QuitGame()
    {
        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
        Application.Quit();
    }
}
