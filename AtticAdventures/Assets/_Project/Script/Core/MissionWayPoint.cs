using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AtticAdventures
{
    public class MissionWayPoint : MonoBehaviour
    {
        [SerializeField] bool isActive = false;

        [Header("Marker")]
        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI distanceText;

        public Transform target;
        public Vector3 offSet;

        [SerializeField] float fadeOutDistance = 10f;
        [SerializeField] float fadeInDistance = 20f;

        [SerializeField] CanvasGroup canvasGroup;

        [Header("Quests")]
        public List<MissionTarget> missionTargets;
        [SerializeField] TextMeshProUGUI headerText;
        [SerializeField] TextMeshProUGUI objectiveText;

        private void Start()
        {
            missionTargets = FindObjectsOfType<MissionTarget>()
                             .OrderBy(m => m.index)
                             .ToList();

            if (missionTargets.Count > 0)
            {
                target = missionTargets[0].transform;
                isActive = true;

                UpdateQuestTexts(missionTargets[0]);
            }
        }

        private void Update()
        {
            if (!isActive || target == null) return;

            float distance = Vector3.Distance(target.position, transform.position);

            float alpha = Mathf.Clamp01((distance - fadeOutDistance) / (fadeInDistance - fadeOutDistance));
            canvasGroup.alpha = alpha;

            if (alpha > 0)
            {
                float minX = img.GetPixelAdjustedRect().width;
                float maxX = Screen.width - minX;

                float minY = img.GetPixelAdjustedRect().height;
                float maxY = Screen.height - minY;

                Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offSet);

                if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
                {
                    pos.x = pos.x < Screen.width / 2 ? maxX : minX;
                }

                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);

                img.transform.position = pos;
                distanceText.text = ((int)distance).ToString();
            }
        }

        public void SetActive(bool value)
        {
            isActive = value;
        }

        public void SetTargetByIndex(int index)
        {
            MissionTarget missionTarget = missionTargets.FirstOrDefault(m => m.index == index);
            if (missionTarget != null)
            {
                target = missionTarget.transform;

                UpdateQuestTexts(missionTarget);
            }
        }

        private void UpdateQuestTexts(MissionTarget missionTarget)
        {
            headerText.text = missionTarget.questHeader;
            objectiveText.text = missionTarget.questObjective;
        }
    }
}
