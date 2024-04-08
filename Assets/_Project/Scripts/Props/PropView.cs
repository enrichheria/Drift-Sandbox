using System.Collections.Generic;
using UnityEngine;

namespace Props
{
    public class PropView : MonoBehaviour
    {
        [SerializeField] private List<Material> _materials;
        [SerializeField] private Color _redColor;
        [SerializeField] private Color _greenColor;

        public void ChangeColorToGreen() =>
           ChangeColor(_greenColor);

        public void ChangeColorToRed() =>
            ChangeColor(_redColor);

        private void ChangeColor(Color color)
        {
            foreach (var material in _materials)
                material.color = color;
        }
    }
}
