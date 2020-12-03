using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void RecInfo(int id_guild, List<M_Character> lstCharacter)
    {
        GameManager.instance.account.id_guild = id_guild;

        GameManager.instance.characters = lstCharacter;
        GameManager.instance.mainName = (lstCharacter.Count == 0) ? C_Enum.MainGame.SelectionScene : C_Enum.MainGame.HomeScene;
        SceneManager.LoadSceneAsync("MainGame");

        SoundManager.instance.PlayLoop();
    }

    public void QuitGame()
    {
        SmartFoxConnection.Sfs.Disconnect();
        SmartFoxConnection.setNull();
        Application.Quit();
    }
}
