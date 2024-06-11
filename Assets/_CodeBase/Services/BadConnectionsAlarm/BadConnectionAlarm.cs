using UnityEngine;

namespace _CodeBase.Services.BadConnectionsAlarm
{
    public class BadConnectionAlarm : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}