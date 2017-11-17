using UnityEngine;
using System.Collections;
using UnityEditor;


public class Level : MonoBehaviour {
	public GameObject GameMenu;//drag and drop here
	public bool paused = false;

	public GameObject DontDel;
	public SaveLoadFunctions saveloadFunctions;
	public SaveLoadManager saveloadManager;

	public SaveLoadData savedata;

	public string SaveFilePath;



	// Use this for initialization
	void Start () 
	{
		DontDel=GameObject.Find ("DontDelete");
		if (DontDel != null) 
		{
			saveloadFunctions=DontDel.GetComponent<SaveLoadFunctions> ();
			saveloadManager = DontDel.GetComponent<SaveLoadManager> ();
		}

		SaveFilePath=Application.persistentDataPath + "/";
		 
		savedata = new SaveLoadData ();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			paused = !paused;

			if (paused == true)
			{
				Time.timeScale = 0;
				GameMenu.SetActive(true);
			}
			else if (paused == false)
			{
				ReturnToGamePressed();                
			}
		}
	}

	public void ReturnToGamePressed()
	{
		paused = false;
		GameMenu.SetActive(false);
		Time.timeScale = 1;

	}


	public void SaveGamePressed()
	{		
		savedata.Level = EditorApplication.currentScene.ToString ();
		saveloadManager.savedata = savedata;

		saveloadManager.SaveFile (SaveFilePath);
		Debug.Log ("Saved");
		ReturnToGamePressed ();

	}





}
