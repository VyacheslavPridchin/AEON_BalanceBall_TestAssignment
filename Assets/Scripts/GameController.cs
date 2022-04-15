using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [HideInInspector]
    public bool GameStarted = false;
    [HideInInspector]
    public TimeSpan CurrentResult = new TimeSpan(0, 0, 0);

    [SerializeField]
    private GameObject UILostPanel;
    [SerializeField]
    private GameObject UIWinPanel;
    [SerializeField]
    private Text UICurrentResult;
    [SerializeField]
    private Text UICountown;

    [Header("Settings")]
    [SerializeField]
    private bool hideCursor = false;
    [Header("Events")]
    public UnityEvent PlayerLost = new UnityEvent();
    public UnityEvent PlayerWin = new UnityEvent();
    public UnityEvent GameStart = new UnityEvent();
    public UnityEvent GameStop = new UnityEvent();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Start()
    {
        StartCoroutine(Countdown());
        GameStart.AddListener(() => StartCoroutine(Timer()));

        PlayerLost.AddListener(() =>
        {
            GameStop?.Invoke();
            UILostPanel.SetActive(true);
        });

        PlayerWin.AddListener(() =>
        {
            GameStop?.Invoke();
            UIWinPanel.SetActive(true);
            SaveManager.AddResult(CurrentResult);
        });

        GameStop.AddListener(() =>
        {
            GameStarted = false;
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LoadScene(0);
        }
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSecondsRealtime(1f);

        for (int i = 3; i > 0; i--)
        {
            UICountown.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        UICountown.gameObject.SetActive(false);
        GameStarted = true;
        GameStart?.Invoke();
    }

    private IEnumerator Timer()
    {
        DateTime startTime = DateTime.Now;
        while (GameStarted)
        {
            yield return new WaitForSeconds(0.01f);
            CurrentResult = DateTime.Now - startTime;
            UICurrentResult.text = $"{CurrentResult.Minutes} мин. ";
            UICurrentResult.text += $"{CurrentResult.Seconds} сек. ";
            UICurrentResult.text += $"{CurrentResult.Milliseconds} мс. ";
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
