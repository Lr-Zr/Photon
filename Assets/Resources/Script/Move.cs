using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class Move : MonoBehaviour
{
    [SerializeField]
    [Range(0, 20)]
    public float moveSpeed = 10.0f;

    PhotonView pv;

    private string logText;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Input_Move();

        //pv.RPC("ttt", 1, "aalalal");
    }

    void Input_Move()
    {
        if (!pv.IsMine) return;

        if (Input.GetKey(KeyCode.W))
        {
            float moveDelta = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveDelta);
        }
        if (Input.GetKey(KeyCode.S))
        {
            float moveDelta = moveSpeed * Time.deltaTime;
            transform.Translate(Vector3.back * moveDelta);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (pv != null && pv.IsMine)
        {
            PhotonView otherPV = collision.gameObject.GetComponent<PhotonView>();
            if (otherPV != null && !otherPV.IsMine)
            {
                pv.RPC("TTTT", RpcTarget.All, pv.ViewID, otherPV.ViewID,100);
            }
        }
    }

    [PunRPC]
    void TTTT(int myid, int otherid,int Damage)
    {
        logText = "Collision - my id : " + myid + ",other ID:" + otherid;
        Debug.Log(myid + "touch  " + otherid);
    }

    private void OnGUI()
    {

        GUI.TextArea(new Rect(10, 50, 300, 20), logText);
    }
}
