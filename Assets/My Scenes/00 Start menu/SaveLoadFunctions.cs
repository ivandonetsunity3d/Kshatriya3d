using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadFunctions : MonoBehaviour 
{
	//public string filePath = Application.persistentDataPath;
	//public string SaveFilePath;

	//void Start()
	//{
		//SaveFilePath=filePath + "/";

	//}


	public void SaveFileData(String SaveFilePath, String filename, SaveLoadData obj)
	{
		Debug.Log("Writing Stream to Disk.");
		//File.Delete(SaveFilePath + filename);
		Stream fileStream = File.Open(SaveFilePath + filename, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fileStream, obj);

		fileStream.Close();
	}

	public SaveLoadData LoadFileData(String SaveFilePath, String filename)
	{
		{
			Debug.Log("Reading Stream from Disk.");
			Stream fileStream = File.Open(SaveFilePath + filename, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			SaveLoadData obj = (SaveLoadData)formatter.Deserialize(fileStream);
			fileStream.Close();
			return obj;
		}
	}


}
