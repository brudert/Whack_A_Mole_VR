using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLeftDisplayer : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    [SerializeField]
    private Mole mole;
    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        _textMeshPro.text = "text";
    }

    // Update is called once per frame
    void Update()
    {
        float tleft = mole.GetActivatedTimeLeft();
        tleft = tleft > 0 ? tleft : 0;
        _textMeshPro.text = tleft.ToString("0");
    }
}
