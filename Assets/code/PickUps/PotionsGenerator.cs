using UnityEngine;
using System.Collections;

public enum PotionType{ 
	PotionRed, 
	PotionGreen, 
	PotionBlue, 
	PotionDarkRed
};

public enum PotionKind{ 
	Health, 
	Mana,
	Regeneration
};


public class PotionsGenerator : MonoBehaviour {

	public GameObject PotionRed, PotionBlue, PotionGreen;
    public GameObject PotionDarkRed;


	public PotionType ThisPotion;

	//public Potion NewPotion  ;
	public Potion PotionScript;


	public void CreatePotion (Vector3 EnemyPosition) 
	{
		EnemyPosition.y = EnemyPosition.y + 0.1f;

        GameObject RndPotion;

		GameObject RndPotionName=PotionRed;

		ThisPotion = (PotionType)Random.Range (0, System.Enum.GetNames(typeof (PotionType) ).Length);

		//NewPotion= new Potion();


		switch (ThisPotion) 
		{
		case PotionType.PotionRed:
			RndPotionName = PotionRed;
			//NewPotion.potionkind = PotionKind.Health;
			break;
		case PotionType.PotionGreen:
			RndPotionName = PotionGreen;
			//NewPotion.potionkind = PotionKind.Regeneration;
			break;
		case PotionType.PotionBlue:
			RndPotionName = PotionBlue;
			//NewPotion.potionkind = PotionKind.Mana;
			break;
		case PotionType.PotionDarkRed:
			RndPotionName = PotionDarkRed;
			//NewPotion.potionkind = PotionKind.Health;
			break;
		/*case PotionType.PotionViolet:
			RndPotionName = PotionViolet;
			//NewPotion.potionkind = PotionKind.Mana;
			break;*/

		}

        //int RndPotionNumber = Random.Range (1, 5);

        /*switch (RndPotionNumber)
		{
		case 5:
			RndPotionName = PotionRed;
			break;
		case 4:
			RndPotionName = PotionDarkRed;
			break;
		case 3:
			RndPotionName = PotionGreen;
			break;
		case 2:
			RndPotionName = PotionBlue;
			break;
		case 1:
			RndPotionName = PotionViolet;
			break;
		default:			
			break;*/

        RndPotion = Instantiate(RndPotionName, EnemyPosition, Quaternion.identity);//  as Transform;
		RndPotion.transform.Rotate(270,0,0);
		RndPotion.name = "Potion";

		PotionScript = 		RndPotion.GetComponent<Potion> ();

		switch (ThisPotion) 
		{
		case PotionType.PotionRed:
			PotionScript.potionkind = PotionKind.Health;
			break;
		case PotionType.PotionGreen:
			PotionScript.potionkind = PotionKind.Regeneration;
			break;
		case PotionType.PotionBlue:
			PotionScript.potionkind = PotionKind.Mana;
			break;
		case PotionType.PotionDarkRed:
			PotionScript.potionkind = PotionKind.Health;
			break;
		/*case PotionType.PotionViolet:
			PotionScript.potionkind = PotionKind.Mana;
			break;*/

		}

		//PotionScript.potionkind = NewPotion.potionkind;





	}




}
