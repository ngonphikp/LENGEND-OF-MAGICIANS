using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C_DD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject Trigger = null;
    public int pos = -1;

    public M_Character character = new M_Character();

    private Canvas canvas;
    private C_DD other;
    private bool isActive = false;
    private bool isOnBeginDrag = false;
    private bool isDD = true;

    public void Init(M_Character character, Canvas canvas, bool isDD = true)
    {
        this.isActive = true;
        this.character = character;
        this.canvas = canvas;
        this.isDD = isDD;

        createNhanVat();
    }

    private void setTrigger(bool isActive)
    {
        Trigger.SetActive(isActive);
    }

    private void createNhanVat()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        if (this.isActive)
        {
            GameObject nvAs = Resources.Load("Prefabs/Character/" + character.id_cfg, typeof(GameObject)) as GameObject;

            if (nvAs == null)
            {
                switch (character.type)
                {
                    case C_Enum.CharacterType.Hero:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Creep:
                        nvAs = Resources.Load("Prefabs/Character/M1000", typeof(GameObject)) as GameObject;
                        break;
                    case C_Enum.CharacterType.Boss:
                        nvAs = Resources.Load("Prefabs/Character/B1000", typeof(GameObject)) as GameObject;
                        break;
                    default:
                        nvAs = Resources.Load("Prefabs/Character/T1004", typeof(GameObject)) as GameObject;
                        break;
                }
                
            }

            if (nvAs != null)
            {
                GameObject obj = Instantiate(nvAs, this.gameObject.transform);
                C_Character hero = obj.GetComponent<C_Character>();
                hero.Set(character);
            }
        }
    }

    public void ReLoad(M_Character character, bool isActive, Canvas canvas = null)
    {
        this.isActive = isActive;
        this.character = character;        

        if (this.canvas == null) this.canvas = canvas;

        createNhanVat();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnBeginDrag: " + character.id);

        this.isOnBeginDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        //Debug.Log("OnDrag: " + parent.character.id);
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isActive || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnEndDrag: " + character.id);

        if (other != null)
        {
            Debug.Log("Other: " + other.character.id);

            Debug.Log(this.character.id + " From: " + pos + " => To: " + other.pos);
            ArrangeGame.instance.characterDic[this.character.id].idx = other.pos;
            
            if(other.character.id != - 1)
            {
                Debug.Log(other.character.id + " From: " + other.pos + " => To: " + this.pos);
                ArrangeGame.instance.characterDic[other.character.id].idx = this.pos;
            }

            M_Character character = this.character;
            bool isActive = this.isActive;

            this.ReLoad(other.character, other.isActive, this.canvas);
            other.ReLoad(character, isActive, this.canvas);

            this.setTrigger(false);
            other.setTrigger(false);            
        }

        this.transform.localPosition = new Vector3();
        other = null;

        this.isOnBeginDrag = false;
    }

    public void OnClick()
    {
        if (!isActive || isOnBeginDrag || !ArrangeGame.instance || !isDD) return;
        Debug.Log("OnClick: " + character.id);

        ArrangeGame.instance.UnActive(character.id);

        this.ReLoad(new M_Character(), false);        
    }

    private C_DD oldOther;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DD") && other.gameObject.GetComponent<C_DD>() != null && this.isOnBeginDrag && ArrangeGame.instance && isDD)
        {
            this.other = other.gameObject.GetComponent<C_DD>();

            if(oldOther != null) oldOther.setTrigger(false);
            this.other.setTrigger(true);

            oldOther = this.other;
        }
    }
}
