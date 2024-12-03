using NaughtyAttributes;
using Project.Interactables;
using Project.Players.Logic;
using Project.Systems.Stats;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Utils
{
    public class PlayerDebugCheats : MonoBehaviour
    {
        [SerializeField] private bool _setStatsOnStart = false;
        [HorizontalLine(3f, EColor.Orange)]
        [SerializeField, Range(1, 100)] private int _chargeLevel;
        [SerializeField, Range(1, 45)] private int _speedLevel;
        [SerializeField, Range(1, 100)] private int _turnSpeed;
        [SerializeField, Range(1, 50)] private int _floatTime;
        [SerializeField, Range(1, 10)] private int _pickUpRange;
        [SerializeField, Range(1, 10)] private int _xpMultiplier;

        private PlayerStats _playerStats;
        private Player _player;
        // private PirateBay _pirateBay;
        private UiCanvas _uiCanvas;

        private void Start()
        {
            if (_setStatsOnStart)
            {
                UpdateStats();
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                PlayerPrefsReseter.ResetPlayerPrefs();
            }
        }

        [Inject]
        private void Construct(
            PlayerStats playerStats,
            Player player,
            // PirateBay pirateBay,
            UiCanvas uiCanvas)
        {
            _playerStats = playerStats;
            _player = player;
            // _pirateBay = pirateBay;
            _uiCanvas = uiCanvas;
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void UpdateStats()
        {
            _playerStats.SetStatValue(StatType.Battery, _chargeLevel);
            _playerStats.SetStatValue(StatType.Speed, _speedLevel);
            _playerStats.SetStatValue(StatType.TurnSpeed, _turnSpeed);
            _playerStats.SetStatValue(StatType.FloatTime, _floatTime);
            _playerStats.SetStatValue(StatType.PickUpRange, _pickUpRange);
            _playerStats.SetStatValue(StatType.XPMultiplier, _xpMultiplier);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void MoveToPirateBay()
        {
            // _player.SetPosition(_pirateBay.PlayerRessurectPoint.position);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void ToggleUi()
        {
            if (_uiCanvas.IsEnable)
                _uiCanvas.Disable();
            else
                _uiCanvas.Enable();
        }
    }
}