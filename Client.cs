using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net.Http.Headers;

public class Client : MonoBehaviour
{
    [SerializeField] VariableJoystick Joystick;
    [SerializeField] DetectionTest TagDetector;
    [SerializeField] RobotManagerV2 RobotMG;
    
    private ClientThread ct;
    public ClientJson ClientJsn;
    private bool isSend;
    private bool isReceive;

    private void Start()
    {
        ct = new ClientThread(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp, "10.100.3.46", 8000);
        ct.StartConnect();
        isSend = true;
    }

    private void Update()
    {
        if (ct.receiveMessage != null)
        {
            Debug.Log("Server:" + ct.receiveMessage);
            ct.receiveMessage = null;
        }
        if (isSend == true)
            StartCoroutine(delaySend());

        ct.Receive();
    }

    private IEnumerator delaySend()
    {
        /* //For ipad joystick
        ClientJsn = new ClientJson(RobotMG.SelectedID, Joystick.Horizontal, Joystick.Vertical);*/
        
        //For Phone Joystick
        ClientJsn = new ClientJson(RobotMG.SelectedID, Joystick.Vertical * -1f, Joystick.Horizontal  * -1f);
        var ToJsn = JsonUtility.ToJson(ClientJsn);

        isSend = false;
        yield return new WaitForSeconds(0.03f);

        ct.Send(ToJsn);
        Debug.Log(ToJsn);
        isSend = true;
    }
    
    private void OnApplicationQuit()
    {
        ct.StopConnect();
    }

    public struct ClientJson
    {
        public int ID;
        public float Linear;
        public float Angular;

        public ClientJson(int id, float linear, float angular)
        {
            ID = id;
            Linear = linear;
            Angular = angular;
        }
    }
}