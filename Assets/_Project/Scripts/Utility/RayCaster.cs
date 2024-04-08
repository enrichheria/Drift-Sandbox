using UnityEngine;

namespace Utility
{
    public class RayCaster
    {
        private Ray _rayMousePosition;
        private Vector3 _hitPosition;
        private readonly Camera _camera;

        private const float RayLength = 100f;
        private const string DropSurface = "DropSurface";

        public Vector3 HitPosition => _hitPosition;

        public bool IsInSpawnZone => _hitPosition.x <= 30f && _hitPosition.x >= -30 
            && _hitPosition.z <= 30f && _hitPosition.z >= -30f;

        public RayCaster(Camera camera) =>
            _camera = camera;

        public void CastRayMousePoistion()
        {
            _rayMousePosition = _camera.ScreenPointToRay(Input.mousePosition);

            //Debug.DrawRay(_ray.origin, _ray.direction * RayLength, Color.red);

            if (Physics.Raycast(_rayMousePosition.origin, _rayMousePosition.direction * RayLength, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(DropSurface))
                {
                    _hitPosition = hit.point;
                }
            }
        }
    }
}
