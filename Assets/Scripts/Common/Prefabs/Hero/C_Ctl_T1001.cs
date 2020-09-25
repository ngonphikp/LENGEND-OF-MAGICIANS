using MEC;
using System.Collections.Generic;
using UnityEngine;

public class C_Ctl_T1001 : MonoBehaviour, I_Control
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
    private float time5d0 = 0.7f;
    [SerializeField]
    private float time5db = 0f;
    [SerializeField]
    private GameObject bl5d0 = null;
    [SerializeField]
    private Transform posB5 = null;
    [SerializeField]
    private Vector3 offset5 = new Vector3();

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
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 2");
    }

    private IEnumerator<float> _Anim3()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 3");
        isPlay = false;

        C_LibSkill.Shoot(bl3d0, posB3, FightingGame.instance.targets, true, time3d0, time3db, offset3);

        yield return Timing.WaitForSeconds(timeAn3 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private IEnumerator<float> _Anim4()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 4");
        isPlay = false;

        yield return Timing.WaitForSeconds(timeAn4 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private IEnumerator<float> _Anim5()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 5");
        isPlay = false;

        C_LibSkill.Shoot(bl5d0, posB5, FightingGame.instance.targets, true, time5d0, time5db, offset5);

        yield return Timing.WaitForSeconds(timeAn5 / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
        isPlay = true;
    }

    private void Anim6()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 6");
    }

    private void Anim7()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 7");
    }
}
