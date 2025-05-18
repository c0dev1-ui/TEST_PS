using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallMassSceneSetup : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody ballRb;

    void Start()
    {
        CreateBall();
        CreateUI();
    }

    void CreateBall()
    {
        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.position = new Vector3(0, 1, 0);
        ballRb = ball.AddComponent<Rigidbody>();
    }

    void CreateUI()
    {
        // Создаём Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Добавляем EventSystem (если его ещё нет)
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // Создаём InputField
        GameObject inputGO = new GameObject("MassInput");
        inputGO.transform.SetParent(canvasGO.transform);

        RectTransform inputRect = inputGO.AddComponent<RectTransform>();
        inputRect.sizeDelta = new Vector2(160, 30);
        inputRect.anchoredPosition = new Vector2(0, 50);
        inputRect.localPosition = new Vector3(0, 50, 0);

        Image inputImage = inputGO.AddComponent<Image>();
        inputImage.color = Color.white;

        InputField inputField = inputGO.AddComponent<InputField>();

        // Добавляем Text для InputField
        GameObject placeholderGO = new GameObject("Placeholder");
        placeholderGO.transform.SetParent(inputGO.transform);
        Text placeholder = placeholderGO.AddComponent<Text>();
        placeholder.text = "Введите массу";
        placeholder.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        placeholder.color = Color.gray;
        placeholder.alignment = TextAnchor.MiddleLeft;
        RectTransform placeholderRect = placeholder.GetComponent<RectTransform>();
        placeholderRect.anchorMin = Vector2.zero;
        placeholderRect.anchorMax = Vector2.one;
        placeholderRect.offsetMin = Vector2.zero;
        placeholderRect.offsetMax = Vector2.zero;

        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(inputGO.transform);
        Text text = textGO.AddComponent<Text>();
        text.text = "";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleLeft;
        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        inputField.placeholder = placeholder;
        inputField.textComponent = text;

        // Создаём кнопку
        GameObject buttonGO = new GameObject("SetMassButton");
        buttonGO.transform.SetParent(canvasGO.transform);

        RectTransform buttonRect = buttonGO.AddComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(160, 30);
        buttonRect.anchoredPosition = new Vector2(0, -10);
        buttonRect.localPosition = new Vector3(0, -10, 0);

        Image buttonImage = buttonGO.AddComponent<Image>();
        buttonImage.color = new Color(0.3f, 0.6f, 1f);

        Button button = buttonGO.AddComponent<Button>();

        GameObject buttonTextGO = new GameObject("Text");
        buttonTextGO.transform.SetParent(buttonGO.transform);
        Text buttonText = buttonTextGO.AddComponent<Text>();
        buttonText.text = "Установить массу";
        buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.color = Color.white;
        RectTransform btnTextRect = buttonText.GetComponent<RectTransform>();
        btnTextRect.anchorMin = Vector2.zero;
        btnTextRect.anchorMax = Vector2.one;
        btnTextRect.offsetMin = Vector2.zero;
        btnTextRect.offsetMax = Vector2.zero;

        // Привязываем событие
        button.onClick.AddListener(() =>
        {
            if (float.TryParse(inputField.text, out float mass))
            {
                mass = Mathf.Clamp(mass, 0.01f, 1000f);
                ballRb.mass = mass;
                Debug.Log("Новая масса шара: " + mass);
            }
            else
            {
                Debug.LogWarning("Неверный ввод массы.");
            }
        });
    }
}
