using InventorySystem;
using Props;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Items Config", menuName = "New items config")]
    public class ItemsDictionaryConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<ItemType, PropsBase> _dictionary = new();

        public IReadOnlyDictionary<ItemType, PropsBase> Dictionary => _dictionary;
    }
}
