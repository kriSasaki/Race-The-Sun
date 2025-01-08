using System.Collections.Generic;
using Ami.BroAudio;
using Project.Configs.Ships;
using Project.Configs.Stats;
using Project.Configs.UI;
using Project.Interfaces.Audio;
using Project.Interfaces.Data;
using Project.Interfaces.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.Upgrades
{
    [RequireComponent(typeof(Canvas))]
    public class UpgradeWindow : UiWindow
    {
        [SerializeField] private StatUpgradeBar _barPrefab;
        [SerializeField] private RectTransform _barHolder;
        [SerializeField] private ShipSelectBar _shipSelectBarPrefab;
        [SerializeField] private RectTransform _shipHolder;
        [SerializeField] private TMP_Text _shipName;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _buyButton;

        private readonly List<StatUpgradeBar> _bars = new();
        private readonly List<ShipSelectBar> _ships = new();

        private StatsSheet _statsSheet;
        private ShipConfigSheet _shipsSheet;
        private IUpgradableStats _stats;
        private IPlayerStorage _playerStorage;
        private IAudioService _audioService;
        private SoundID _upgradeSound;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (StatUpgradeBar bar in _bars)
            {
                bar.StatUpgraded -= OnStatUpgraded;
            }

            foreach (ShipSelectBar bar in _ships)
            {
                bar.ShipSelected -= OnShipSelected;
            }
        }

        public void Open()
        {
            Show();

            foreach (StatUpgradeBar bar in _bars)
            {
                bar.Fill();
            }

            foreach (ShipSelectBar bar in _ships)
            {
                bar.Fill();
            }
        }

        [Inject]
        private void Construct(
            StatsSheet statsSheet,
            ShipConfigSheet shipConfigSheet,
            IUpgradableStats stats,
            IPlayerStorage playerStorage,
            IAudioService audioService,
            UiConfigs config)
        {
            _statsSheet = statsSheet;
            _shipsSheet = shipConfigSheet;
            _stats = stats;
            _playerStorage = playerStorage;
            _audioService = audioService;
            _upgradeSound = config.UpgradeSound;
            CreateUpgradeBars();
            CreateShipsBars();
        }

        private void CreateUpgradeBars()
        {
            foreach (StatConfig statConfig in _statsSheet.Stats)
            {
                if (statConfig.IsUpgradeable)
                {
                    StatUpgradeBar upgradeBar = Instantiate(_barPrefab, _barHolder);
                    upgradeBar.Initialize(statConfig, _stats, _playerStorage);
                    upgradeBar.StatUpgraded += OnStatUpgraded;

                    _bars.Add(upgradeBar);
                }
            }
        }

        private void CreateShipsBars()
        {
            foreach (ShipConfig shipConfig in _shipsSheet.Ships)
            {
                ShipSelectBar selectBar = Instantiate(_shipSelectBarPrefab, _shipHolder);
                selectBar.Initialize(shipConfig, _playerStorage, _shipName, _selectButton, _buyButton);
                selectBar.ShipSelected += OnShipSelected;
                
                _ships.Add(selectBar);
            }
        }

        private void OnStatUpgraded()
        {
            _audioService.PlaySound(_upgradeSound);

            foreach (StatUpgradeBar bar in _bars)
            {
                bar.CheckUpgradePrice();
            }
        }

        private void OnShipSelected()
        {
            foreach (ShipSelectBar bar in _ships)
            {
                bar.HideGlow();
            }
        }
    }
}