using UnityEngine;

namespace Project.Players.Logic
{
    public class MoveHandler
    {
        private const float RotationSmoothness = 2f;
        public float forwardSpeed = 10f;
        public float maxTurnSpeed = 5f;

        private float _screenCenter => Screen.width / 2f;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _targetRotationZ;
        private float _totalDirection;
        private float _turnStrength;
        private float _averageDirection;
        private int _inputCount;

        public void Initialize(Rigidbody rigidbody, Transform transform)
        {
            _rigidbody = rigidbody;
            _transform = transform;
        }

        public void ReadInput()
        {
            _totalDirection = 0;
            _inputCount = 0;
            _turnStrength = 0;

#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
            {
                _totalDirection += (Input.mousePosition.x - _screenCenter) / _screenCenter;
                _inputCount++;
            }

#else
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            _totalDirection += (touch.position.x - _screenCenter) / _screenCenter;
            _inputCount++;
        }
#endif

            _turnStrength = 0;

            if (_inputCount > 0)
            {
                _averageDirection = _totalDirection / _inputCount;
                _turnStrength = Mathf.Clamp(_averageDirection * maxTurnSpeed, -maxTurnSpeed, maxTurnSpeed);
            }
        }

        public void Rotate()
        {
            _targetRotationZ = -_turnStrength * 10f;
            float smoothRotationZ = Mathf.LerpAngle(_transform.eulerAngles.z, _targetRotationZ, Time.deltaTime * RotationSmoothness);
            Quaternion targetRotation = Quaternion.Euler(0, 0, smoothRotationZ);
            _rigidbody.MoveRotation(targetRotation);
        }

        public void Move()
        {
            Vector3 forwardMovement = Vector3.forward * forwardSpeed;
            Vector3 finalVelocity = forwardMovement + Vector3.right * _turnStrength;
            _rigidbody.velocity = finalVelocity;
        }
    }
}