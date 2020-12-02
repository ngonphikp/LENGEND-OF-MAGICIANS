using UnityEngine;
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

        //if (GameManager.instance.tick_milestonesDic.ContainsKey(this.milestone.id))
        //{
        //    UpdateStar(GameManager.instance.tick_milestonesDic[milestone.id].star);

        //    this.GetComponent<Button>().interactable = true;
        //}        
    }

    public void Click()
    {
        Debug.Log("Click :" + this.milestone.id);

        GameManager.instance.isAttack = true;
        GameManager.instance.battleType = C_Enum.BattleType.Campain;
        GameManager.instance.milestone = this.milestone;

        ScenesManager.instance.ChangeScene("PlayGame");
    }

    private void UpdateStar(int star)
    {
        if(star <= spriteStars.Length)
        {
            img.sprite = spriteStars[star];
        }
        else
        {
            Debug.LogWarning("Exits Sprite");
        }
    }
}
