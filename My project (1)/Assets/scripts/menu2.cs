using UnityEngine;

public class PanelToggle : MonoBehaviour
{
	// Панель, которую нужно показать
	public GameObject hiddenPanel;


	// Кнопка, которую нужно скрыть (сам объект кнопки, а не только её визуальная часть)
	public GameObject buttonToHide;



	// Метод вызывается при нажатии на кнопку
	public void ShowPanelAndHideButton()
	{
		if (hiddenPanel != null)
		{
			hiddenPanel.SetActive(true); // Показываем панель
		}

		if (buttonToHide != null)
		{
			buttonToHide.SetActive(false); // Скрываем кнопку
		}
	}
	
	public void HidePanelAndShowButton()
{
	if (hiddenPanel != null)
	{
		hiddenPanel.SetActive(false);
	}

	if (buttonToHide != null)
	{
		buttonToHide.SetActive(true);
	}
}

	
	
}
