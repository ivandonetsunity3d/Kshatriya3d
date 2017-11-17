using UnityEngine;
using System.Collections;

public class DiabloCameraController : MonoBehaviour {

	public GameObject player; //The offset of the camera to centrate the player in the X axis 
	public float offsetX = -8f; //The offset of the camera to centrate the player in the Z axis 
	public float offsetY = 4f; //The maximum distance permited to the camera to be far from the player, its used to make a smooth movement 
	public float offsetZ = 2f; //The maximum distance permited to the camera to be far from the player, its used to make a smooth movement 

	private float PosX;
	private float PosY;
	private float PosZ;


	
	// Update is called once per frame
	void Update () 
	{
        if (player != null) 
        {
            PosX = player.transform.position.x + offsetX;
            PosY = player.transform.position.y + offsetY;
            PosZ = player.transform.position.z + offsetZ;

            this.transform.position = new Vector3(PosX, PosY, PosZ);
            this.transform.LookAt(player.transform);
        }
		
	}

}
