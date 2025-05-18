using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		
	}// Start is called once before the first execution of Update after the MonoBehaviour is created
	public void ExitGame()
	{
		Debug.Log("Игра оконченна");
		Application.Quit();	
	}
}
