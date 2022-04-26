using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignScript : MonoBehaviour {

	public string Text = "Default Sign Text";
	public Rect BoxSize = new Rect( 0, 0, 400, 300);
	public GUISkin format;
    public bool toggleGUI;

	void OnTriggerEnter() {
		toggleGUI = true;
	}

	void OnTriggerExit() {
		toggleGUI = false;
	}

	void OnGUI()
	{
		if (format != null){
			GUI.skin = format;
		}
		if (toggleGUI == true){
			GUI.BeginGroup (new Rect ((Screen.width - BoxSize.width) / 2,
            (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
			GUI.Label(BoxSize, Text);
			GUI.EndGroup ();
		}


	}

}