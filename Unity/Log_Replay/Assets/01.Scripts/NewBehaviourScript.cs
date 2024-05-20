using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public enum ClientStatus
    {
        Inactive,
        Activating,
        Active,
        Deactivating
    }
    public string sub_to_ip = "127.0.0.1";
    public string sub_to_port = "11012";
    private Listener _listener;
    private ClientStatus _clientStatus = ClientStatus.Inactive;

    private void Start()
    {
        _listener = new Listener(sub_to_ip, sub_to_port, HandleMessage);
        _listener.Start();
        _clientStatus = ClientStatus.Active;
        Debug.Log("Client started!");
    }

    private void Update()
    {
        if (_clientStatus == ClientStatus.Active)
            _listener.DigestMessage();
    }

    private void OnDestroy()
    {
        if (_clientStatus != ClientStatus.Inactive)
            OnStopClient();
    }

    private void HandleMessage(string message)
    {
        Debug.Log(message);
    }

    private void OnStopClient()
    {
        Debug.Log("Stopping client...");
        _clientStatus = ClientStatus.Deactivating;
        _listener.Stop();
        Debug.Log("Client stopped!");
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////
    public class Listener
    {
        private Thread _clientThread;
        private readonly string _host;
        private readonly string _port;
        private readonly Action<string> _messageCallback;
        private bool _clientCancelled;
        private readonly ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();

        public Listener(string host, string port, Action<string> messageCallback)
        {
            _host = host;
            _port = port;
            _messageCallback = messageCallback;
        }

        public void Start()
        {
            _clientCancelled = false;
            _clientThread = new Thread(ListenerWork);
            _clientThread.Start();
        }

        public void Stop()
        {
            _clientCancelled = true;
            _clientThread?.Join();
            _clientThread = null;
        }

        private void ListenerWork()
        {
            AsyncIO.ForceDotNet.Force();
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect($"tcp://{_host}:{_port}");
                subSocket.SubscribeToAnyTopic();
                while (!_clientCancelled)
                {
                    if (!subSocket.TryReceiveFrameString(out var message)) continue;
                    _messageQueue.Enqueue(message);
                }
                subSocket.Close();
            }
            NetMQConfig.Cleanup();
        }

        public void DigestMessage()
        {
            while (!_messageQueue.IsEmpty)
            {
                if (_messageQueue.TryDequeue(out var message))
                    _messageCallback(message);
                else
                    break;
            }
        }
    }
}
