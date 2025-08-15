using UnityEngine;
using UnityEngine.UI;

namespace Assets._2DScripts.UI.Bars
{
    public class SimpleGenericBar<T> : GenericBarBase<T> where T : IChangeObservable
    {
        [SerializeField] private Image _bar;

        protected override void UpdateView(float current, float max)
        {
            _bar.fillAmount = current / max;
        }
    }
}