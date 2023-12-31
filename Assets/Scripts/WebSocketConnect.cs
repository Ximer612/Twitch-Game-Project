using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WebSocketConnect : MonoBehaviour
{
    public static void Start()
    {
        Task t = Echo();
        t.Wait();
    }

    private static async Task Echo()
    {
        using (ClientWebSocket ws = new ClientWebSocket())
        {
            Uri serverUri = new Uri("wss://pubsub-edge.twitch.tv");
            await ws.ConnectAsync(serverUri, CancellationToken.None);
            while (ws.State == WebSocketState.Open)
            {
                Console.Write("Input message ('exit' to exit): ");
                string msg = "{\"type\":\"PING\"}";
                Console.ReadLine();
                if (msg == "exit")
                {
                    break;
                }
                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            }
        }
    }
}
