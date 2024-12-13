using System;
using System.Collections.Generic;
using Lean.Localization;
using Project.Configs.Stats;
using Project.Interfaces.Data;
using Project.Interfaces.Stats;
using Project.Systems.Data;
using Project.Systems.Stats;
using Project.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Upgrades
{
    public class StatUpgradeBar : MonoBehaviour
    {
        private const int Zero = 0;
        private const int One = 1;
        private const string LevelToken = "LevelToken";
        
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statDescription;
        [SerializeField] private GameObject _levelProgressTemlplate;
        [SerializeField] private Image _levelProgressPrefab;

        [SerializeField] private Button _upgradeButton;

        [SerializeField] private UpgradeCostView _upgradeCostViewPrefab;
        [SerializeField] private RectTransform _upgradePriceHolder;

        private readonly List<UpgradeCostView> _upgradePriceView = new();
        private readonly List<Image> _levelProgressImages = new();

        private StatConfig _config;
        private IUpgradableStats _stats;
        private IPlayerStorage _playerStorage;
        private StatType _statType;

        public event Action StatUpgraded;

        private string LevelLabel => LeanLocalization.GetTranslationText(LevelToken);
        private int CurrentStatLevel => _stats.GetStatLevel(_statType);

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(TryUpgradeStat);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(TryUpgradeStat);
        }

        public void Initialize(StatConfig config, IUpgradableStats stats, IPlayerStorage playerStorage)
        {
            _config = config;
            _stats = stats;
            _playerStorage = playerStorage;

            _statType = _config.StatType;

            SpawnLevelProgress(CurrentStatLevel);
        }

        public void Fill()
        {
            _statName.text = _config.Name;
            _statDescription.text = _config.Description;
            
            SetLevelProgress(CurrentStatLevel);
            CheckUpgradePrice();
        }

        public void CheckUpgradePrice()
        {
            int currentLevel = _stats.GetStatLevel(_statType);
            List<GameResourceAmount> upgradePrice = _config.GetUpgradePrice(currentLevel);

            UpdatePriceView(upgradePrice, currentLevel);
        }

        private void SpawnLevelProgress(int currentLevel)
        {
            for (int i = 0; i < _config.MaxLevel; i++)
            {
                var progress = Instantiate(_levelProgressPrefab, _levelProgressTemlplate.transform);

                if (i >= currentLevel)
                {
                    progress.color = new Color(Zero, Zero, Zero, Zero);
                }
                
                _levelProgressImages.Add(progress);
            }
        }

        private void SetLevelProgress(int currentLevel)
        {
            for (int i = 0; i < _levelProgressImages.Count; i++)
            {
                Color color = _levelProgressImages[i].color;
                
                if (i < currentLevel)
                {
                    color = Color.white;
                }
                else
                {
                    color = Color.clear;
                }

                _levelProgressImages[i].color = color;
            }
        }

        private void UpdatePriceView(List<GameResourceAmount> upgradePrice, int currentLevel)
        {
            if (_config.IsMaxLevel(currentLevel))
            {
                _upgradeButton.gameObject.SetActive(false);
                return;
            }

            SetUpgradePrice(upgradePrice);
            _upgradeButton.interactable = _playerStorage.CanSpend(upgradePrice);
        }

        private void SetUpgradePrice(List<GameResourceAmount> upgradePrice)
        {
            foreach (var upgradeCostView in _upgradePriceView)
            {
                upgradeCostView.Hide();
            }

            for (int i = 0; i < upgradePrice.Count; i++)
            {
                if (_upgradePriceView.Count <= i)
                {
                    UpgradeCostView upgradeCostView = Instantiate(_upgradeCostViewPrefab, _upgradePriceHolder);
                    _upgradePriceView.Add(upgradeCostView);
                }

                GameResourceAmount upgradeCost = upgradePrice[i];
                Sprite resourceSprite = upgradeCost.Resource.Sprite;
                string resourceAmount = upgradeCost.Amount.ToNumericalString();
                bool canSpend = _playerStorage.CanSpend(upgradeCost);

                _upgradePriceView[i].Set(resourceSprite, resourceAmount, canSpend);
            }
        }

        private void TryUpgradeStat()
        {
            List<GameResourceAmount> upgradePrice = _config.GetUpgradePrice(CurrentStatLevel);

            if (_playerStorage.TrySpendResource(upgradePrice))
            {
                _stats.UpgradeStat(_statType);
                Fill();

                StatUpgraded?.Invoke();
            }
        }
    }
}