using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] GameObject _cube;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _exam;

    public GameObject wall;

    GameObject _gap;
    Renderer rend;

    public bool ex;
    public bool create;
    void Start()
    {
        create = true;
        rend = _exam.GetComponent<MeshRenderer>();
    }
    void Update()
    {
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward * 20, Color.green, 0, true);
        if (create)
        {
            rend.sharedMaterial.color = new Color(0f, 0.1778189f, 0.7490196f, 0.25f);
        }
        else
        {
            rend.sharedMaterial.color = new Color(1f, 0.03301889f, 0.07127205f, 0.39f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ex = !ex;
        }
        if (ex && Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
        {
            Vector3 pos = new Vector3(RoundForFort(hit.point.x, hit, false, 0.5f), RoundForFort(hit.point.y, hit, true, 0.5f), RoundForFort(hit.point.z, hit, false, 0.5f));
            
            if (hit.collider == null || Vector3.Distance(transform.position, pos) > 6)
            {
                Destroy(_gap);
            }
            else if (_gap == null)
            {
                _gap = Instantiate(_exam, pos, Quaternion.identity);
            }
            else
            {
                _gap.transform.position = pos;
            }
        }
        else
        {
            Destroy(_gap);
        }
        if (ex && create && Input.GetMouseButtonDown(0) && Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
        {
            CreateCubes(_cube, hit, 0.5f);
        }
        if (ex && create && Input.GetKeyDown(KeyCode.V) && Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit))
        {
            CreateCubes(wall, hit, 0.5f);
        }
    }
    void CreateCubes(GameObject obj, RaycastHit hit, float offset)
    {
        Vector3 pos = new Vector3(RoundForFort(hit.point.x, hit, false, offset), RoundForFort(hit.point.y, hit, true, offset), RoundForFort(hit.point.z, hit, false, offset));
        if (hit.transform.position != pos && Vector3.Distance(transform.position, pos) <= 6)
        {
            Instantiate(obj, pos, Quaternion.identity);
        }
    }
    float RoundForFort(float value, RaycastHit hit, bool IsY, float offset)
    {
        int roundValue = Mathf.RoundToInt(value);
        if (hit.collider.tag == "ground")
        {
            if (IsY)
            {
                return roundValue + offset;
            }
            else
            {
                if (roundValue > value)
                {
                    return roundValue - offset;
                }
                else
                {
                    return roundValue + offset;
                }
            }
        }
        else
        {
            if (roundValue > value)
            {
                return roundValue - offset;
            }
            else
            {
                return roundValue + offset;
            }
        }
    }
    Vector3 RoundForWalls(Vector3 value, RaycastHit hit, float offset)
    {
        int roundValueX = Mathf.RoundToInt(value.x);
        int roundValueZ = Mathf.RoundToInt(value.z);
        int roundValueY = Mathf.RoundToInt(value.y);
        if (Mathf.Abs(roundValueZ - value.z) > Mathf.Abs(roundValueX - value.x))
        {
            if (roundValueZ > value.z)
            {
                if (roundValueX > value.x)
                {
                    if (roundValueY > value.y)
                    {
                        return new Vector3(roundValueX - offset, roundValueY - offset, roundValueZ);
                    }                  
                }
            }
        }
    }
    //void CreateWall(GameObject obj, RaycastHit hit)
    //{
    //    Vector3 pos = new Vector3(RoundForFort(hit.point.x, hit, false, 0), RoundForFort(hit.point.y, hit, true, 0), RoundForFort(hit.point.z, hit, false, 0));
    //    if (hit.transform.position != pos && Vector3.Distance)
    //    {

    //    }
    //}
}
