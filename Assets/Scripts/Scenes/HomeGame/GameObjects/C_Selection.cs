using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Selection : MonoBehaviour
{
    [SerializeField]
    private GameObject popup = null;
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject Prb = null;

    [SerializeField]
    private List<C_BagEl> Objs = new List<C_BagEl>();
    private Dictionary<int, C_BagEl> DicObj = new Dictionary<int, C_BagEl>();

    [SerializeField]
    private Transform content2 = null;
    [SerializeField]
    private GameObject Prb2 = null;

    [SerializeField]
    private Image imgFight = null;
    [SerializeField]
    private Text txtFight = null;
    [SerializeField]
    private Material gray = null;

    [SerializeField]
    private List<C_CharacterAvEl> Seleces = new List<C_CharacterAvEl>();
    private Dictionary<int, C_CharacterAvEl> DicSelec = new Dictionary<int, C_CharacterAvEl>();

    private int maxCount = 9;
    public int count = 0;

    public void Open()
    {
        Timing.RunCoroutine(_set());
    }

    private IEnumerator<float> _set()
    {
        int i;
        count = 0;
        DicObj.Clear();
        DicSelec.Clear();

        for (i = 0; i < GameManager.instance.characters.Count; i++)
        {
            if (i < Objs.Count)
            {
                Timing.RunCoroutine(Objs[i]._set(i, this));
                Objs[i].gameObject.SetActive(true);
                DicObj.Add(GameManager.instance.characters[i].id, Objs[i]);
            }
            else
            {
                C_BagEl obj = Instantiate(Prb, content).GetComponent<C_BagEl>();
                Timing.RunCoroutine(obj._set(i, this));
                Objs.Add(obj);
                DicObj.Add(GameManager.instance.characters[i].id, obj);
            }            

            if (GameManager.instance.characters[i].isSelec && count < maxCount)
            {
                if (count < Seleces.Count)
                {
                    Seleces[count].set(GameManager.instance.characters[i]);
                    Seleces[count].gameObject.SetActive(true);
                    DicSelec.Add(GameManager.instance.characters[i].id, Seleces[count]);
                }
                else
                {
                    C_CharacterAvEl obj = Instantiate(Prb2, content2).GetComponent<C_CharacterAvEl>();
                    obj.set(i, this);
                    Seleces.Add(obj);
                    DicSelec.Add(GameManager.instance.characters[i].id, obj);
                }
                count++;
            }
        }

        for (int j = i; j < Objs.Count; j++)
        {
            Objs[j].gameObject.SetActive(false);
        }

        for (int j = count; j < Seleces.Count; j++)
        {
            Objs[j].gameObject.SetActive(false);
        }

        popup.SetActive(true);

        txtFight.text = "Chiến " + count + " / " + maxCount;
        imgFight.material = (count == maxCount) ? null : gray;
        yield break;
    }

    public void Select(int idx)
    {
        if (GameManager.instance.characters[idx].isSelec)
        {
            DicObj[GameManager.instance.characters[idx].id].Select(false);
            DicSelec[GameManager.instance.characters[idx].id].gameObject.SetActive(false);

            GameManager.instance.characters[idx].isSelec = false;
            count--;
            txtFight.text = "Chiến " + count + " / " + maxCount;
            imgFight.material = (count == maxCount) ? null : gray;
        }
        else
        {
            if(count < maxCount)
            {
                GameManager.instance.characters[idx].isSelec = true;

                DicObj[GameManager.instance.characters[idx].id].Select(true);
                if (DicSelec.ContainsKey(GameManager.instance.characters[idx].id))
                {
                    DicSelec[GameManager.instance.characters[idx].id].gameObject.SetActive(true);
                }
                else
                {
                    C_CharacterAvEl obj = Instantiate(Prb2, content2).GetComponent<C_CharacterAvEl>();
                    obj.set(idx, this);
                    Seleces.Add(obj);
                    DicSelec.Add(GameManager.instance.characters[idx].id, obj);
                }
                
                count++;
                txtFight.text = "Chiến " + count + " / " + maxCount;
                imgFight.material = (count == maxCount) ? null : gray;
            }
        }
    }

    public void Fight()
    {
        if (count < maxCount) return;

        PvP.instance.StartPvP();
    }
}
