using UnityEngine;
using System.Collections;

public class Puppy: MonoBehaviour
{
	public class Attributes{

		public int Strength;
		public int MaxStr;
		public int MinStr;

		public int Agility;
		public int MaxAgi;
		public int MinAgi;

		public int Intelligence;
		public int MaxInt;
		public int MinInt;

		/*public int[] Perception;
		public int[] Attitude;
		public int Playfulness;
		public int Obedience;
		public int Motivation;
		public int Height;
		public int Weight;
		public int Length;
		public int Furcoat;
		public int Pawsize;
		public int ExerciseNeed;
		*/
		public Attributes(){

			Strength = 0;
			MaxStr=100;
			MinStr=100;

			Agility =0;
			MaxAgi=100;
			MinAgi=0;

			Intelligence=0;
			MaxInt=100;
			MinInt=0;
			/*Perception=new int[]{0,0,0};
			Attitude=new int[] {0,0,0,0,0};
			 Playfulness=0;
			 Obedience=0;
			 Motivation=0;
			 Height=0;
			 Weight=0;
			 Length=0;
			 Furcoat=0;
			 Pawsize=0;
			 ExerciseNeed=0;*/
		}
		public Attributes(int str, int minstr, int maxstr, int agi, int minagi, int maxagi, int intel, int minint, int maxint)
			/*, int[] per, int[] att, int play, int obe, int mot, int hg, int wg, int ln, int fur, int paw, int exer)*/
		{
			Strength = str;
			MaxStr=maxstr;
			MinStr=minstr;
			
			Agility =agi;
			MaxAgi=maxagi;
			MinAgi=minagi;
			
			Intelligence=intel;
			MaxInt=maxint;
			MinInt=minint;
			/*Perception=per;
			Attitude=att;
			Playfulness=play;
			Obedience=obe;
			Motivation=mot;
			Height=hg;
			Weight=wg;
			Length=ln;
			Furcoat=fur;
			Pawsize=paw;
			ExerciseNeed=exer;
			*/
		}

		//Testmethod to print data
		override public string ToString(){
			return "Strength:" + getStr()[0] + " Agility:" + Agility + " Intelligence:" + Intelligence+"\n"
				+"StrLimits:"+getStr()[1]+"-"+getStr()[2]+" AgiLimits:"+MinAgi+"-"+MaxAgi+" IntLimits:"+MinInt+"-"+MaxInt;
		}

		//Utility methods
		//Request attributes
		public int[] getStr(){
			return new int[]{Strength, MinStr, MaxStr};
		}
		public int[] getAgi(){
			return new int[]{Agility, MinAgi, MaxAgi};
		}
		public int[] getInt(){
			return new int[]{Intelligence, MinInt, MaxInt};
		}

		//Methods to change attribute values
		public void changeStr(int amount){
			if (amount>=0 && Strength + amount <= MaxStr) {
				Strength = Strength + amount;
			} else if (amount>=0) {
				Strength=MaxStr;
			} else if (amount < 0 && Strength + amount >= MinStr) {
				Strength = Strength + amount;
			} else {
				Strength=MinStr;
			}
		}
		public void changeAgi(int amount){
			if (amount>=0 &&Agility + amount <= MaxAgi) {
				Agility = Agility + amount;
			} else if (amount>=0) {
				Agility=MaxAgi;
			} else if (amount < 0 && Agility + amount >= MinAgi) {
				Agility = Agility + amount;
			} else {
				Agility=MinAgi;
			}
		}
		public void changeInt(int amount){
			if (amount >=0 && Intelligence + amount <= MaxInt) {
				Intelligence = Intelligence + amount;
			} else if (amount>=0) {
				Intelligence=MaxInt;
			} else if (amount < 0 && Intelligence + amount >= MinInt) {
				Intelligence = Intelligence + amount;
			} else {
				Intelligence=MinInt;
			}
		}

	}
	//Continue imaginary savegame state
	public Attributes myPuppy =new Attributes(55, 20, 88, 33, 10, 90 , 25, 10, 55);/*,new int[]{1,1,1} ,new int[]{1,1,1,1,1},1,1,1,1,1,1,1,1,1);
*/
	void Start(){
		Debug.Log (myPuppy.ToString()); 
		myPuppy.changeAgi (0);
		myPuppy.changeStr (34);
		myPuppy.changeInt (-30);
		Debug.Log (myPuppy.ToString()); 

	}
}
