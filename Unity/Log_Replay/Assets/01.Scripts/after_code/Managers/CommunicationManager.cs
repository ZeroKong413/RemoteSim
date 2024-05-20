using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Security.Cryptography;

// NetMQ package
//using NetMQ.Sockets;
//using NetMQ;

public class CommunicationManager 
{

    static public IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11013);
    public Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

    public byte[] received_message = null;

    public void Checksocket()
    {

        socket.Connect(endPoint);
        Console.WriteLine("Connect ...");

    }

    public void Send_data(string message)
    {
        socket.Send(Encoding.UTF8.GetBytes(message));
    }
    public IEnumerator Message_recieve()
    {
        // 메시지 수신 처리
        
        byte[] recvBuff = new byte[10240000];
        //int recvBytes = socket.Receive(recvBuff);
        int n = socket.Receive(recvBuff);

        //string dataReceived = Encoding.UTF8.GetString(recvBuff, 0, recvBytes); //Converting byte data to string

        received_message = recvBuff;

        //socket.
        //received_message = BitConverter.GetBytes(recvBytes);
        Debug.Log("Recv!");

        yield return new WaitForSeconds(1.0f);
        //yield return null;
        

    }

    //public void Stop()
    //{
    //    _clientCancelled = true;
    //    _clientThread?.Join();
    //    _clientThread = null;
    //}



    // NetMQ Code

    //public PublisherSocket publisher;
    //public SubscriberSocket subscriber;


    //public byte[] received_message ;
    // NetMQ\
    //public Thread _clientThread;
    //public readonly Action<string> _messageCallback;
    //public bool _clientCancelled;

    //public void Checksocket()
    //{
    //    publisher = new PublisherSocket();
    //    publisher.Bind("tcp://localhost:11011");

    //}

    //public void InChecksocket()
    //{
    //    try
    //    {
    //        AsyncIO.ForceDotNet.Force();
    //        subscriber = new SubscriberSocket();
    //        subscriber.Connect("tcp://127.0.0.1:11012");
    //        subscriber.Subscribe("");
    //        Debug.Log("Subscriber connected and subscribed");
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("Failed to initialize the subscriber socket: " + ex.ToString());
    //    }
    //}

    //public void Send_data()
    //{
    //    publisher.SendFrame("unity data");
    //}

    //public void Message_recieve()
    //{
    //    // 메시지 수신 처리
    //    try
    //    {

    //        while (!_clientCancelled)
    //        {
    //            if (!subscriber.TryReceiveFrameBytes(out byte[] message)) continue;
    //            received_message = message;
    //        }
    //        subscriber.Close();
    //        NetMQConfig.Cleanup();
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.LogError("Error in Message_receive: " + ex.Message);
    //    }

    //}

    //public void Stop()
    //{
    //    _clientCancelled = true;
    //    _clientThread?.Join();
    //    _clientThread = null;
    //}
}
