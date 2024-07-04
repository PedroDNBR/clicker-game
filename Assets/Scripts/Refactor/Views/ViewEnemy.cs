using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class ViewEnemy : MonoBehaviour
    {
        Enemy enemy;

        public Image healthBarFill;

        private void Start()
        {
            enemy = gameObject.GetComponent<Enemy>();
            enemy.onTookDamageAction += UpdateUI;
        }

        void UpdateUI()
        {
            healthBarFill.fillAmount = enemy.GetHealthPercentage01();
        }
    }
}
