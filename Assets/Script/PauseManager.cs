using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button titleButton;
    [SerializeField]SimpleUITransition[] transitions;
    [SerializeField] private GameObject[] nowPanels;
    [SerializeField] GameObject player;
   
    bool pause;
    float time;
    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        returnButton.onClick.AddListener(Resume);
        exitButton.onClick.AddListener(GameEnd);
        titleButton.onClick.AddListener(TitleReturn);
        pause = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (Input.GetKey(KeyCode.M) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {

                Pause();       // Time.timeScale = 0;
                               //player.SetActive(false);  
            }
        }
        if (pause)
        {
            time++;
            if (time >= 30)
            {
                Time.timeScale = 0;

            }
        }
    }
    public void Pause()
    {       
        pause = true;
        foreach (var panel in nowPanels)
        {
            panel.SetActive(false);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);

        foreach (var trans in transitions)
        {
            trans.Show();
        }
        
    }
    private void Resume()
    {
        pause = false;
        time = 0;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);

        foreach (var trans in transitions)
        {
            trans.SetValue(0);
        }
        foreach (var panel in nowPanels)
        {
            panel.SetActive(true);
        }
    }
    private void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();
#endif
    }
    private void TitleReturn()
    {
        FadeManager.Instance.LoadScene(Constants.titleSceneName.ToString(), 1f);

        pause = false;
        time = 0;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //pausePanel.SetActive(false);
    }
}
