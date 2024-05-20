using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.WebRTC;


public class WebRTCManager
{
    [System.Serializable]
    private class SignalingMessage
    {
        public string id;
        public RTCSdpType type;
        public string sdp;

    }

    [System.Serializable]
    public class TostringMessage
    {
        public string id;
        public string type;
        public string sdp;
    }

    // Inverse_define
    //private string ID = "answerer01";
    //private string SIGNALING_SERVER_URL = "http://192.168.50.88:19612/signaling";

    private string[] STUNSERVER = { "stun:stun.l.google.com:19302" };
    //
    //public UnityWebRequest webRequest;
    //public UnityWebRequest postRequest;
    public RTCDataChannel channel;
    public RTCDataChannel receiveChannel;

    //public RTCSessionDescription a_desc;

    public RTCPeerConnection peerConnection;
    private RTCConfiguration config;
    //public byte[] byte_;
    //public string loading_t;

    private void On_Update()
    {

        //WebRTC.Initialize();
        config = new RTCConfiguration();
        RTCIceServer[] iceServers = { new RTCIceServer { urls = STUNSERVER } };
        config.iceServers = iceServers;

        peerConnection = new RTCPeerConnection(ref config);

        RTCDataChannelInit channelConfig = new RTCDataChannelInit();

        channel = peerConnection.CreateDataChannel("video", channelConfig);
        channel.OnOpen += () =>
        {
            Debug.Log("Data channel Open!");
        };
        channel.OnClose += () =>
        {
            Debug.Log("Data channel closed!");
        };

        channel.OnMessage += (data) =>
        {
            Debug.Log("Data channel received message: " + data);
        };

        //StartCoroutine(GetResquest(SIGNALING_SERVER_URL));
        //StartCoroutine(Peer_open());

    }


}
