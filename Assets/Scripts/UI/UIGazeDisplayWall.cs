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
    private Text leftEyeText;
    [SerializeField]
    private Text rightEyeText;
    [SerializeField]
    private Transform leftEyeHitPointBall;
    [SerializeField]
    private Transform rightEyeHitPointBall;

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
            leftEyeText.text = "Left Eye :\nX : " + ((float)data["GazeHitPosition0X"]).ToString() +
                "\nY : " + ((float)data["GazeHitPosition0Y"]).ToString() +
                "\nZ : " + ((float)data["GazeHitPosition0Z"]).ToString();
            leftEyeHitPointBall.gameObject.SetActive(true);
            leftEyeHitPointBall.position = new Vector3(
                (float)data["GazeHitPosition0X"],
                (float)data["GazeHitPosition0Y"],
                (float)data["GazeHitPosition0Z"]
            );
        }
        else
        {
            leftEyeHitPointBall.gameObject.SetActive(false);
            leftEyeText.text = "Right Eye :\nX : N/A\nY : N/A\nZ : N/A";
        }
        if ((bool)data["GazeHit1"])
        {
            rightEyeText.text = "Right Eye :\nX : " + ((float)data["GazeHitPosition1X"]).ToString() +
                "\nY : " + ((float)data["GazeHitPosition1Y"]).ToString() +
                "\nZ : " + ((float)data["GazeHitPosition1Z"]).ToString();
            rightEyeHitPointBall.gameObject.SetActive(true);
            rightEyeHitPointBall.position = new Vector3(
                (float)data["GazeHitPosition1X"],
                (float)data["GazeHitPosition1Y"],
                (float)data["GazeHitPosition1Z"]
            );
        }
        else
        {
            rightEyeHitPointBall.gameObject.SetActive(false);
            rightEyeText.text = "Right Eye :\nX : N/A\nY : N/A\nZ : N/A";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisual();
    }
}
