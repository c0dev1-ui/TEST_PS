using UnityEngine;
using UnityEngine.UI;

public class TextSwitcher : MonoBehaviour
{
    public GameObject firstText2;     // Первый текст
    public GameObject secondText;     // Второй текст
    public GameObject secondText2;    // Третий текст

    public Button openSecondTextButton;   // Кнопка показать второй текст
    public Button openSecondTextButton2;  // Кнопка показать третий текст

    public Button backButton;         // Кнопка назад ко второму
    public Button backButton2;        // Кнопка назад к первому из третьего

    private void Start()
    {
        // Показываем только первый текст
        firstText2.SetActive(true);
        secondText.SetActive(false);
        secondText2.SetActive(false);

        openSecondTextButton.onClick.AddListener(ShowSecondText);
        openSecondTextButton2.onClick.AddListener(ShowThirdText);
        backButton.onClick.AddListener(BackToFirstFromSecond);
        backButton2.onClick.AddListener(BackToFirstFromThird);
    }

    private void ShowSecondText()
    {
        firstText2.SetActive(false);
        secondText.SetActive(true);
        secondText2.SetActive(false);
    }

    private void ShowThirdText()
    {
        firstText2.SetActive(false);
        secondText.SetActive(false);
        secondText2.SetActive(true);
    }

    private void BackToFirstFromSecond()
    {
        secondText.SetActive(false);
        firstText2.SetActive(true);
    }

    private void BackToFirstFromThird()
    {
        secondText2.SetActive(false);
        firstText2.SetActive(true);
    }
}
