using UnityEngine;
using System.Collections;

public enum ItemKind{ 
	Potion, 
	Ring
};

public class PickUpAbleItem : MonoBehaviour {

	public ItemKind itemkind;

	void PickUp () 
	{
		Destroy (gameObject);


	}


}
