﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interactable
{
    public Transform transform;
    public GameObject effect;
    public string label;
    public string description;
}

public class PathFinder : MonoBehaviour
{
    static PathFinder instance;
    public LayerMask interactableMask;
    public Transform player;
    public GameObject Tooltip;
    public List<Interactable> targets = new List<Interactable>();
    public LineRenderer line;

    private Pathfinding pathfinding;
    private Vector3[] path;
    private int currentIndex = 0;
    private float targetRadius = 4.0F;
    private bool nearInteractable = false;

    private void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
        Tooltip.SetActive(false);
    }

    private void Start() {
        InvokeRepeating("UpdatePath", 0, 1.0F);

        targets[0].effect.SetActive(false);
    } 

    private void Update()
    {
        nearInteractable = Physics.CheckSphere(player.position, targetRadius, interactableMask);
        if(nearInteractable) {
            UnityEngine.UI.Text[] t = Tooltip.GetComponentsInChildren<UnityEngine.UI.Text>();
            t[0].text = targets[currentIndex].label;
            t[1].text = targets[currentIndex].description;

            Tooltip.SetActive(true);
        }
        else {
            Tooltip.SetActive(false);
        }

        if(nearInteractable && Input.GetButtonDown("Jump")) {
            DoAction();
        }

        if (Input.GetMouseButton(0)) {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)) {
                if (hit.transform == targets[currentIndex].transform) {
                    DoAction(); 
                }
            }

        }

    } 

    private void DoAction() { 
        Debug.Log("Completed Action:" + targets[currentIndex].label);
        if (targets[currentIndex].effect != null)
            targets[currentIndex].effect.SetActive(true);

        currentIndex++;
    }

    private void UpdatePath() {
        path = pathfinding.FindPath(player.position, targets[currentIndex].transform.position);
        if (path.Length > 0)
            DrawPath();
    }

    public void NextPath() {
        currentIndex++;
    }

    void DrawPath() {
        line.positionCount = path.Length;
        line.SetPositions(path);
    }

    private void OnDrawGizmos() {
        if (nearInteractable == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, targetRadius);
        }  
    }
}
