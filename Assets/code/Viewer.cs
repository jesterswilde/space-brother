using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Viewer : MonoBehaviour {

    static Viewer t;
    SpaceDetectors[] detectors;
    [SerializeField]
    LayerMask insideMask; 
    public static LayerMask InsideMask { get { return t.insideMask; } }
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float sensitivity = 1;
    [SerializeField]
    float spinSpeed = 1;

    Vector3 prevDir; 
    Vector3 prevPos;
    [SerializeField]
    Transform posTrans;
    [SerializeField]
    Transform xRotTrans;
    [SerializeField]
    Transform yRotTrans;
    [SerializeField]
    Transform spinTrans;
    [SerializeField]
    Transform camTrans;
    [SerializeField]
    Transform roomPlaceTrans;
    public static Transform RoomPlaceTrans { get { return t.roomPlaceTrans; } }
    [SerializeField]
    Transform roomRotTrans; 
    public static Transform RoomRotTrans { get { return t.roomRotTrans; } }


    private void Awake()
    {
        prevPos = transform.position; 
        t = this; 
        detectors = GetComponentsInChildren<SpaceDetectors>();
    }
    private void Update()
    {
        if (!InputManager.IsRotatingBuildling)
        {
            if (true)//CanMove
            {
                prevPos = posTrans.position; 
                Move();
            }
            else
            {
                posTrans.position += (prevPos - posTrans.position).normalized * 0.5f;
            
            }
            Spin();
            Rotate(); 
        }
    }

    bool CanMove()
    {
        return detectors.All((detector) => detector.isInside());
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        posTrans.position += (camTrans.forward * vertical + camTrans.right * horizontal)
            .normalized * speed * Time.deltaTime; 
    }
    void Spin()
    {
        float spinDir = 0;
        if (Input.GetKey(KeyCode.E))
        {
            spinDir += 1; 
        }
        if (Input.GetKey(KeyCode.Q))
        {
            spinDir -= 1; 
        }
        spinTrans.Rotate(camTrans.forward, spinDir * spinSpeed * Time.deltaTime * 90);
    }
    void Rotate()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        xRotTrans.Rotate(new Vector3(0, x, 0));
        yRotTrans.Rotate(new Vector3(y * -1, 0, 0));
    }
}
