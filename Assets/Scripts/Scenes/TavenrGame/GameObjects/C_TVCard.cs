using UnityEngine;
using UnityEngine.UI;

public class C_TVCard : MonoBehaviour
{
    public C_Enum.CardType type = C_Enum.CardType.CT;

    [SerializeField]
    private Button btnConfirm = null;

    [SerializeField]
    private Transform content = null;

    [SerializeField]
    private Image bG = null;

    [SerializeField]
    private GameObject title = null;

    private void OnEnable()
    {
        Show(true);
    }

    public void Show(bool isshow)
    {
        bG.gameObject.SetActive(!isshow);
        btnConfirm.gameObject.SetActive(!isshow);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        content.gameObject.SetActive(!isshow);
        title.SetActive(isshow);
        this.GetComponent<Button>().interactable = isshow;
    }

    [System.Obsolete]
    public void OnClick()
    {
        Debug.Log("OnClick");
        btnConfirm.interactable = false;

        TavernGame.instance.ReqCard(this);
    }

    public void Rec(M_Character character)
    {
        bG.sprite = Resources.Load<Sprite>("Sprites/BG/BG" + character.element);
        btnConfirm.interactable = true;

        Debug.Log("Nhan vat moi: " + character.id + " / " + character.id_cfg);

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        GameObject nvAs = Resources.Load<GameObject>("Prefabs/Character/" + character.id_cfg);

        if (nvAs == null)
        {
            nvAs = Resources.Load<GameObject>("Prefabs/Character/T1004");
        }

        if (nvAs != null)
        {
            GameObject obj = Instantiate(nvAs, content);
            C_Character c_character = obj.GetComponent<C_Character>();
            c_character.Set(character);
        }

        GameManager.instance.characters.Add(character);        
    }

}
