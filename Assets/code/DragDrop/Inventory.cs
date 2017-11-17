using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject SmallItemsPanel;
    public GameObject BigItemsPanel;

    public int Dollars = 5000;

    public Text TextDollars;

    // Use this for initialization
    void Start ()
    {
        UpdateDollarsLabel();
    }

    public void UpdateDollarsLabel()
    {
        TextDollars.text = Dollars.ToString() + "$";

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void _SmallItemsButton()
    {
        BigItemsPanel.SetActive(false);
        SmallItemsPanel.SetActive(true);
    }

    public void _BigItemsButton()
    {
        SmallItemsPanel.SetActive(false);
        BigItemsPanel.SetActive(true );
        
    }


}
