using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
