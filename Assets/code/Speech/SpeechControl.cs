using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechControl : MonoBehaviour {

    public GameObject SpeechPanel;
    public UnityEngine.UI.Image SpeakerPortrait;
    public UnityEngine.UI.Text SpeakerName;
    public UnityEngine.UI.Text Speech;

    public GameObject PortraitPlane;

    public bool EnableSpeech = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseSpeechPanel()
    {
        //SpeechPanel.gameObject.SetActive(false);
        SpeechPanel.SetActive(false);

    }


}
