﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SettingsGame
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Dropdown qualityDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        private Settings settings;
        private string pathToSettings;
        private List<String> options;
        private int currentResolutionIndex;

        private Resolution[] resolutions;

        private void Start()
        {
            pathToSettings = Path.Combine(Application.streamingAssetsPath, "Settings.xml");
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            options = new List<string>();

            currentResolutionIndex = 0;
            getListOfResolutions();

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

            getSettings();

        }

        private void getSettings()
        {
            settings = XMLWorker.deserialize<Settings>(pathToSettings);

            if (settings == null)
            {
                saveSettings(currentResolutionIndex, 2, true);
                setValuesInsettings();
            }
            else
            {
                setValuesInsettings();
            }
        }

        private void setValuesInsettings()
        {
            resolutionDropdown.value = settings.resolutions;
            resolutionDropdown.RefreshShownValue();

            qualityDropdown.value = settings.levelOfDetails;
            qualityDropdown.RefreshShownValue();

            fullscreenToggle.isOn = settings.fullscreen;
        }

        private void getListOfResolutions()
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        public void setResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void setQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        public void setFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void saveSettings(int resolution, int quality, bool isFullscreen)
        {
            settings = new Settings();
            settings.resolutions = resolution;
            settings.levelOfDetails = quality;
            settings.fullscreen = isFullscreen;
            XMLWorker.serialize(settings, pathToSettings);
        }

        public void saveSettings()
        {
            settings.fullscreen = fullscreenToggle.isOn;
            settings.resolutions = resolutionDropdown.value;
            settings.levelOfDetails = qualityDropdown.value;
            XMLWorker.serialize(settings, pathToSettings);
        }
    }
}
