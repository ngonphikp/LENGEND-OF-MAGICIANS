using MEC;
using System.Collections.Generic;
using UnityEngine;

public class C_Ctl_T1002 : MonoBehaviour, I_Control
{
    //[Header("Anim2")]

    [Header("Anim3")]
    [SerializeField]
    private float timeAn3 = 0.0f;
    [SerializeField]
    private float time3d0 = 0.7f;
    [SerializeField]
    private float time3db = 0f;
    [SerializeField]
    private GameObject bl3d0 = null;
    [SerializeField]
    private Transform posB3 = null;
    [SerializeField]
    private Vector3 offset3 = new Vector3();

    [Header("Anim4")]
    [SerializeField]
    private float timeAn4 = 0.0f;

    [Header("Anim5")]
    [SerializeField]
    private float timeAn5 = 0.0f;
    [SerializeField]
    private float time5d0 = 0.6f;
    [SerializeField]
    private GameObject fx5d0 = null;
    [SerializeField]
    private float time5ds = 0.3f;
    [SerializeField]
    private float time5df = 0.8f;
    [SerializeField]
    private float time5dm = 0.3f;
    [SerializeField]
    private float dis5d0 = 17f;

    private bool isPlay = true;
    public bool IsPlay() { return isPlay; }

    public void Play(int anim)
    {
        switch (anim)
        {
            case 2:
                Anim2();
                break;
            case 3:
                Timing.RunCoroutine(_Anim3());
                break;
            case 4:
                Timing.RunCoroutine(_Anim4());
                break;
            case 5:
                Timing.RunCoroutine(_Anim5());
                break;
            case 6:
                Anim6();
                break;
            case 7:
                Anim7();
                break;
        }
    }

    private void Anim2()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 2");
    }

    private IEnumerator<float> _Anim3()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 3");
        isPlay = false;

        C_LibSkill.Shoot(bl3d0, posB3, FightingGame.instance.targets, true, time3d0, time3db, offset3);

        yield return Timing.WaitForSeconds(timeAn3 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private IEnumerator<float> _Anim4()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 4");
        isPlay = false;

        yield return Timing.WaitForSeconds(timeAn4 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private IEnumerator<float> _Anim5()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 5");
        isPlay = false;

        Vector3 G = C_LibSkill.GHero(FightingGame.instance.targets);
        Vector3 C = C_LibSkill.DisABC(G, this.transform.position, dis5d0 + Vector3.Distance(this.transform.position, G));
        Timing.RunCoroutine(C_LibSkill._MoveTo(this.transform, C, time5ds, time5df, time5dm, false));

        Timing.RunCoroutine(C_LibSkill._FxHit(FightingGame.instance.targets, fx5d0, time5d0));

        yield return Timing.WaitForSeconds(timeAn5 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private void Anim6()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 6");
    }

    private void Anim7()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().character.id + " Anim 7");
    }
}
