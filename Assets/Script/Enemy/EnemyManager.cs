// Script by : Nanatchy
// Porject : Metroid Like

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Attributs
        
        private List<MoveEnemy> _enemyList = new List<MoveEnemy>();
        public int MaxNumbEnemy { get; private set; }
        public int NumbEnemyRemain { get; private set; }

        #endregion

        #region Methods

        public void EndGame()
        {
            NumbEnemyRemain = _enemyList.Count;
        }
        
        #endregion

        #region Behaviors

        private void Start()
        {
            _enemyList.Clear();
            
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                var script = child.GetComponent<MoveEnemy>();
                if (script != null)
                {
                    _enemyList.Add(script);
                }
            }

            MaxNumbEnemy = _enemyList.Count;
        }

        private void Update()
        {
            foreach (var enemy in _enemyList.ToList().Where(enemy => enemy.IsDead))
            {
                _enemyList.Remove(enemy);
            }
        }

        #endregion
    }
}
