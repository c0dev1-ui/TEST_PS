#if UNITY_STANDALONE_WIN || UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System.IO;
using System.Collections;

public class FileBrowserWithoutPlugin : MonoBehaviour
{
    public RawImage targetImage; // объект, на который будет применен фон

    public void OpenImageFile()
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
        dialog.Title = "Выберите изображение";
        
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string path = dialog.FileName;
            StartCoroutine(LoadImage(path));
        }
    }

    IEnumerator LoadImage(string path)
    {
        string url = "file:///" + path.Replace("\\", "/");
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (www.texture != null)
            {
                targetImage.texture = www.texture;
            }
            else
            {
                Debug.LogError("Не удалось загрузить изображение: " + path);
            }
        }
    }
}
#endif
