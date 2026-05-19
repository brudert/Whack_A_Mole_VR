using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class UIGazeDisplayWall : MonoBehaviour
{
    [SerializeField]
    public WallManager wallManager;
    [SerializeField]
    public HTCGazeLogger gaze;
    [SerializeField]
    private GameObject leftEye;
    [SerializeField]
    private GameObject rightEye;
    [SerializeField]
    private Text leftEyeText;
    [SerializeField]
    private Text rightEyeText;

    private float SelfWidth;

    // Start is called before the first frame update
    void Start()
    {
        SelfWidth = GetComponent<RectTransform>().sizeDelta.x;
        UpdateVisual();
    }

    private float n;
    void UpdateVisual()
    {
        WallInfo winfo = wallManager.CreateWallInfo();
        // find the slace difference between the actual wall and the UI wall
        float scale = SelfWidth/ (winfo.meshBoundsXmax - winfo.meshBoundsXmin);
        //get wall center and remove Z component
        //Vector3 wcenter = winfo.wallCenter;
        //wcenter.z = 0;
        Vector3 wcenter = new Vector3(
            (winfo.meshBoundsXmax + winfo.meshBoundsXmin) / 2,
            (winfo.meshBoundsYmax + winfo.meshBoundsYmin) / 2
        );
        Dictionary<string, object> data = gaze.GetData();
        if ((bool)data["GazeHit0"])
        {
            leftEye.SetActive(true);
            Vector3 newPos = new Vector3((float)data["GazeHitPosition0X"], (float)data["GazeHitPosition0Y"]);
            newPos -= wcenter; // Anchor coordinates to center
            leftEye.transform.localPosition = newPos*scale;
            leftEyeText.text = "Left Eye :\nX : " + data["GazeHitPosition0X"].ToString() +
                "\nY : " + data["GazeHitPosition0Y"].ToString() +
                "\nZ : " + data["GazeHitPosition0Z"].ToString();
        }
        else
        {
            leftEye.SetActive(false);
            leftEyeText.text = "Right Eye :\nX : N/A\nY : N/A\nZ : N/A";
        }
        if ((bool)data["GazeHit1"])
        {
            rightEye.SetActive(true);
            Vector3 newPos = new Vector3((float)data["GazeHitPosition1X"], (float)data["GazeHitPosition1Y"]);
            newPos -= wcenter; // Anchor coordinates to center
            rightEye.transform.localPosition = newPos*scale;
            rightEyeText.text = "Right Eye :\nX : " + data["GazeHitPosition1X"].ToString() +
                "\nY : " + data["GazeHitPosition1Y"].ToString() +
                "\nZ : " + data["GazeHitPosition1Z"].ToString();
        }
        else
        {
            rightEye.SetActive(false);
            rightEyeText.text = "Right Eye :\nX : N/A\nY : N/A\nZ : N/A";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisual();
    }
}
