using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField playername;
 
   
   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        SetName();
        SceneManager.LoadScene(1);
        
    }
    public void exit()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();

    }

    public void SetName()
    {
        string name = playername.text;
       PlayerPrefs.SetString("playername",name);
    }

    public void getname()
    {
        if (PlayerPrefs.HasKey("playername"))
        {
            playername.text = PlayerPrefs.GetString("playername");
        }
       
    }

   
}
