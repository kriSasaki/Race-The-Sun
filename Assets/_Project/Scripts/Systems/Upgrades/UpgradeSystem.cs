using System;
using System.Collections.Generic;
using Project.Systems.Cameras;
using Project.UI.Panels;
using Project.UI.Settings;
using Project.UI.Upgrades;
using UnityEngine;
using Zenject;

namespace Project.Systems.Upgrades
{
    public class UpgradeSystem : IInitializable, IDisposable
    {
        private readonly UpgradeButton _upgradeButton;
        private readonly UpgradeWindow _upgradeWindow;
        private readonly SettingsButton _settingsButton;
        private readonly SettingsWindow _settingsWindow;
        private readonly ButtonsPanel _buttonsPanel;
        private readonly InfoPanel _infoPanel;
        
        private CameraSystem _cameraSystem;

        public UpgradeSystem(UpgradeButton upgradeButton, 
            UpgradeWindow upgradeWindow, 
            CameraSystem cameraSystem, 
            SettingsButton settingsButton, 
            SettingsWindow settingsWindow, 
            ButtonsPanel buttonsPanel,
            InfoPanel infoPanel)
        {
            _upgradeButton = upgradeButton;
            _upgradeWindow = upgradeWindow;
            _cameraSystem = cameraSystem;
            _settingsButton = settingsButton;
            _settingsWindow = settingsWindow;
            _buttonsPanel = buttonsPanel;
            _infoPanel = infoPanel;
        }
        
        public void Initialize()
        {
            _upgradeButton.Bind(OpenUpgrades);
            _upgradeWindow.OnHided += ShowUI;
        }
        
        public void Dispose()
        {
            _upgradeWindow.OnHided -= ShowUI;
        }

        private void OpenUpgrades()
        {
            _upgradeWindow.Open();
            _cameraSystem.GoToPlayer();
            _settingsButton.Hide();
        }

        private void ShowUI()
        {
            _settingsButton.Show(_settingsWindow.Open);
            _buttonsPanel.Open();
            _infoPanel.Open();
        }
    }
}