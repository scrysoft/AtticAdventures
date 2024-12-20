using Opsive.UltimateCharacterController.Traits;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class DamageHealZone : MonoBehaviour
    {

        GameObject player;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && name == "ww")
            {
                player = (GameObject)other.gameObject;
                StartCoroutine(DamageOverTime());
            }
            if (other.gameObject.tag == "Player" && name == "ww_1")
            {
                player = (GameObject)other.gameObject;
                StartCoroutine(HealOverTime());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StopAllCoroutines();
            }
        }



        private IEnumerator DamageOverTime()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(2);
                var attr = player.GetComponentInParent<CharacterAttributeManager>();
                var health = attr.GetAttribute("Health");
                health.Value -= 10;
            }
        }

        private IEnumerator HealOverTime()
        {
            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(2);
                var attr = player.GetComponentInParent<CharacterAttributeManager>();
                var health = attr.GetAttribute("Health");
                health.Value += 10;
            }
        }
    }
}
