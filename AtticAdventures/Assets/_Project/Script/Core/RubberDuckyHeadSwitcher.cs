using Rewired;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class RubberDuckyHeadSwitcher : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> objectsToActivate;

        [SerializeField]
        private List<GameObject> objectsToDeactivate;

        private int playerId = 0;

        private Player rewiredPlayer;

        private bool isDucked = false;

        void Awake()
        {
            rewiredPlayer = ReInput.players.GetPlayer(playerId);
        }
        void Update()
        {
            if (rewiredPlayer.GetButtonDown("Equip Fourth Item"))
            {
                ToggleObjects();
            }
        }

        private void ToggleObjects()
        {
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(!isDucked);
                }
            }

            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(isDucked);
                }
            }

            isDucked = !isDucked;
        }
    }
}
