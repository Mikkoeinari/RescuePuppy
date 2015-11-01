using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyBar : MonoBehaviour {

	public int energy;
	public Slider energySlider;
	// Use this for initialization
	void Start () {
		energySlider.value=energy;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public int getEnergy(){
		return energy;
	}
	public void setEnergy(int newEnergy) {
		energy = newEnergy;
		energySlider.value=energy;

	}
}
