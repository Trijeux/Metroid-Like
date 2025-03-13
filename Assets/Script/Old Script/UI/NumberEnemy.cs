// Script by : Nanatchy
// Porject : Metroid Like

using Script.Old_Script.Enemy;
using TMPro;
using UnityEngine;

namespace Script.Old_Script.UI
{
    public class NumberEnemy : MonoBehaviour
    {
        #region Attributs

        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region Methods

        #endregion

        #region Behaviors

        private void Start()
        {
            enemyManager.EndGame();
            textMeshProUGUI.text = enemyManager.MaxNumbEnemy - enemyManager.NumbEnemyRemain + " / " + enemyManager.MaxNumbEnemy;
        }

        private void Update()
        {

        }

        #endregion
    }
}
