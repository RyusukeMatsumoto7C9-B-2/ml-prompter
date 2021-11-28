using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;

using ml_prompter.Ml.SpeakerTools;


namespace ml_prompter.Network
{
    /// <summary>
    /// MagicLeap側のイベントリスナ.
    /// </summary>
    public class ClientEventListener : GlobalEventListener
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private SpeakerNote speakerNote;
        
        private List<string> screenCaptureStrings = new List<string>();
        private TextureCompressor textureCompressor = new TextureCompressor();


        public override void OnEvent(ScreenCaptureEvent ev)
        {
            screenCaptureStrings.Add(ev.CaptureString1);
            Debug.Log("Texture情報を取得中");
            if (ev.CaptureString1.Contains("-e"))
            {
                Debug.Log("Textureを生成するよ");
                // ここでスクリーンショット構築クラスを使って構築.
                var tex = textureCompressor.DecompressToTexture2D(screenCaptureStrings.ToArray());
                meshRenderer.material.mainTexture = tex;
                screenCaptureStrings.Clear();
            }
        }
        
    }
}