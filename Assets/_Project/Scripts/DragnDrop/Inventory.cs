using Player.InputReader;
using Props;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryView _view;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private List<DraggableItem> _items;
        [SerializeField] private ItemsDictionaryConfig _itemsDictionaryConfig;

        private PropsBase _currentItem;

        public InventoryView View => _view;

        public bool FirstItemSpawned { get; private set; }

        private void OnEnable() =>
            SubcribeItems();

        private void OnDisable() =>
            UnsubcribeItems();

        private void SubcribeItems()
        {
            foreach (var item in _items)
                item.DragBegan += InstantiateItem;

            foreach (var item in _items)
                item.Dragging += MoveItem;

            foreach (var item in _items)
                item.EnteredSpawnZone += ChangeItemView;

            foreach (var item in _items)
                item.DragFinished += CheckSpawn;
        }

        private void UnsubcribeItems()
        {
            foreach (var item in _items)
                item.DragBegan -= InstantiateItem;

            foreach (var item in _items)
                item.Dragging -= MoveItem;

            foreach (var item in _items)
                item.EnteredSpawnZone -= ChangeItemView;

            foreach (var item in _items)
                item.DragFinished -= CheckSpawn;
        }

        private void InstantiateItem(ItemType type)
        {
            _currentItem = Instantiate(GetItem(type), null);
            _currentItem.transform.position = Vector3.one * 500;
        }

        private PropsBase GetItem(ItemType type) =>
            _itemsDictionaryConfig.Dictionary[type];

        private void MoveItem(Vector3 position)
        {
            _currentItem.transform.position = position;

            if (_playerInput.IsInDrivingMode)
                _playerInput.SwitchInput(false);
        }

        private void ChangeItemView(bool canSpawn)
        {
            if (canSpawn)
                _currentItem.View.ChangeColorToGreen();
            else
                _currentItem.View.ChangeColorToRed();
        }

        private void CheckSpawn(bool canSpawn)
        {
            if (canSpawn)
            {
                _currentItem.EnableObject();
                FirstItemSpawned = true;
            }
            else
                Destroy(_currentItem.gameObject);

            _playerInput.SwitchInput(true);
        }
    }
}
