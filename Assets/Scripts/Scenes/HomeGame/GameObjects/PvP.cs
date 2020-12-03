using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (isLoading)
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
    }

    public void RecCancle()
    {
        popUp.SetActive(false);
        isLoading = false;
    }

    private bool isCall = false;

    public IEnumerator<float> _SuccessPvP(M_Account account, List<M_Character> characters) 
    {
        if (!isCall)
        {
            isCall = true;
            isLoading = false;
            c_rival.set(account);
            c_rival.gameObject.SetActive(true);

            yield return Timing.WaitForSeconds(2);

            GameManager.instance.isAttack = true;
            GameManager.instance.battleType = C_Enum.BattleType.PVP;

            M_Milestone milestone = new M_Milestone();
            milestone.maxTurn = 20;
            milestone.name = "PVP";
            milestone.lstCharacter = characters;

            GameManager.instance.milestone = milestone;

            GameManager.instance.mainName = C_Enum.MainGame.HomeScene;
            SceneManager.LoadSceneAsync("PlayGame");
        }

        yield return Timing.WaitForOneFrame;
    }
}
