using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MassInputManager : MonoBehaviour
{
	public TMP_InputField massInputField;
	public TMP_InputField angleInputField;
	
	public GameObject errorPanel;
	public TextMeshProUGUI errorText;

	
	void Start()
	{
		float savedMass = PlayerPrefs.GetFloat("BallMass", 1f);
		massInputField.text = savedMass.ToString("0.##");
		

		float savedAngle = PlayerPrefs.GetFloat("CubeAngle", 30f);
		angleInputField.text = savedAngle.ToString("0.##");
	}

	public void OnStartButtonClicked()
	{
		string massInput = massInputField.text;
		string angleInput = angleInputField.text;
	

		float mass, angle;

		if (!float.TryParse(massInput, out mass))
		{
			ShowError("Масса — это число.");
			return;
		}

		if (mass <= 0)
		{
			ShowError("Масса должна быть положительной.");
			return;
		}

		if (!float.TryParse(angleInput, out angle))
		{
			ShowError("Угол — это число.");
			return;
		}

		if (angle < 0 || angle > 90)
		{
			ShowError("Угол должен быть от 0 до 90.");
			return;
		}

		PlayerPrefs.SetFloat("BallMass", mass);
		PlayerPrefs.SetFloat("CubeAngle", angle);
		SceneManager.LoadScene("SimulationScene");
	}

	void ShowError(string msg)
	{
		errorText.text = msg;
		errorPanel.SetActive(true);
	}

	public void OnCloseErrorPanel()
	{
		errorPanel.SetActive(false);
	}
}
