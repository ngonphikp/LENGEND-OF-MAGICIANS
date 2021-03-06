﻿using System.Collections.Generic;
using MEC;
using UnityEngine;

[System.Serializable]
public class C_Character : MonoBehaviour
{
    [Header("Điều khiển hành động")]
    [SerializeField]
    private Animator anim = null;

    public bool isAnim2 = false;
    public bool isAnim3 = false;
    public bool isAnim4 = false;
    public bool isAnim5 = false;
    public bool isAnim6 = false;
    public bool isAnim7 = false;

    [Header("Thành phần giao diện")]
    [SerializeField]
    private C_UICharacter UICharacter = null;

    [Header("Khả năng chiến đấu")]
    public bool isCombat = false;
    public bool isHit = true;

    [SerializeField]
    private I_Control ctl = null;
    public M_Character character = new M_Character();

    private List<M_Prop> propHPs = new List<M_Prop>(); // Mảng diễn thay đổi hp
    private List<M_Prop> propEPs = new List<M_Prop>(); // Mảng diễn thay đổi ep

    private CoroutineHandle preUpdate;

    private void Awake()
    {
        ctl = this.GetComponent<I_Control>();
    }

    private void Start()
    {
        if (UICharacter != null)
        {
            UICharacter.hp = character.current_hp * 1.0f / character.max_hp;
            UICharacter.ep = character.current_ep * 1.0f / character.max_ep;
        }

        preUpdate = Timing.RunCoroutine(_preUpdate());
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines(preUpdate);
    }

    private void OnEnable()
    {
        Timing.ResumeCoroutines(preUpdate);
    }

    private void OnDisable()
    {
        Timing.PauseCoroutines(preUpdate);
    }

    private IEnumerator<float> _preUpdate()
    {
        while (true)
        {
            if (isAnim2)
            {
                anim.SetTrigger("anim2");
                isAnim2 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(2);
            }
            if (isAnim3)
            {
                anim.SetTrigger("anim3");
                isAnim3 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(3);
            }
            if (isAnim4)
            {
                anim.SetTrigger("anim4");
                isAnim4 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(4);
            }
            if (isAnim5)
            {
                anim.SetTrigger("anim5");
                isAnim5 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(5);
            }
            if (isAnim6)
            {
                anim.SetTrigger("anim6");
                isAnim6 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(6);

                Timing.RunCoroutine(_AnimDie());
            }
            if (isAnim7)
            {
                anim.SetTrigger("anim7");
                isAnim7 = false;

                if (isCombat && FightingGame.instance.targets.Count > 0) ctl.Play(7);
            }

            if (anim != null)
            {
                anim.speed = ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    public void Set(M_Character character, bool isUI = true)
    {
        this.character = character;

        if(isUI)
        {
            if (UICharacter != null) UICharacter.set(this);
        }
        else
        {
            if (UICharacter != null) UICharacter.gameObject.SetActive(false);
        }
    }

    public void Play(int i)
    {
        Debug.Log("=========================== " + this.character.id + " Play:" + i);

        if (isAnim1())
        {
            switch (i)
            {
                case 2:
                    isAnim2 = true;
                    break;
                case 3:
                    isAnim3 = true;
                    break;
                case 4:
                    isAnim4 = true;
                    break;
                case 5:
                    isAnim5 = true;
                    break;
                case 6:
                    isAnim6 = true;
                    break;
                case 7:
                    isAnim7 = true;
                    break;
            }
        }
        else
        {
            Debug.LogWarning("=========================== " + this.character.id + " Not Anim 1");
            Timing.RunCoroutine(_FAnim(i));
        }
    }

    private IEnumerator<float> _FAnim(int i)
    {
        Debug.Log("=========================== F anim: " + i);
        switch (i)
        {
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:
                while (true)
                {
                    Debug.Log("=========================== Loop F Anim");
                    if (this == null || this.gameObject == null || isAnim1() || character.isDie) break;
                    yield return Timing.WaitForSeconds(0.01f);
                }
                isAnim6 = true;
                break;
            case 7:
                while (true)
                {
                    Debug.Log("=========================== Loop F Anim");
                    if (this == null || this.gameObject == null || isAnim1() || character.isDie) break;
                    yield return Timing.WaitForSeconds(0.01f);
                }
                isAnim7 = true;
                break;
        }
    }

    public void Beaten()
    {
        if (character.isDie)
        {
            Play(6);
        }
        if (!character.isDie && isHit) Play(7);
        if (!isHit && UICharacter != null)
        {
            UICharacter.CreateText(C_Enum.TypeText.DG);
        }

        FightingGame.instance.Beaten--;

        ChangeEp();
        ChangeHp();
    }

    public void ChangeHp()
    {
        //Debug.Log("ChangeHp: " + this.character.id);
        while (propHPs.Count > 0)
        {
            M_Prop prop = propHPs[0];
            character.current_hp += prop.hpChange;
            if (UICharacter != null)
            {
                UICharacter.hp = character.current_hp * 1.0f / character.max_hp;

                if (prop.hpChange >= 0)
                {
                    UICharacter.CreateText(C_Enum.TypeText.HP1, "+ " + prop.hpChange.ToString());
                }
                else
                {
                    if (prop.hpChangeCrit)
                    {
                        UICharacter.CreateText(C_Enum.TypeText.HP2c, "- " + Mathf.Abs(prop.hpChange).ToString());
                    }
                    else
                    {
                        UICharacter.CreateText(C_Enum.TypeText.HP2, "- " + Mathf.Abs(prop.hpChange).ToString());
                    }
                }
            }
            propHPs.RemoveAt(0);
        }
    }

    public void ChangeEp()
    {
        //Debug.Log("ChangeEp: " + this.character.id);
        while (propEPs.Count > 0)
        {
            M_Prop prop = propEPs[0];
            character.current_ep += prop.epChange;
            if (UICharacter != null)
            {
                UICharacter.ep = character.current_ep * 1.0f / character.max_ep;

                if (prop.epChange >= 0)
                {
                    UICharacter.CreateText(C_Enum.TypeText.EP1, "+ " + prop.epChange.ToString());
                }
                else
                {
                    UICharacter.CreateText(C_Enum.TypeText.EP2, "- " + Mathf.Abs(prop.epChange).ToString());
                }
            }
            propEPs.RemoveAt(0);
        }
    }

    public void PushChangeHp(M_Prop prop)
    {
        propHPs.Add(prop);
    }

    public void PushChangeEp(M_Prop prop)
    {
        propEPs.Add(prop);
    }

    public bool IsPlay()
    {
        return (this.isAnim1() || this.character.isDie || !this.gameObject.activeSelf);
    }

    public bool isAnim1()
    {
        if (this == null) return false;
        if (!ctl.IsPlay()) return false;

        if (this.GetComponent<RectTransform>().localPosition != new Vector3() && FightingGame.instance) return false;

        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo.Length < 1) return false;
        string m_ClipName = m_CurrentClipInfo[0].clip.name;

        return (m_ClipName == "anim1");
    }

    private IEnumerator<float> _AnimDie()
    {
        // Làm mờ
        SpriteRenderer sp = anim.gameObject.GetComponent<SpriteRenderer>();

        float maxTime = 0.6f;
        float timeOp = 0.6f;
        float op = 1.0f;
        while (true)
        {
            if (op <= 0.0f || timeOp <= 0.0f) break;

            float delta = Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);

            timeOp -= delta;
            op = timeOp / maxTime;

            yield return Timing.WaitForSeconds(delta);

            sp.color = new Color(1.0f, 1.0f, 1.0f, op);
        }

        this.gameObject.SetActive(false);
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}