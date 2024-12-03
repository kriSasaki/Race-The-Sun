using Project.Configs.GameResources;
using Project.Interfaces.Storage;
using Project.Utils.Tweens;
using Project.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI.GameResources
{
    public class ResourceBar : MonoBehaviour
    {
        [SerializeField] private GameResource _currency;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private ScaleTween _scaleTween;

        private IStorageNotifier _storage;

        private void OnDestroy()
        {
            _storage.ResourceAmountChanged -= OnCurrencyAmountChanged;
        }

        [Inject]
        public void Construct(IStorageNotifier storage)
        {
            _storage = storage;
            _icon.sprite = _currency.Sprite;

            int amount = _storage.GetResourceAmount(_currency);
            ChangeResourceAmount(amount);

            _storage.ResourceAmountChanged += OnCurrencyAmountChanged;
            _scaleTween.Initialize(_amount.transform);
        }

        private void OnCurrencyAmountChanged(GameResource resource, int amount)
        {
            if (resource != _currency)
                return;

            ChangeResourceAmount(amount);
            _scaleTween.RunFrom();
        }

        private void ChangeResourceAmount(int amount)
        {
            _amount.text = amount.ToNumericalString();
        }
    }
}