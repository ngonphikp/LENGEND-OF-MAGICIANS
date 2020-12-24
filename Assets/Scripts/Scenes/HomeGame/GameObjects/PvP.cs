using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PvP : MonoBehaviour
{
    public static PvP instance = null;

    [SerializeField]
    private GameObject popUp = null;
    [SerializeField]
    private GameObject loading = null;
    [SerializeField]
    private float speedLoading = -200f;
    [SerializeField]
    private Text txtTime = null;

    [SerializeField]
    private C_ProfileAcc c_self = null;
    [SerializeField]
    private C_ProfileAcc c_rival = null;

    private bool isLoading = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (isLoading && loading.activeInHierarchy)
        {
            loading.transform.Rotate(0, 0, speedLoading * Time.deltaTime);
        }
    }

    public void StartPvP()
    {
        if (isLoading) return;
        RequestPvP.StartPvP();
        c_self.set(GameManager.instance.account);
    }

    public void CanclePvP()
    {
        if (!isLoading) return;
        RequestPvP.CanclePvP();
    }

    public void RecStart()
    {
        popUp.SetActive(true);
        isLoading = true;
        loading.SetActive(true);
    }

    public void RecCancle()
    {
        popUp.SetActive(false);
        isLoading = false;
    }


    public IEnumerator<float> _SuccessPvP(M_Account account) 
    {
        isLoading = false;
        c_rival.set(account);
        c_rival.gameObject.SetActive(true);

        yield return Timing.WaitForSeconds(1);
        loading.SetActive(false);
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(_CDT()));

        List<M_Character> characters = new List<M_Character>();
        foreach (M_Character character in GameManager.instance.characters)
        {
            if (character.isSelec) characters.Add(new M_Character(character));
        }

        RequestGame.Init(characters);
    }

    public void InitMilestone(List<M_Character> lstCharacter)
    {
        GameManager.instance.isAttack = true;
        GameManager.instance.battleType = C_Enum.BattleType.PVP;

        M_Milestone milestone = new M_Milestone();
        milestone.maxTurn = 20;
        milestone.name = "PVP";
        milestone.lstCharacter = lstCharacter;

        GameManager.instance.milestone = milestone;

        GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
        SceneManager.LoadSceneAsync("PlayGame");
    }

    private IEnumerator<float> _CDT()
    {
        int t = 3;
        txtTime.gameObject.SetActive(true);
        while (true)
        {
            if (t <= 0) break;
            txtTime.text = t + "";
            yield return Timing.WaitForSeconds(1);
            t -= 1;
        }
        txtTime.gameObject.SetActive(false);
        yield return Timing.WaitForOneFrame;
    }
}
