﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class startMenuDemo : MonoBehaviour {

    public InputField keyInputField;
    public Text keyString = null;
    public OTPworker otpWorker;

    public GameObject encryptButton;
    public GameObject demoKeyButton;
    private bool allowEnter;
    private bool entered;

	void Start () 
    {
        otpWorker = FindObjectOfType<OTPworker>();
        DideryDemoManager.isDemo = false;
        entered = false;
        allowEnter = false;
    }
	
	void Update () 
    {
        if (allowEnter && keyInputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            encryptKey();
        }
        else
            allowEnter = keyInputField.isFocused;
    }

    public void encryptKey()
    {
        if (!entered)
        {
            //Debug.Log(keyInputField.text);
            //otpWorker.encryptKey(keyInputField.text);
            deactivateEncryptButtons();
            changeKeyToCensored();
            entered = true;
        }
        else
        {
            deactivateEncryptButtons();
        }
    }

    public void deactivateEncryptButtons()
    {
        encryptButton.SetActive(false);
        demoKeyButton.SetActive(false);
    }

    public void changeKeyToCensored()
    {
        keyInputField.text = censoredKey(keyInputField.text);
    }

    public void useDemoKey()
    {
        DideryDemoManager.isDemo = true;
        DideryDemoManager.demoBlob = "4040C1A90886218984850151AC123249";
        SeedManager.InputSeed = "abcd";
        deactivateEncryptButtons();
    }

    public void testGetKey()
    {
        otpWorker.getEncryptedKey();
    }

    public void testDecrypt()
    {
        string seed = SeedManager.InputSeed;
        //Debug.Log("Seed: " + seed);
        byte[] keyByte = otpWorker.decryptFromBlob(seed);
        string finalKey = Encoding.ASCII.GetString(keyByte);
        keyString.text = finalKey;
        Debug.Log("Decrypted key: " + finalKey);
    }

    public string censoredKey(string key)
    {
        char[] oldKey = key.ToCharArray();
        char[] newKey = new char[key.Length];
        for (int i = 0; i < oldKey.Length; i++)
        {
            Debug.Log("i: " + i + " other: " + (oldKey.Length - 5));
            if (i > oldKey.Length - 5)
            {
                newKey[i] = oldKey[i];
            }
            else
                newKey[i] = '*';
        }
        string returnStr = new string(newKey);
        return returnStr;
    }

}