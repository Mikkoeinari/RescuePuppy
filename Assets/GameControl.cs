using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float energy;
	public float hunger;
	public float morale;

	void awake (){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy(gameObject);
		}
		
	}
	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 100, 20), "LOAD")) {
			GameControl.control.load ();
		}
		if (GUI.Button (new Rect (10, 30, 100, 20), "SAVE")) {
			GameControl.control.save ();
		}
	}
	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/puppy.dat", FileMode.Open);
		PuppyData data = new PuppyData ();
		data.energy = energy;
		data.hunger = hunger;
		data.morale = morale;
		bf.Serialize (file, data);
		file.Close ();
	}
	public void load(){
		if (File.Exists (Application.persistentDataPath + "/puppy.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/puppy.dat", FileMode.Open);
			PuppyData data=(PuppyData)bf.Deserialize(file);
			file.Close();
			energy = data.energy;
			hunger = data.hunger;
			morale = data.morale;
		}

	}
[Serializable]
	class PuppyData{
		public float energy;
		public float hunger;
		public float morale;
	}
}
