using System.Collections;
using System.Collections.Generic;
using Unity.WebRTC;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class WebRTCController : MonoBehaviour
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

    private string ID = "answerer01";
    private string SIGNALING_SERVER_URL = "http://192.168.50.88:19612/signaling";

    public UnityWebRequest webRequest;
    public UnityWebRequest postRequest;

    public RTCSessionDescription a_desc;

    public byte[] byte_;
    public string loading_t;



    private RTCPeerConnection peerConnection;// = GameManager.WebRTC.peerConnection;
    public RTCDataChannel channel; //= GameManager.WebRTC.channel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetResquest(SIGNALING_SERVER_URL));
        StartCoroutine(Peer_open());
    }


    IEnumerator Createanswer()
    {

        var op = peerConnection.CreateAnswer();
        yield return op;
        peer_local_connection_access(op.Desc);

    }

    IEnumerator PostResquest(RTCSessionDescription desc)
    {
        var message = new TostringMessage();
        message.id = ID;
        message.type = desc.type.ToString();
        message.sdp = desc.sdp;

        string json = JsonUtility.ToJson(message);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(SIGNALING_SERVER_URL + "/answer", json))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Offer SDP 수신 완료에 따른 Unity Answer SDP 전송.");
                // GameManager.Instance.peerConnection1 = peerConnection;
                // LoadScene(nextScene);
            }
        }
    }

    IEnumerator GetResquest(string uri)
    {
        webRequest = UnityWebRequest.Get(uri + "/get_offer");
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    loading_t = "WebRequest Success";
                    get_next_level();
                    break;
            }
        }

    }
    IEnumerator Peer_open()
    {

        while (true)
        {
            peerConnection.OnDataChannel = (channel_1) =>
            {

                channel = channel_1;

                channel.OnMessage = (byte[] bytes) =>
                {
                    loading_t = "Receive data from channel";
                    byte_ = bytes;
                };
            };


            yield return new WaitForSeconds(0.1f);
        }
    }


    public void get_next_level()
    {
        var data = JsonUtility.FromJson<SignalingMessage>(webRequest.downloadHandler.text);
        if (data.type == RTCSdpType.Offer)
        {
            RTCSessionDescription desc = new RTCSessionDescription();
            desc.sdp = data.sdp;
            desc.type = data.type;
            peer_connection_remote_acces(desc);
            StartCoroutine(Createanswer());
            loading_t = "local/remote peer_connection..";
        }
        else
        {
            Debug.Log("74error");

        }
    }

    async void peer_connection_remote_acces(RTCSessionDescription desc)
    {
        peerConnection.SetRemoteDescription(ref desc);
        await Task.Delay(500);

    }

    async void peer_local_connection_access(RTCSessionDescription desc)
    {
        peerConnection.SetLocalDescription(ref desc);
        await Task.Delay(500);
        StartCoroutine(PostResquest(desc));
    }
}
