using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    public static void mouseVisible()
    {
        Debug.Log("Cursor is now visible");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public static void mouseInvisible()
    {
        Debug.Log("Cursor is now invisible");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
