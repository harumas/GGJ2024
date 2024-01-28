using UnityEngine;
using DG.Tweening;
using Utility;
using UnityEngine.InputSystem;
using System;
using UI;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float sensibilityX;
        [SerializeField] private float sensibilityY;

        [SerializeField] private Vector2 verticalClamp;
        [SerializeField] private Vector2 horizontalClamp;
        [SerializeField] private float defaultFOV, focusFOV;
        private PlayerInput playerInput;

        private float currentX, currentY;
        private bool cursorLock = true;
        private Camera mainCamera;
        private Vector3 startPos;

        private void Awake()
        {
            Locator.Register(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
            playerInput = GetComponent<PlayerInput>();

            if (Camera.main != null)
            {
                mainCamera = Camera.main;

                Vector3 rotation = mainCamera.transform.localRotation.eulerAngles;
                currentX = rotation.x;
                currentY = rotation.y;
            }
            else
            {
                Debug.LogError("カメラがありません");
            }

            SetPCSeeing(true);
        }

        private void Update()
        {
            if (Locator.Resolve<PauseScript>().isPause) return;
            if (Locator.Resolve<GameEvent>().IsCameraLock) return;

            Look();
            UpdateCursorLock();

            SetFocus();
            SetPCSeeing();

            if (Input.GetMouseButtonDown(0))
            {
                BackFromPC();
            }
        }

        private Vector2 oldMousePosition;

        private void Look()
        {
            if (isPcSeeing) return;
            var look = playerInput.currentActionMap["Look"].ReadValue<Vector2>();
            currentY += look.x * sensibilityX;
            currentX -= look.y * sensibilityY;

            currentX = Mathf.Clamp(currentX, horizontalClamp.x, horizontalClamp.y);
            currentY = Mathf.Clamp(currentY, verticalClamp.x, verticalClamp.y);

            mainCamera.transform.localRotation = Quaternion.Euler(currentX, currentY, 0);

            oldMousePosition = Input.mousePosition;
        }

        private void UpdateCursorLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLock = !cursorLock;
            }
            else if (Input.GetMouseButton(0))
            {
                cursorLock = true;
            }

            Cursor.lockState = cursorLock ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public bool isFocus { get; private set; }
        public bool isPcSeeing { get; private set; }
        private Tweener tweener;

        private void SetFocus()
        {
            if (isPcSeeing) return;
            isFocus = Input.GetMouseButton(1);
            tweener = DOTween.To(() => mainCamera.fieldOfView, (v) => mainCamera.fieldOfView = v, isFocus ? focusFOV : defaultFOV, 0.2f);
        }

        public void MovePCPoint(Transform point)
        {
            ResetLookValue();
            transform.position = point.position;
            transform.localRotation = Quaternion.identity;
            tweener.Kill();
            mainCamera.fieldOfView = 40;

            Locator.Resolve<TypingSystem>().OnCameraFocused();
        }

        private void SetPCSeeing(bool forceLook = false)
        {
            var pc = Locator.Resolve<RayController>().IsHitPC().collider;
            if (pc == null) return;

            if (pc.CompareTag("PC") && (forceLook || Input.GetMouseButtonDown(1)))
            {
                isPcSeeing = true;
                Locator.Resolve<DepthController>().SetDepth(true);
                MovePCPoint(pc.transform.parent.Find("Point"));
            }
        }

        public void BackFromPC()
        {
            if (!isPcSeeing) return;

            transform.position = startPos;
            ResetLookValue();
            isPcSeeing = false;
            isFocus = false;
            Locator.Resolve<DepthController>().SetDepth(false);
            Locator.Resolve<TypingSystem>().OnCameraUnfocused();
        }

        private void ResetLookValue()
        {
            currentX = 0;
            currentY = 0;
            oldMousePosition = Input.mousePosition;
        }
    }
}