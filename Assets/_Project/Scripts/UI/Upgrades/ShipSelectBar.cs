using System;
using System.Collections;
using System.Collections.Generic;
using Project.Configs.Ships;
using Project.Interfaces.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Upgrades
{
    public class ShipSelectBar : MonoBehaviour
    {
        [SerializeField] private Button _showButton;
        [SerializeField] private Image _shipIcon;
        [SerializeField] private Image _selectGlow;

        private ShipConfig _config;
        private IPlayerStorage _playerStorage;
        private TMP_Text _shipName;
        private Button _selectButton;
        private Button _buyButton;
        
        public event Action ShipSelected;

        public void Initialize(ShipConfig shipConfig, IPlayerStorage playerStorage, TMP_Text shipName, Button selectButton, Button buyButton)
        {
            _config = shipConfig;
            _playerStorage = playerStorage;
            _shipName = shipName;
            _selectButton = _selectButton;
            _buyButton = buyButton;
        }
        
        private void OnEnable()
        {
            _showButton.onClick.AddListener(ShowShip);
        }

        private void OnDisable()
        {
            _showButton.onClick.RemoveListener(ShowShip);
        }

        public void Fill()
        {
            _shipName.text = _config.Name;
            _shipIcon.sprite = _config.Sprite;
        }

        public void HideGlow()
        {
            _selectGlow.enabled = false;
        }

        private void ShowShip()
        {
            ShipSelected?.Invoke();
            _selectGlow.enabled = true;
        }
    }
}