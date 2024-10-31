using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtticAdventures
{
    public class MissionWayPoint : MonoBehaviour
    {
        [SerializeField] Image img;
        public TextMeshProUGUI text;

        public Transform target;
        public Vector3 offSet;

        [SerializeField] bool isActive = false;

        private void Start()
        {
            if (target == null)
            {
                target = FindAnyObjectByType<MissionTarget>().transform;
                isActive = true;
            }
        }

        private void Update()
        {
            if (!isActive) return;

            float minX = img.GetPixelAdjustedRect().width;
            float maxX = Screen.width -minX;

            float minY = img.GetPixelAdjustedRect().height;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offSet);

            if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.position = pos;
            text.text = ((int)Vector3.Distance(target.position, transform.position)).ToString();
        }

        public void SetActive(bool value)
        {
            isActive = value;
        }
    }
}
