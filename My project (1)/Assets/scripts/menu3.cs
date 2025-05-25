using UnityEngine;

public class SecondPanelController : MonoBehaviour
{
    public GameObject panel;         // Панель, которую нужно открыть
    public GameObject openButton;    // Кнопка, которая должна скрыться

    // Метод для открытия панели и скрытия кнопки
    public void ShowPanel()
    {
        if (panel != null && openButton != null)
        {
            panel.SetActive(true);
            openButton.SetActive(false);
        }
        else
        {
            Debug.LogError("SecondPanelController: Не назначены ссылки!");
        }
    }

    // Метод для закрытия панели и показа кнопки обратно
    public void HidePanel()
    {
        if (panel != null && openButton != null)
        {
            panel.SetActive(false);
            openButton.SetActive(true);
        }
        else
        {
            Debug.LogError("SecondPanelController: Не назначены ссылки!");
        }
    }
}
