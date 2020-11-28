using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LstCharacter : MonoBehaviour
{
    [SerializeField]
    private GameObject prb = null;
    [SerializeField]
    private Transform content = null;

    private List<C_Avatar> lstObjs = new List<C_Avatar>();

    public void set(List<M_Character> data)
    {
        List<C_Avatar> news = new List<C_Avatar>();
        int i;
        for (i = 0; i < data.Count; i++)
        {
            if (i < lstObjs.Count)
            {
                lstObjs[i].set(data[i]);
                news.Add(lstObjs[i]);
            }
            else
            {
                C_Avatar obj = Instantiate(prb, content).GetComponent<C_Avatar>();
                obj.set(data[i]);
                news.Add(obj);
            }
        }

        for (int j = i; j < lstObjs.Count; j++)
        {
            Destroy(lstObjs[j].gameObject);
        }

        lstObjs = news;
    }
}
