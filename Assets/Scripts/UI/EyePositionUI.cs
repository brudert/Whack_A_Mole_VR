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
        Dictionary<string, object> data = gaze.GetCachedData();
        if ((bool)data["GazeHit0"])
        {
            leftEyeText.text = "LX : " + ((float)data["GazeHitPosition0X"]).ToString("F2") +
                "\nLY : " + ((float)data["GazeHitPosition0Y"]).ToString("F2") +
                "\nLZ : " + ((float)data["GazeHitPosition0Z"]).ToString("F2");
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
            leftEyeText.text = "LX : N/A\nLY : N/A\nLZ : N/A";
        }
        if ((bool)data["GazeHit1"])
        {
            rightEyeText.text = "RX : " + ((float)data["GazeHitPosition1X"]).ToString("F2") +
                "\nRY : " + ((float)data["GazeHitPosition1Y"]).ToString("F2") +
                "\nRZ : " + ((float)data["GazeHitPosition1Z"]).ToString("F2");
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
            rightEyeText.text = "RX : N/A\nRY : N/A\nRZ : N/A";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisual();
    }
}
