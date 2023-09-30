using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Controls the play and stop of a clip associated to a AudioSource
    /// </summary>
    public class AudioControl
    {
        private AudioSource m_audioSource;

        public AudioControl(AudioSource audioSource)
        {
            m_audioSource = audioSource;
        }

        public void Play(AudioClip clip, bool oneShot = true)
        {
            if (oneShot)
            {
                m_audioSource.PlayOneShot(clip);
            }
            else
            {
                if (!m_audioSource.isPlaying)
                {
                    m_audioSource.clip = clip;
                    m_audioSource.Play();
                }
            }
        }

        public void Stop() => m_audioSource.Stop();
    }
}
