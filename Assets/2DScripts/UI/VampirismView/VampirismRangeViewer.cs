using UnityEngine;
using UnityEngine.UI;

namespace Assets._2DScripts.UI.VampirismView
{
    internal class VampirismRangeViewer : MonoBehaviour
    {
        [SerializeField] private VampirismAbility _vampirismAbility;
        [SerializeField] private Image _abilityEffect;

        private void Awake()
        {
            _abilityEffect.rectTransform.sizeDelta = _vampirismAbility.AbilityRange;
        }

        private void OnEnable()
        {
            _vampirismAbility.UsingAbility += UpdateView;
        }

        private void OnDisable()
        {
            _vampirismAbility.UsingAbility -= UpdateView;
        }

        private void UpdateView(bool isUsing) =>
            _abilityEffect.gameObject.SetActive(isUsing);
    }
}