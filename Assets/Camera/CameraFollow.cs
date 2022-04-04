using UnityEngine;

namespace Assets.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(0, 0, -15);
        [Range(1, 10)] [SerializeField] private float _smoothFactor = 6;

        void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            Vector3 targetPosition = _target.position + _offset;
            Vector3 smoothPosition =
                Vector3.Lerp(transform.position, targetPosition, _smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}