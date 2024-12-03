using Lean.Localization;
using UnityEngine;

namespace Project.Interactables
{
    public class OptionalTipZone : TipZone
    {
        [SerializeField, LeanTranslationName] private string _mobileTipToken;

        protected override void ShowTip()
        {
            Typewriter.ShowText(GetTipMessage(_mobileTipToken));
        }
    }
}