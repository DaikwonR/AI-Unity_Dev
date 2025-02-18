using UnityEngine;

public class Agent_destination : MonoBehaviour
{
    [SerializeField] LayerMask layersMask;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))// || (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layersMask))
            {
                
                var movements = GameObject.FindObjectsByType<Movement>(FindObjectsSortMode.None);

                foreach (var movement in movements)
                {
                    print("q");
                    movement.Destination = hitInfo.point;
                }
            }
        }

    }
}
