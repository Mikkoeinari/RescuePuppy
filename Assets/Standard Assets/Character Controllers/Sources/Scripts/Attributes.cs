using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Attributes : MonoBehaviour {

	public float energy, hunger, morale;
	public Slider energySlider, hungerSlider, moraleSlider ;
	// Use this for initialization
	void Start () {
		energySlider.value = energy;
		hungerSlider.value = hunger;
		moraleSlider.value = morale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public float getEnergy(){
		return energy;
	}
	public void setEnergy(float newEnergy) {
		if (newEnergy >= energySlider.minValue && newEnergy <= energySlider.maxValue) {
			energy = newEnergy;
			energySlider.value = energy;
		}
	}
	public float getHunger(){
		return hunger;
	}
	public void setHunger(float newHunger) {
		if (newHunger >= hungerSlider.minValue && newHunger <= hungerSlider.maxValue) {
			hunger = newHunger;
			hungerSlider.value = hunger;
		}
	}
	public float getMorale(){
		return morale;
	}
	public void setMorale(float newMorale) {
		if (newMorale >= moraleSlider.minValue && newMorale <= moraleSlider.maxValue) {
			morale = newMorale;
			moraleSlider.value = morale;
		}
	}
}
