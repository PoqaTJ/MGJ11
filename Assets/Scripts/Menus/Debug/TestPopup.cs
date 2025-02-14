using Menus;
using Menus.MenuTypes;
using Services;
using UnityEngine;

public class TestPopup : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClick()
    {
        var context = new PopupMenuOneButton.PopupMenuOneButtonContext();
        context.titleLocString = "Look!";
        context.bodyLocString = "A wild menu appeared! Hopefully anims and gameplay are frozen and clicks are blocked.";
        context.buttonLocString = "Close";
        ServiceLocator.Instance.MenuManager.Show(MenuType.PopupOneButton, context);
    }
}
