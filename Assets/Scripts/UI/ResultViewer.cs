using System;
using UnityEngine;
using UnityEngine.UI;

public class ResultViewer : MonoBehaviour
{
    [SerializeField]
    private bool showCurrentResult = false;
    [SerializeField]
    private Text UICurrentResult;
    [SerializeField]
    private Text UIOldResult;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (showCurrentResult)
        {
            TimeSpan currentResult = GameController.Instance.CurrentResult;

            UICurrentResult.text = "Результат: ";
            UICurrentResult.text += $"{currentResult.Minutes} мин. ";
            UICurrentResult.text += $"{currentResult.Seconds} сек. ";
            UICurrentResult.text += $"{currentResult.Milliseconds} мс. ";
        }

        UIOldResult.text = SaveManager.GetResults();

    }
}
