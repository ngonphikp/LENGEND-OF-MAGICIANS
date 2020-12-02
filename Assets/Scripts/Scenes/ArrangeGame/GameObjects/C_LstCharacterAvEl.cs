using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LstCharacterAvEl : MonoBehaviour
{
    [SerializeField]
    private GameObject prb = null;
    [SerializeField]
    private Transform content = null;

    private List<C_CharacterAvEl> lstObjs = new List<C_CharacterAvEl>();
    private Dictionary<int, C_CharacterAvEl> dicObjs = new Dictionary<int, C_CharacterAvEl>();

    public void set(List<M_Character> data)
    {
        dicObjs.Clear();
        List<C_CharacterAvEl> news = new List<C_CharacterAvEl>();
        int i;
        for (i = 0; i < data.Count; i++)
        {
            if (i < lstObjs.Count)
            {
                lstObjs[i].set(data[i]);
                news.Add(lstObjs[i]);
                dicObjs.Add(data[i].id, lstObjs[i]);
            }
            else
            {
                C_CharacterAvEl obj = Instantiate(prb, content).GetComponent<C_CharacterAvEl>();
                obj.set(data[i]);
                news.Add(obj);
                dicObjs.Add(data[i].id, obj);
            }
        }

        for (int j = i; j < lstObjs.Count; j++)
        {
            Destroy(lstObjs[j].gameObject);
        }

        lstObjs = news;
    }

    public void UnActive(int id)
    {
        dicObjs[id].UnActive();
    }
}
