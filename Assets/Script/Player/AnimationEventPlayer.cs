// Script by : Nanatchy
// Porject : Metroid Like

using Script.My_Tool_Script;
using UnityEngine;

namespace Script.Player
{
	public class AnimationEventPlayer : MonoBehaviour
	{
		#region Attributs

		[SerializeField] private PlayerController _playerController;

		#endregion

		#region Methods

		private void AnimationEventCrunch()
		{
			_playerController.AnimEventWeaponCrunch();
		}

		#endregion
	}
}
