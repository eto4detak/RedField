using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    //[Tooltip("Sensitivity multiplier for moving the camera around")]
    //public float lookSensitivity = 1f;
    //[Tooltip("Additional sensitivity multiplier for WebGL")]
    //public float webglLookSensitivityMultiplier = 0.25f;
    //[Tooltip("Limit to consider an input when using a trigger on a controller")]
    //public float triggerAxisThreshold = 0.4f;
    //[Tooltip("Used to flip the vertical input axis")]
    //public bool invertYAxis = false;
    //[Tooltip("Used to flip the horizontal input axis")]
    //public bool invertXAxis = false;
    SelectObjects selectObjects;
    private void Start()
    {
        selectObjects = GManager.selectObjects;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        //m_FireInputWasHeld = GetFireInputHeld();
    }
    public bool GetFireInputHeld()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 startPos = Input.mousePosition;
        }

        return false;
    }

    //private void OnRightMouseButton()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

    //        Collider[] intersecting = Physics.OverlapSphere(mousePosition, 0.01f);
    //        Rect rect = new Rect(Input.mousePosition, Input.mousePosition);
    //        selectObjects.SetSelected(rect);

    //    }

    //}


}
