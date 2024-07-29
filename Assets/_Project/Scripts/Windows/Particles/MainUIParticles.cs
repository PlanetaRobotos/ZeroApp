using System;
using _Project.Scripts.Infrastructure;
using UnityEngine;

namespace _Project.Windows.Particles
{
    public class MainUIParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles = Array.Empty<ParticleSystem>();

        [Inject] private UIMainController UIMainController { get; }

        private void OnEnable()
        {
            UIMainController.ParticlesObserver.AddListener(SwitchParticles);
        }

        private void OnDisable()
        {
            UIMainController.ParticlesObserver.RemoveListener(SwitchParticles);
        }

        private void SwitchParticles(bool isVisible)
        {
            for (var i = 0; i < particles.Length; i++)
                if (isVisible)
                    particles[i].Play(true);
                else
                    particles[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}