using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance = null;

    [SerializeField]
    private List<GameObject> scenes = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        ShowScene((GameManager.instance.characters.Count > 0) ? scenes[0] : scenes[1]);
    }

    public void ShowScene(GameObject obj)
    {
        scenes.ForEach(x => x.SetActive(false));
        obj.SetActive(true);
    }

    public void ShowScene(string name)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i].name == name)
            {
                ShowScene(scenes[i]);
                break;
            }
        }
    }
}
