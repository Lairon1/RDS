using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitialSceneLoader : MonoBehaviour
{

    // Use this for initialization

    public Texture2D initialLogo;
    public float logoDisplayTime = 8f;
    public float fadeTime = 1.5f;
    public Color backGroundColor = Color.black;
    public Vector2 textureSize = new Vector2(250f, 250f);


    public string nextSceneName = "CarChooseScene";
    private Texture2D backGround;

    void Start()
    {
        backGround = new Texture2D(2, 2);
        backGround.SetPixel(0, 0, backGroundColor);
        backGround.SetPixel(1, 1, backGroundColor);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - (logoDisplayTime + fadeTime * 2) > 0)
            SceneManager.LoadScene(nextSceneName);
    }

    void OnGUI()
    {
        Color originalColor = GUI.color;
        GUI.color = backGroundColor;
        Rect rBK = new Rect(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(rBK, backGround);
        GUI.color = originalColor;
        Color newColor = originalColor;
        if (Time.time < fadeTime) newColor.a = Mathf.Lerp(0f, 1.0f, Time.time / fadeTime);
        else if (Time.time > fadeTime + logoDisplayTime) newColor.a = Mathf.Lerp(0.0f, 1.0f, 1 - ((Time.time - (logoDisplayTime + fadeTime)) / fadeTime));



        GUI.color = newColor;
        Rect r = new Rect(Screen.width / 2 - textureSize.x / 2f, Screen.height / 2 - textureSize.y / 2f, textureSize.x, textureSize.y);
        GUI.DrawTexture(r, initialLogo);
        GUI.color = originalColor;
    }
}
