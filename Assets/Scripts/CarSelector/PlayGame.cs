using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayGame : MonoBehaviour
{
  public string Game;

  //loading scene name
  public void LoadLevel()
  {
      SceneManager.LoadScene(Game);
  }
}
