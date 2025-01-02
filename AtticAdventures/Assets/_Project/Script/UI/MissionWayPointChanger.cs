using UnityEngine;

namespace AtticAdventures
{
    public class MissionWayPointChanger : MonoBehaviour
    {
        private MissionWayPoint missionWayPoint;

        private void Start()
        {
            missionWayPoint = FindAnyObjectByType<MissionWayPoint>();
        }

        public void ChangeMissionWayPointByIndex(int value)
        {
            if (missionWayPoint == null) 
            {
                missionWayPoint = FindAnyObjectByType<MissionWayPoint>();
            }

            missionWayPoint.SetTargetByIndex(value);
        }
    }
}
