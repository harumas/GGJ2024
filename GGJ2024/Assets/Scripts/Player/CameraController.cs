using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float sensibilityX;
        [SerializeField] private float sensibilityY;

        [SerializeField] private Vector2 verticalClamp;
        [SerializeField] private Vector2 horizontalClamp;

        private Transform camTransform;

        private float currentX, currentY;
        private bool cursorLock = true;

        // Start is called before the first frame update
        void Start()
        {
            if (Camera.main != null)
            {
                camTransform = Camera.main.transform;

                Vector3 rotation = camTransform.localRotation.eulerAngles;
                currentX = rotation.x;
                currentY = rotation.y;
            }
            else
            {
                Debug.LogError("カメラがありません");
            }
        }

        private void Update()
        {
            currentY += Input.GetAxis("Mouse X") * sensibilityX;
            currentX += -Input.GetAxis("Mouse Y") * sensibilityY;

            currentX = Mathf.Clamp(currentX, horizontalClamp.x, horizontalClamp.y);
            currentY = Mathf.Clamp(currentY, verticalClamp.x, verticalClamp.y);

            camTransform.localRotation = Quaternion.Euler(currentX, currentY, 0);

            UpdateCursorLock();
        }

        private void UpdateCursorLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLock = false;
            }
            else if (Input.GetMouseButton(0))
            {
                cursorLock = true;
            }

            Cursor.lockState = cursorLock ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}