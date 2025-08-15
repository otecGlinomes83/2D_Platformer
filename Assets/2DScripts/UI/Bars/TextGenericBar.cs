using TMPro;
using UnityEngine;

namespace Assets._2DScripts.UI.Bars
{
    public class TextGenericBar<T> : GenericBarBase<T> where T : IChangeObservable
    {
        [SerializeField] private TMP_Text _textMeshPro;

        protected override void UpdateView(float current, float max) =>
             _textMeshPro.text = $"[{current}/{max}]";
    }
}