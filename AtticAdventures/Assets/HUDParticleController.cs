using DG.Tweening;
using Opsive.UltimateCharacterController.Traits;
using Opsive.UltimateCharacterController.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtticAdventures
{
    public class HUDParticleController : MonoBehaviour
    {
        private CharacterAttributeManager characterAttributeManager;
        private float _health;
        private float _maxHealth;
        public ParticleSystem _healthParticleSystem;
        public ParticleSystem _stamParticleSystem;
        private ParticleSystem.MainModule _healthMainModule;
        private ParticleSystem.MainModule _stamMainModule;
        private ParticleSystem.ShapeModule _stamShapeModule;
        private Vector3 _currentScale;
        const float HALF = 0.5f;
        const float PERC_20 = 0.2f;
        const float DEAD = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            if(_healthParticleSystem == null ||  _stamParticleSystem == null)
            {
                Debug.Log("PS NULL");
            }
            characterAttributeManager = GetComponentInParent<CharacterAttributeManager>();
            _healthMainModule = _healthParticleSystem.main;
            _stamMainModule = _stamParticleSystem.main;
            _stamShapeModule = _stamParticleSystem.shape;
            _currentScale = _stamShapeModule.scale;
        }

        // Update is called once per frame
        void Update()
        {
            _maxHealth = characterAttributeManager.GetAttribute("Health").MaxValue;
            _health = characterAttributeManager.GetAttribute("Health").Value;
            var currentHealth = _health / _maxHealth;

            if (currentHealth >= HALF)
            {
                _healthMainModule.startLifetime = 1f;
                _healthMainModule.startColor = Color.green;
            }
            if (currentHealth <= HALF )
            {
                _healthMainModule.startLifetime = 0.5f;
                _healthMainModule.startColor = Color.yellow;
            }
            if (currentHealth <= PERC_20)
            {
                _healthMainModule.startLifetime = 0.2f;
                _healthMainModule.startColor = Color.red;
            }
            if(currentHealth <= DEAD)
            {
                _healthMainModule.loop = false;
            }

            var stam = characterAttributeManager.GetAttribute("Stamina").Value;
            var maxStam = characterAttributeManager.GetAttribute("Stamina").MaxValue;
            _stamMainModule.startLifetime = stam / maxStam;
        }
    }
}
