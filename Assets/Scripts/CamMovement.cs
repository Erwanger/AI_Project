using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    Camera cam;
    Vector2 vec;
    GameObject[] presets;
    int actuPreset = -1;
    [SerializeField] Transform presetParent;

    GameObject[] textsPreset;
    [SerializeField] Transform textParent;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        vec = Vector2.zero;
        presets = new GameObject[presetParent.childCount];
        textsPreset = new GameObject[textParent.childCount];


        for (int i = 0; i < presetParent.childCount; i++)
        {
            presets[i] = presetParent.GetChild(i).gameObject;
            presets[i].SetActive(false);
        }

        for (int i = 0; i < presetParent.childCount; i++)
        {
            textsPreset[i] = textParent.GetChild(i).gameObject;
            textsPreset[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(cam.transform.position.x + Input.GetAxis("Horizontal"), cam.transform.position.y, cam.transform.position.z + Input.GetAxis("Vertical"));

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine("MakeSound", hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine("MakeSoundLoud", hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CyclePresets();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    IEnumerator MakeSound(Vector3 _vec)
    {
        float range = Random.Range(0.0f, 10.0f);

        GameObject sp = new GameObject("Sound");
        sp.transform.position = _vec;
        sp.transform.localScale = Vector3.one * 10;
        sp.AddComponent<SphereCollider>();
        sp.GetComponent<SphereCollider>().isTrigger = true;
        sp.tag = "Sound";
        sp.layer = 10;

        yield return new WaitForSeconds(0.1f);

        Destroy(sp);
    }

    IEnumerator MakeSoundLoud(Vector3 _vec)
    {
        float range = Random.Range(0.0f, 10.0f);

        GameObject sp = new GameObject("Sound");
        sp.transform.position = _vec;
        sp.transform.localScale = Vector3.one * 50;
        sp.AddComponent<SphereCollider>();
        sp.GetComponent<SphereCollider>().isTrigger = true;
        sp.tag = "Sound";
        sp.layer = 10;

        yield return new WaitForSeconds(0.1f);

        Destroy(sp);
    }

    private void CyclePresets()
    {
        if (actuPreset != -1)
        {
            presets[actuPreset].SetActive(false);
            textsPreset[actuPreset].SetActive(false);

        }

        actuPreset++;

        if (actuPreset >= presets.Length)
        {
            actuPreset = 0;
        }

        presets[actuPreset].SetActive(true);
        textsPreset[actuPreset].SetActive(true);
    }
}
