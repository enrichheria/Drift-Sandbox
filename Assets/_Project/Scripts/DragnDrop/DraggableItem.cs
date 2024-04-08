using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace InventorySystem
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private int _price;
        [SerializeField] private ItemType _itemType;

        private Camera _camera;
        private RayCaster _rayCaster;

        public event Action<ItemType> DragBegan;
        public event Action<Vector3> Dragging;
        public event Action<bool> EnteredSpawnZone;
        public event Action<bool> DragFinished;

        private void Awake()
        {
            _camera = Camera.main;
            _rayCaster = new RayCaster(_camera);
        }

        public void OnBeginDrag(PointerEventData eventData) => 
            DragBegan?.Invoke(_itemType);

        public void OnDrag(PointerEventData eventData)
        {
            _rayCaster.CastRayMousePoistion();
            Dragging?.Invoke(_rayCaster.HitPosition);
            EnteredSpawnZone?.Invoke(_rayCaster.IsInSpawnZone);
        }

        public void OnEndDrag(PointerEventData eventData) => 
            DragFinished?.Invoke(_rayCaster.IsInSpawnZone);
    }

    public enum ItemType
    {
        Barrel,
        Skibidi,
        FatBoy,
        Monster,
        Bench,
        Hydrant
    }
}
