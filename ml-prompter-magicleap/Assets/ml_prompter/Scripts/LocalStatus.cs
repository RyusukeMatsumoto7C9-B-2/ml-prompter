using MagicLeap;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;


namespace ml_promter
{
    /// <summary>
    /// Local側のステータスメニューUI.
    /// </summary>
    public class LocalStatus : MonoBehaviour
    {
        [SerializeField]
        private MLWebRTCVideoSinkBehavior videoSinkBehavior;
        
        [SerializeField]
        private Text localStatusText;

        [SerializeField]
        private Dropdown localVideoSourceDropdown;
        
        [SerializeField]
        private Toggle toggleLocalVideo;
        
        [SerializeField]
        private Toggle toggleLocalAudio;



        private MLWebRTC.MediaStream localMediaStream = null;
        private MLWebRTC.MediaStream.Track localVideoTrack = null;

        
        
        private void Start()
        {
            toggleLocalVideo.onValueChanged.AddListener(ToggleLocalVideo);
            toggleLocalAudio.onValueChanged.AddListener(ToggleLocalAudio);
        }
        
        
        public void CreateLocalMediaStream(MLWebRTC.PeerConnection connection)
        {
            SetActiveLocalVideoSinkBehavior(true);
            localStatusText.text = "";

            string id = "local";

            // Use factory methods to create a new media stream.
            if(localMediaStream == null)
            {
                switch (localVideoSourceDropdown.value)
                {
                    // MLCamera defined source.
                    case 0:
                    {
#if PLATFORM_LUMIN
                        localMediaStream = MLWebRTC.MediaStream.CreateWithAppDefinedVideoTrack(id, MLCameraVideoSource.CreateLocal(new MLCamera.CaptureSettings(), out MLResult result), MLWebRTC.MediaStream.Track.AudioType.Microphone);
#endif
                        break;
                    }

                    // MLMRCamera defined source.
                    case 1:
                    {
                        localMediaStream = MLWebRTC.MediaStream.CreateWithAppDefinedVideoTrack(id, MLMRCameraVideoSource.CreateLocal(MLMRCamera.InputContext.Create(), out MLResult result), MLWebRTC.MediaStream.Track.AudioType.Microphone);
                        break;
                    }
                }
            }
            else // Replace the local video track with another.
            {
                // Determine which local video source to use
                switch (localVideoSourceDropdown.value)
                {
                    // MLCamera defined source.
                    case 0:
                    {
                        if (!(localVideoTrack is MLCameraVideoSource))
                        {
#if PLATFORM_LUMIN
                            localVideoTrack = MLCameraVideoSource.CreateLocal(new MLCamera.CaptureSettings(), out MLResult result);
#endif
                        }
                        break;
                    }

                    // MLMRCamera defined source.
                    case 1:
                    {
                        if (!(localVideoTrack is MLMRCameraVideoSource))
                        {
                            localVideoTrack = MLMRCameraVideoSource.CreateLocal(MLMRCamera.InputContext.Create(), out MLResult result);
                        }
                        break;
                    }
                }

                localMediaStream.AddLocalTrack(localVideoTrack);
                localMediaStream.SelectTrack(localVideoTrack);
            }

            foreach (MLWebRTC.MediaStream.Track track in localMediaStream.Tracks)
            {
#if PLATFORM_LUMIN
                // TODO : in case we're recycling the connection / sources, the track might already have been added.
                connection.AddLocalTrack(track);
#endif
            }

            videoSinkBehavior.VideoSink.SetStream(localMediaStream);
            videoSinkBehavior.ResetFrameFit();

            if (localVideoSourceDropdown.value == 1 || localVideoSourceDropdown.value == 3)
            {
                // Turn off the local video track behavior when in MLMRCamera ("screen share") mode.
                SetActiveLocalVideoSinkBehavior(false);
                localStatusText.text = "Screen Sharing";
            }
        }
        
        
        private void ToggleLocalVideo(bool value)
        {
#if PLATFORM_LUMIN
            videoSinkBehavior.VideoSink.Stream.ActiveVideoTrack.SetEnabled(value);
#endif
        }

        
        private void ToggleLocalAudio(bool value)
        {
#if PLATFORM_LUMIN
            localMediaStream.ActiveAudioTrack.SetEnabled(value);
#endif
        }


        public void SetActiveLocalVideoSinkBehavior(bool value) => videoSinkBehavior.gameObject.SetActive(value);


        public void DestroyLocalMediaStream()
        {
            localMediaStream.DestroyLocal();
            localMediaStream = null;
        }


        public void ClearLocalStatusText() => localStatusText.text = "";
    }
}