using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BagHero : MonoBehaviour
{
    [SerializeField]
    private GameObject popup = null;
    [SerializeField]
    private Transform content = null;
    [SerializeField]
    private GameObject Prb = null;

    [SerializeField]
    private List<C_BagEl> Objs = new List<C_BagEl>();

    public void Open()
    {
        Timing.RunCoroutine(_set());
    }

    private IEnumerator<float> _set()
    {
        int i;
        for (i = 0; i < GameManager.instance.characters.Count; i++)
        {
            if (i < Objs.Count)
            {
                Timing.RunCoroutine(Objs[i]._set(i));
                Objs[i].gameObject.SetActive(true);
            }
            else
            {
                C_BagEl obj = Instantiate(Prb, content).GetComponent<C_BagEl>();
                Timing.RunCoroutine(obj._set(i));
                Objs.Add(obj);
            }
        }

        for (int j = i; j < Objs.Count; j++)
        {
            Objs[j].gameObject.SetActive(false);
        }

        popup.SetActive(true);
        yield break;
    }
}
