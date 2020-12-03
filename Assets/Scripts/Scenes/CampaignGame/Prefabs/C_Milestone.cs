using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_Milestone : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteStars = null;

    private Image img;
    private M_Milestone milestone = new M_Milestone();

    private void Awake()
    {
        img = this.GetComponent<Image>();
    }

    public void set(M_Milestone milestone)
    {
        this.milestone = milestone;
    }

    public void Click()
    {
        Debug.Log("Click :" + this.milestone.id);

        GameManager.instance.isAttack = true;
        GameManager.instance.battleType = C_Enum.BattleType.Campaign;
        GameManager.instance.milestone = this.milestone;

        GameManager.instance.mainName = C_Enum.MainGame.CampaignScene;
        SceneManager.LoadSceneAsync("PlayGame");
    }

    public void UpdateStar(int star)
    {
        img.sprite = spriteStars[star];
        this.GetComponent<Button>().interactable = true;
    }
}
