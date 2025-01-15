using UnityEngine;

public class Agent_spawner : MonoBehaviour
{
    [SerializeField] AiAgent[] agents;
    [SerializeField] AiAgent[] ops;

    [SerializeField] LayerMask layersMask;

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) index = ++index % agents.Length;

        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layersMask))
            {
                Instantiate(agents[index], hitInfo.point, Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonDown(1) || (Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layersMask))
            {
                Instantiate(ops[index], hitInfo.point, Quaternion.identity);
            }
        }
        
    }
}
