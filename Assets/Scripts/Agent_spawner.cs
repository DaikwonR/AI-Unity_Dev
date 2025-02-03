using UnityEngine;

public class Agent_spawner : MonoBehaviour
{
    [SerializeField] AiAgent[] agents;
    [SerializeField] AiAgent[] ops;

    [SerializeField] LayerMask layersMask;
    [Range(0, 5)][SerializeField] float randomMax = 0;

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) index = ++index % agents.Length;

        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layersMask))
            {
                Instantiate(agents[index], hitInfo.point, Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonDown(1) || (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layersMask))
            {
                Instantiate(ops[index], hitInfo.point + Random.onUnitSphere * randomMax * Random.value, Quaternion.identity);
            }
        }
        
    }
}
