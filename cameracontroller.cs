using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    public float panspeed;
    public float scrollspeed;
    public float miny;
    public float maxy;
    public bool canmove;
    public GameObject pausescreen;
    public AudioSource backgroundmusic;
    // Start is called before the first frame update
    void Start()
    {
        pausescreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausescreen.SetActive(true);
            backgroundmusic.Pause();
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * panspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * panspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * panspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * panspeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 direction = transform.position;
        direction += Vector3.up * scroll * scrollspeed * Time.deltaTime;
        direction.y = Mathf.Clamp(direction.y, miny, maxy);
        transform.position = direction;

    }
}
