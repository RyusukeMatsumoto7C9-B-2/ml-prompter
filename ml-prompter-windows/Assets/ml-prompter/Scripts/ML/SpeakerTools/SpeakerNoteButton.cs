using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ml_prompter.Ml.SpeakerTools
{
    /// <summary>
    /// スピーカーノート用UIボタン.
    /// </summary>
    public class SpeakerNoteButton : MonoBehaviour
    {
        private const string SpeakerControllerTag = "SpeakerController";
        
        private readonly UnityEvent onTriggerEnterEvent = new UnityEvent();
        

        private void OnDestroy()
        {
            onTriggerEnterEvent.RemoveAllListeners();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(SpeakerControllerTag)) return;
            onTriggerEnterEvent?.Invoke();
        }

        
        public void RegisterListener(UnityAction listener)
        {
            onTriggerEnterEvent.AddListener(listener);
        }
        

    }
}

