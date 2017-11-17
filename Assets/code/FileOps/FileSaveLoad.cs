using UnityEngine;
using System.Collections;
using System;
using System.IO;
//using System.IO.Stream;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class FileSaveLoad : MonoBehaviour {
	public void SaveFileData(String filename, SavedData obj) 
	{
		Debug.Log("Writing Stream to Disk.");
		Stream fileStream = File.Open("C:\\" + filename, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fileStream, obj);

		fileStream.Close();
	}

	public SavedData LoadFileData(String filename)  
	{
		//try
		{
			Debug.Log("Reading Stream from Disk.");
			Stream fileStream = File.Open("C:\\" + filename, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			SavedData obj = (SavedData)formatter.Deserialize(fileStream);
			fileStream.Close();
			return obj;	
		}

	}



	public void SaveFileString(String filename, String obj) 
	{
		Debug.Log("Writing Stream to Disk.");
		Stream fileStream = File.Open("C:\\" + filename, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fileStream, obj);

		fileStream.Close();
	}

	public string LoadFileString(String filename)  
	{
		//try
		{
			Debug.Log("Reading Stream from Disk.");
			Stream fileStream = File.Open("C:\\" + filename, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			String obj = (String)formatter.Deserialize(fileStream);
			fileStream.Close();
			return obj;	
		}

	}


	public void SaveFileInt(String filename, int obj) 
	{
		Debug.Log("Writing Stream to Disk.");
		Stream fileStream = File.Open("C:\\" + filename, FileMode.Create);
		BinaryFormatter formatter = new BinaryFormatter();
		formatter.Serialize(fileStream, obj);

		fileStream.Close();
	}

	public int LoadFileInt(String filename)  
	{
		//try
		{
			Debug.Log("Reading Stream from Disk.");
			Stream fileStream = File.Open("C:\\" + filename, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			int obj = (int)formatter.Deserialize(fileStream);
			fileStream.Close();
			return obj;	
		}

	}


}
