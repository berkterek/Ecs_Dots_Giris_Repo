using UnityEngine;

namespace SpaceShipEcsDots.Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;

        void OnValidate()
        {
            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}