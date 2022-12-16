using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuSceneName;
    public Font pauseMenuFont;
    public int fontSize = 16;
    private bool pauseEnabled = false;

    void Start()
    {
        pauseEnabled = false;
        Time.timeScale = 1;
        AudioListener.volume = 1;
        //	Screen.showCursor = false;
    }

    void Update()
    {

        //check if pause button (escape key) is pressed
        if (Input.GetKeyDown("escape"))
        {

            //check if game is already paused		
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                Time.timeScale = 1;
                AudioListener.volume = 1;
                //			Screen.showCursor = false;			
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                AudioListener.volume = 0;
                Time.timeScale = 0;
                //			Screen.showCursor = true;
            }
        }
    }

    private bool showGraphicsDropDown = false;

    void OnGUI()
    {

        GUI.skin.box.font = pauseMenuFont;
        GUI.skin.button.font = pauseMenuFont;
        GUI.skin.button.fontSize = fontSize;

        if (pauseEnabled == true)
        {

            //Make a background box
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 200), "Pause Menu");

            //Make Main Menu button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Main Menu"))
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }

            //Make Change Graphics Quality button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 250, 50), "Change Graphics Quality"))
            {

                if (showGraphicsDropDown == false)
                {
                    showGraphicsDropDown = true;
                }
                else
                {
                    showGraphicsDropDown = false;
                }
            }

            //Create the Graphics settings buttons, these won't show automatically, they will be called when
            //the user clicks on the "Change Graphics Quality" Button, and then dissapear when they click
            //on it again....
            if (showGraphicsDropDown == true)
            {
                for (int i = 0; i < QualitySettings.names.Length; ++i)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + i * 50, 250, 50), QualitySettings.names[i]))
                    {
                        QualitySettings.SetQualityLevel(i);
                    }
                }

                if (Input.GetKeyDown("escape"))
                {
                    showGraphicsDropDown = false;
                }
            }

            //Make quit game button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 250, 50), "Quit Game"))
            {
                Application.Quit();
            }
        }
    }
}
