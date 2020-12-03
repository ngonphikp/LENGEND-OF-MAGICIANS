using System.Collections.Generic;
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
        ShowScene((int) GameManager.instance.mainName);
    }

    private void ShowScene(int idx)
    {
        for (int i = 0; i < scenes.Count; i++) scenes[i].SetActive((i == idx));
    }

    public void ShowScene(C_Enum.MainGame sceneName)
    {
        ShowScene((int) sceneName);
    }
}
