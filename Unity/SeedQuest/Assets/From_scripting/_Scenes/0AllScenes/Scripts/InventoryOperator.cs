﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOperator : MonoBehaviour {

    public GameObject backPanel;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    /* public GameObject item5;
    public GameObject item6;
    public GameObject item7;
    public GameObject item8; */

    private bool item1Active = false;
    private bool item2Active = false;
    private bool item3Active = false;
    private bool item4Active = false;
    private bool showing = false;


    private int index = 0;

	// Use this for initialization
	void Start () {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
        item4.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void addItem(int ID){
        
        switch (ID)
        {
            case 100001: // rock
                // do something
                //index += 1;
                Debug.Log("Adding rock to inventory.");
                item2Active = true;
                break;
            case 100002: // soccer ball
                //index += 1;
                Debug.Log("Adding soccer ball to inventory.");
                item1Active = true;
                break;
            default:
                break;
                
        }
        if (showing){
            show();
        }


    }

    public void show(){

        showing = true;

        backPanel.SetActive(true);

        if (item1Active == true){
            item1.SetActive(true);
        }
        if (item2Active == true)
        {
            item2.SetActive(true);
        }
        if (item3Active == true)
        {
            item3.SetActive(true);
        }
        if (item4Active == true)
        {
            item4.SetActive(true);
        }

    }

    public void hide(){

        showing = false;
        backPanel.SetActive(false);
        item1.SetActive(false);
        item2.SetActive(false);
        //item3.SetActive(false);
        //item4.SetActive(false);


    }

    public void removeItem(){
        
    }

}
