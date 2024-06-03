using UnityEngine;

namespace CKB
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private float openDelay;
        public float OpenDelay => openDelay;
    }
}
