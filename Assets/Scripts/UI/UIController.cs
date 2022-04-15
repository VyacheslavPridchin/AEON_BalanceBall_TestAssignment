using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject UIMain, UIResult;
    public void ShowMain()
    {
        UIMain.SetActive(true);
        UIResult.SetActive(false);
    }

    public void ShowResults()
    {
        UIMain.SetActive(false);
        UIResult.SetActive(true);
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
