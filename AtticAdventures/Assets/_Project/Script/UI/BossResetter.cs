using Opsive.UltimateCharacterController.Traits;
using UnityEngine;

namespace AtticAdventures
{
    public class BossResetter : MonoBehaviour
    {
        public AttributeManager tank;
        public AttributeManager boss;

        public CanvasGroup canvasGroupsTank;
        public CanvasGroup canvasGroupsBoss;

        private bool tankAlive = true;
        private bool bossAlive = true;

        public void ResetBosses()
        {
            if (tankAlive)
            {
                Attribute tankHealth = tank.GetAttribute("Health");
                tankHealth.Value = tankHealth.MaxValue;

                canvasGroupsTank.alpha = 0f;
            }

            if (bossAlive)
            {
                Attribute bossHealth = boss.GetAttribute("Health");
                bossHealth.Value = bossHealth.MaxValue;

                canvasGroupsBoss.alpha = 0f;
            }

        }

        public bool IsTankAlive()
        {
            return tankAlive;
        }

        public bool IsBossAlive()
        {
            return bossAlive;
        }

        public void TankIsDead()
        {
            tankAlive = false;
        }

        public void BossIsDead()
        {
            bossAlive = false;
        }
    }
}
