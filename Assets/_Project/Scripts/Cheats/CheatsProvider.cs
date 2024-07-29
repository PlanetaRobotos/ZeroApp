using _Project.Core;
using QFSW.QC;
using UnityEngine;

namespace _Project.Cheats
{
    public class CheatsProvider : MonoBehaviour
    {
        [Inject] private IGameTracker _gameTracker { get; }

        #region Game Tracker

        [Command("record-win", "Records a win")]
        private void RecordWin()
        {
            Debug.Log("Win recorded");

            _gameTracker.RecordWin();
        }

        [Command("reset-wins", "Resets wins amount")]
        private void ResetWinsAmount()
        {
            Debug.Log("Wins amount reset");

            _gameTracker.ResetWinsAmount();
        }

        #endregion
    }
}