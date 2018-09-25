﻿using UnityEngine;

public class InteractableManager : MonoBehaviour {

    public float nearDistance = 2.0f;
    public GameObject actionSpotIcon;
    private Interactable activeItem = null;
    public InteractableLog log;
    public Interactable[] list;

    static public Interactable[] InteractableList
    {
        get { return findAllInteractables(); }
    }

    static public InteractableLog Log {
        get { return Instance.log; }
    }

    static private InteractableManager instance = null;
    static public InteractableManager Instance {
        get {
            if (instance == null)
                instance = GameObject.FindObjectOfType<InteractableManager>();
            return instance;
        }
    }

    private void Awake() {
        log = new InteractableLog();
        list = InteractableList;
    }

    static Interactable[] findAllInteractables() {
        return GameObject.FindObjectsOfType<Interactable>();
    }

    static void findNearInteractables() {
        Interactable[] list = FindObjectsOfType<Interactable>();

        foreach (Interactable item in list)
        {
            Vector3 playerPosition = PlayerManager.Position;
            float dist = (item.transform.position - playerPosition).magnitude;
            if (dist < Instance.nearDistance)
                doNearInteractable(true);
            else
                doNearInteractable(false);
        }  
    }

    static void doNearInteractable(bool isNear) {
        
    }

    static ParticleSystem getEffect() {
        ParticleSystem effect;

        InteractableStateData data = Instance.activeItem.stateData;
        if(data == null)
            effect = EffectsManager.createEffect(Instance.activeItem.transform);
        else if(data.effect == null)
            effect = EffectsManager.createEffect(Instance.activeItem.transform);
        else 
            effect = EffectsManager.createEffect(Instance.activeItem.transform, data.effect);

        return effect;
    }

    static public void showActions(Interactable interactable) {
        InteractableManager.Instance.activeItem = interactable;
        GameManager.State = GameState.Interact;
        InteractableUI.show(interactable);
    }

    /// <summary> Do Interaction - Does Action, actives effect, logs action, updates path (for rehersal) and exits interactable ui dialog </summary>
    static public void doInteractableAction(int actionIndex) {

        ParticleSystem effect = getEffect();
        effect.Play();

        Instance.activeItem.doAction(actionIndex);
        InteractableManager.Log.Add(Instance.activeItem, actionIndex);
        Instance.activeItem = null;
        GameManager.State = GameManager.PrevState;
        PathManager.NextPathSegment();
    }
}