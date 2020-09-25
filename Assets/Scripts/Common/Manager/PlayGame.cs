using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    public static PlayGame instance = null;

    [SerializeField]
    private GameObject fightingScene = null;
    [SerializeField]
    private GameObject arrangeScene = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ShowScene(bool isShow)
    {
        fightingScene.SetActive(isShow);
        arrangeScene.SetActive(!isShow);
    }
}
