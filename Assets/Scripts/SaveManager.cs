using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{

	static string Path = Application.persistentDataPath + "PlayerData.batata";
	
	public static void Save()
	{
		BinaryFormatter br = new BinaryFormatter();

		FileStream file = File.Open(Path,FileMode.OpenOrCreate);

		br.Serialize(file, GameManager.instance.data);

		file.Close();
	}
	public static void LoadData()
	{
		if(File.Exists(Path))
		{
			BinaryFormatter br = new BinaryFormatter();
			FileStream file = File.Open(Path, FileMode.Open);

			GameManager.instance.data =(PlayerData) br.Deserialize(file);
			file.Close();



		}

		else
		{
			print(GameManager.instance == null);
			GameManager.instance.NewPlayer();
			
			Save();


		}

	}



}
