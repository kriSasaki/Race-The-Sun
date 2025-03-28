using System;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Project.Configs.Game;
using Project.Players.Logic;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Systems.Cameras
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private bool _isFreeLook;
        [SerializeField] private bool _isOpeningShows = true;
        [SerializeField] private float _openingViewDuration = 2f;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _targetCamera;
        [SerializeField] private CinemachineVirtualCamera _openingCamera;
        [SerializeField] private CinemachineFreeLook _freeLookCamera;
        [SerializeField] private CinemachineFreeLook _upgradeCamera;

        private List<CinemachineVirtualCameraBase> _cameras;
        private CinemachineBrain _brain;
        private Player _player;
        private CinemachineTransposer _targetCameraTransposer;
        private UiCanvas _uiCanvas;
        private Vector3 _followOffset;

        public async UniTask ShowOpeningAsync(CancellationToken cts)
        {
            if (_isOpeningShows == false)
            {
                await UniTask.NextFrame(cancellationToken: cts);

                return;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_openingViewDuration), cancellationToken: cts);

            GoToPlayer();

            await UniTask.WaitUntil(() => _brain.IsBlending == false, cancellationToken: cts);
        }

        public void GoToTarget(Transform target, CameraFollowOffset cameraFollowOffset = null)
        {
            if (cameraFollowOffset == null)
            {
                _targetCameraTransposer.m_FollowOffset = _followOffset;
            }
            else
            {
                _targetCameraTransposer.m_FollowOffset = cameraFollowOffset.Value;
            }

            SetTargetCamera(target);
            EnableCamera(_targetCamera);
        }

        public void GoToPlayer()
        {
            EnableCamera(_playerCamera);
        }

        public void GoToUpgrades()
        {
            EnableCamera(_upgradeCamera);
        }

        public void GoToFreeLook()
        {
            EnableCamera(_freeLookCamera);
        }

        public async UniTaskVoid ShowTargetAsync(Transform target, float duration)
        {
            _uiCanvas.Disable();
            GoToTarget(target);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: destroyCancellationToken);
            GoToPlayer();
            await UniTask.WaitUntil(() => _brain.IsBlending == false, cancellationToken: destroyCancellationToken);
            _uiCanvas.Enable();
        }

        [Inject]
        private void Construct(Player player, CinemachineBrain brain, UiCanvas uiCanvas)
        {
            _player = player;
            _brain = brain;
            _uiCanvas = uiCanvas;

            _cameras = new List<CinemachineVirtualCameraBase>()
            {
                _playerCamera,
                _targetCamera,
                _openingCamera,
                _freeLookCamera,
                _upgradeCamera
            };

            _targetCameraTransposer = _targetCamera.GetCinemachineComponent<CinemachineTransposer>();
            _followOffset = _targetCameraTransposer.m_FollowOffset;

            SetPlayerCamera();

            if (_isFreeLook != true)
            {
                if (_isOpeningShows)
                {
                    EnableCamera(_openingCamera);
                }
                else
                {
                    GoToPlayer();
                }
            }
        }

        private void SetPlayerCamera()
        {
            _playerCamera.Follow = _player.transform;
            _playerCamera.LookAt = _player.transform;
        }

        private void SetTargetCamera(Transform target)
        {
            _targetCamera.Follow = target;
            _targetCamera.LookAt = target;
        }

        private void EnableCamera(CinemachineVirtualCameraBase camera)
        {
            foreach (CinemachineVirtualCameraBase virtualCamera in _cameras)
            {
                virtualCamera.gameObject.SetActive(virtualCamera == camera);
            }
        }
    }
}