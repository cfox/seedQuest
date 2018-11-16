﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/GameState")]
public class GameStateData : ScriptableObject {
    public bool startPathSearch = false;
    public bool pathComplete = false;
    public bool showPathTooltip = false;
    public bool inRehersalMode = true;
    public bool isPaused = false;
    public bool isStarted = false;
    public bool isCameraPaused = false;

    public bool musicMute;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    public Sprite uncheckedState;
    public Sprite checkedState;
    public TMPro.TMP_FontAsset actionItemFont;
    public Material actionItemMaterial;

    public LayerMask interactableMask;
    public float interactionRadius = 4.0f;

    public InteractableID currentAction;
    public InteractableID[] targetList;

    public string SeedString;
    public string recoveredSeed;
    public InteractableLog actionLog;

    public int SiteBits = 4;
    public int SpotBits = 4;
    public int ActionBits = 3;
    public int ActionCount = 4;
    public int SiteCount = 4;


}