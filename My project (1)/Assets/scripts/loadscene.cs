using UnityEngine;
using UnityEngine.SceneManagement;
public class loadscene : MonoBehaviour
{
	public int SceneNumber;
	public void Transition()
	{
		SceneManager.LoadScene(SceneNumber);
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created

}
