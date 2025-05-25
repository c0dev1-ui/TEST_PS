using UnityEngine;

public class DualButtonPanelController : MonoBehaviour
{
    public GameObject panel;               // Панель, которую показываем/скрываем
    public GameObject[] buttonsToHide;     // Кнопки, которые скрываем и показываем

    private bool isPanelOpen = false;      // Флаг, чтобы не выполнять действия повторно

    public void ShowPanel()
    {
        if (isPanelOpen)
        {
            Debug.Log("Панель уже открыта — повторное открытие отменено.");
            return;
        }

        if (panel != null)
        {
            panel.SetActive(true);
            isPanelOpen = true;
        }
        else
        {
            Debug.LogError("Panel не назначена!");
        }

        foreach (var btn in buttonsToHide)
        {
            if (btn != null)
            {
                btn.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Одна из кнопок в списке не назначена.");
            }
        }
    }

    public void HidePanel()
    {
        if (!isPanelOpen)
        {
            Debug.Log("Панель уже закрыта — повторное закрытие отменено.");
            return;
        }

        if (panel != null)
        {
            panel.SetActive(false);
            isPanelOpen = false;
        }

        foreach (var btn in buttonsToHide)
        {
            if (btn != null)
            {
                btn.SetActive(true);
            }
        }
    }
}
