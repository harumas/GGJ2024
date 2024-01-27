using TMPro;
using UnityEngine;

namespace UI
{
    public class ChatView : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        
        private Camera cam;
        private static readonly Vector3 OutSideCamera = new(1000000f, 1000000f, 0f);

        private void Awake()
        {
            cam = Camera.main;
            gameObject.SetActive(false);
        }

        public void Update()
        {
            var targetPos = target.position;

            //カメラ上のスクリーン座標を取得
            var screenPoint = cam.WorldToScreenPoint(targetPos + new Vector3(offset.x, offset.y, offset.z));

            //カメラの後ろにいるときは遠くに飛ばす
            if (screenPoint.z <= 0f)
            {
                transform.position = OutSideCamera;
                return;
            }

            transform.position = screenPoint;
        }

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}