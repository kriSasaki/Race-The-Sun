using Project.Players.Logic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI.Bars
{
    public class PlayerHealthBar : FadeableBar
    {
        [SerializeField] private TMP_Text _amountLabel;
        [SerializeField] private Color _hitColor = Color.white;
        [SerializeField] private float _hitDuration = 0.15f;

        private Player _player;

        private void OnDestroy()
        {
            _player.ChargeChanged -= OnHealthChanged;
        }

        [Inject]
        private void Construct(Player player)
        {
            _player = player;

            _player.ChargeChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _amountLabel.text = _player.CurrentCharge.ToString();

            Fill(_player.CurrentCharge, _player.MaxCharge);
            LerpColor(_hitColor, _hitDuration);

            if (_player.CurrentCharge == _player.MaxCharge)
                TryFade(() => _player.CurrentCharge == _player.MaxCharge);
        }
    }
}