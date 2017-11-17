using UnityEngine;
using System.Collections;
using UnityEditor;

public class SaveLoadManager : MonoBehaviour 
{
	public string filename = "HeroSavedGame.bin";

	public SaveLoadData savedata;
	SaveLoadFunctions savefunctions;


	void Start()
	{
		savefunctions = transform.GetComponent<SaveLoadFunctions> ();
		savedata = new SaveLoadData ();

		DontDestroyOnLoad (transform.gameObject);
	}


	public void LoadFile(string path)
	{
		savedata = savefunctions.LoadFileData (path,filename);

		//string fixedpath = Application.dataPath.



		savedata.Level = Application.dataPath  + savedata.Level ;
		//EditorApplication.path

		//Application.dataPath.ToString();
		//m.CompareTo(


		Application.LoadLevel (savedata.Level);

	}


	public void SaveFile(string path)
	{
		savefunctions.SaveFileData (path,filename, savedata);

	}



}
