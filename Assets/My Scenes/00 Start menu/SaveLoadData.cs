using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveLoadData 
{

	public int HeroHealth;
	public int HeroMana;

	public float HeroPositionX;
	public float HeroPositionY;
	public float HeroPositionZ;

	public Item[] InventoryItems;
	public Item[] WearableItems;

	public string Level;


}
