using MagicLeap;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

namespace ml_promter
{
    /// <summary>
    /// リモート側のステータスメニューUI.
    /// </summary>
    public class RemoteStatus : MonoBehaviour
    {
        [SerializeField]
        private MLWebRTCVideoSinkBehavior remoteVideoSinkBehavior;

        [SerializeField]
        private MLWebRTCAudioSinkBehavior remoteAudioSinkBehavior;

        [SerializeField]
        private Text remoteStatusText;
        
        [SerializeField] 
        private Toggle toggleRemoteVideo;
        
        [SerializeField] 
        private Toggle toggleRemoteAudio;

        [SerializeField]
        private Slider audioCacheSizeSlider;

        [SerializeField]
        private Text audioCacheSliderValue;
        
        private MLWebRTC.MediaStream remoteMediaStream = null;


        private void Start()
        {
            audioCacheSizeSlider.onValueChanged.AddListener(OnAudioCacheSizeSliderValueChanged);
            toggleRemoteVideo.onValueChanged.AddListener(ToggleRemoteVideo);
            toggleRemoteAudio.onValueChanged.AddListener(ToggleRemoteAudio);
        }
        
        
        private void OnAudioCacheSizeSliderValueChanged(float value)
        {
            if (audioCacheSliderValue != null)
            {
                audioCacheSliderValue.text = $"{value} ms";
            }

            if (remoteAudioSinkBehavior.AudioSink != null)
            {
                remoteAudioSinkBehavior.AudioSink.SetCacheSize((uint)value);
            }
        }
        
        
        private void ToggleRemoteAudio(bool on)
        {
#if PLATFORM_LUMIN
            remoteAudioSinkBehavior.AudioSink.Stream.ActiveAudioTrack.SetEnabled(on);
#endif
        }

        
        private void ToggleRemoteVideo(bool on)
        {
#if PLATFORM_LUMIN
            remoteVideoSinkBehavior.VideoSink.Stream.ActiveVideoTrack.SetEnabled(on);
#endif
        }



        public void AddConnectionTrack(List<MLWebRTC.MediaStream> mediaStream, MLWebRTC.MediaStream.Track addedTrack)
        {
           SetStatusText($"Adding {addedTrack.TrackType} track.");
            
            if (remoteMediaStream == null) remoteMediaStream = mediaStream[0];

            switch (addedTrack.TrackType)
            {
                // if the incoming track is audio, set the audio sink to this track.
                case MLWebRTC.MediaStream.Track.Type.Audio:
                {
                    remoteAudioSinkBehavior.AudioSink.SetStream(remoteMediaStream);
                    remoteAudioSinkBehavior.gameObject.SetActive(true);
                    remoteAudioSinkBehavior.AudioSink.SetCacheSize((uint)audioCacheSizeSlider.value);
                    break;
                }

                // if the incoming track is video, set the video sink to this track.
                case MLWebRTC.MediaStream.Track.Type.Video:
                {
                    remoteVideoSinkBehavior.VideoSink.SetStream(remoteMediaStream);
                    remoteVideoSinkBehavior.ResetFrameFit();
                    remoteVideoSinkBehavior.gameObject.SetActive(true);
                    break;
                }
            }
        }


        public void RemoveConnectionTrack(List<MLWebRTC.MediaStream> mediaStream, MLWebRTC.MediaStream.Track removedTrack)
        {
            SetStatusText($"Removed {removedTrack.TrackType} track.");
            
            switch (removedTrack.TrackType)
            {
                case MLWebRTC.MediaStream.Track.Type.Audio:
                {
                    remoteAudioSinkBehavior.AudioSink.SetStream(null);
                    remoteAudioSinkBehavior.gameObject.SetActive(false);
                    break;
                }

                case MLWebRTC.MediaStream.Track.Type.Video:
                {
                    remoteVideoSinkBehavior.VideoSink.SetStream(null);
                    remoteVideoSinkBehavior.gameObject.SetActive(false);
                    break;
                }
            }
        }

        
        public void DestroyMediaStream() => remoteMediaStream = null;
        
        public void SetStatusText(string value) => remoteStatusText.text = value;


        public void ClearStatusText() => remoteStatusText.text = "";


        public void DisableSinkBehavior()
        {
            remoteVideoSinkBehavior.VideoSink.SetStream(null);
            remoteAudioSinkBehavior.gameObject.SetActive(false);
            remoteAudioSinkBehavior.gameObject.SetActive(false);
            remoteVideoSinkBehavior.gameObject.SetActive(false);
        }



    }
}