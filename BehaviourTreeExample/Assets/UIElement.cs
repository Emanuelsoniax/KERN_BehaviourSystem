using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElement : MonoBehaviour
{
    private RectTransform rectTransform;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.rotation = Quaternion.LookRotation(transform.position - FindObjectOfType<Camera>().transform.position);
    }
}
