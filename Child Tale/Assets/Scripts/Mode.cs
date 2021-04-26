using UnityEngine;
using UnityEngine.SceneManagement;

public class Mode : MonoBehaviour
{
    public float mode;

    public void SetMode()
    {
        PlayerPrefs.SetFloat("mode", mode);
        SceneManager.LoadScene(1);
    }
}
