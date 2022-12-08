using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BTUpdateUI : BTBaseNode
{
    private TextMeshProUGUI text;
    private string message;

    public BTUpdateUI(UIElement _UI, string _message)
    {
        text = _UI.text;
        message = _message;
    }

    public override TaskStatus Run()
    {
        text.text = "Current state: " + message;
        return TaskStatus.Success;
    }
}
