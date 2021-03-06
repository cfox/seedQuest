﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SeedQuest.Interactables;
using SeedQuest.SeedEncoder;

public enum ProgressTrackerLocation { TopLeft, BottomCenter };

public class ProgressTrackerUI : MonoBehaviour {

    public ProgressTrackerLocation location = ProgressTrackerLocation.BottomCenter;
    public float progress = 0;
    public Sprite tickOnIcon;
    public Sprite tickOffIcon;

    private RectTransform progressCanvas;
    private Image progressBar;
	private Image[] progressTicks;
    private TextMeshProUGUI progressText;
    private Image progressPartialIcon;

    void Start() {
        progressCanvas = GameObject.FindGameObjectWithTag("ProgressCanvas").GetComponent<RectTransform>();
        progressBar = GameObject.FindGameObjectWithTag("ProgressBar").GetComponent<Image>();
        if(GameObject.FindGameObjectWithTag("ProgressTicks") != null)
            progressTicks = GameObject.FindGameObjectWithTag("ProgressTicks").GetComponentsInChildren<Image>();
        progressText = progressCanvas.GetComponentInChildren<TextMeshProUGUI>();
        progressPartialIcon = GameObject.FindGameObjectWithTag("ProgressText").GetComponentsInChildren<Image>()[1];

        SetProgressBar();
        SetProgressTicks();
        SetLocation();
    }

    private void Update() {
        setProgress();
        SetProgressBar();
        SetProgressTicks();
        SetProgressText();
        SetLocation();
    }

    private void setProgress() {
        if (GameManager.Mode == GameMode.Rehearsal) {
            progress = InteractablePath.PercentComplete / 100.0f;
        }
        else if (GameManager.Mode == GameMode.Recall) {
            progress = InteractableLog.PercentComplete / 100.0f;
        }          
    }

    private void SetProgressBar() {
        float width = 400.0f;
        float value = width * (progress - 1);
        progressBar.GetComponent<RectTransform>().offsetMax = new Vector2(value, 0);
    }

    private void SetProgressTicks() {
        if (progressTicks == null) return;
        int value =  (int )Mathf.Round(progress * InteractableConfig.ActionsPerGame);
        for (int i = 0; i < progressTicks.Length; i++) {
            if (i < value)
                progressTicks[i].sprite = tickOnIcon;
            else
                progressTicks[i].sprite = tickOffIcon;
        }
    }

    private void SetProgressText() {
        SeedConverter converter = new SeedConverter();
        string SeedString = converter.DecodeSeed();
        int length = Mathf.RoundToInt(10.0f / (float)InteractableConfig.ActionsPerGame * (float)InteractableLog.Log.Count);
        progressText.text =  SeedString.Substring(0, length);
        progressPartialIcon.GetComponent<RectTransform>().localPosition = new Vector3(length * 22 - 80, 0, 0);
    }

    private void SetLocation() {
        if (location == ProgressTrackerLocation.BottomCenter) {
            progressCanvas.anchorMax = new Vector2(0.5f, 0);
            progressCanvas.anchorMin = new Vector2(0.5f, 0);
            progressCanvas.pivot = new Vector2(0.5f, 0);
            progressCanvas.anchoredPosition3D = Vector3.zero;
        }
        else if (location == ProgressTrackerLocation.TopLeft) {
            progressCanvas.anchorMax = new Vector2(0, 1);
            progressCanvas.anchorMin = new Vector2(0, 1);
            progressCanvas.pivot = new Vector2(0.5f, 0);
            progressCanvas.anchoredPosition3D = new Vector3(265, -380, 0);
        }
    }
}