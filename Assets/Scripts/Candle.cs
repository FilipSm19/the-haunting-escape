using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool picked;
    private bool messageShown;
    public GameObject message;
    public PickableObject candle;
    private bool candleOn;
    private bool showSmoke;
    public float smoothTime = 20F;
    public float moveSpeed = 2;
    public Vector3 velocity = Vector3.zero;
    public GameObject flame;
    public ParticleSystem smoke;
    public AudioSource blow;
    public AudioSource ignite;
    // Start is called before the first frame update
    void Start()
    {
        candleOn = false;
        picked = false;
        message.SetActive(false);
        transform.localPosition = new Vector3(0.099f, -1f, -0.046f);
    }

    // Update is called once per frame
    void Update()
    {
        picked = candle.picked;
        if (picked == true && messageShown == false)
        {
            StartCoroutine(showMessage());
            messageShown = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && picked == true)
        {
            candleOn = !candleOn;
            if (candleOn == false) showSmoke = true;
            else ignite.Play();
        }
        if (candleOn)
        {
            showCandle();
        }
        else
        {
            hideCandle();
        }
        if (transform.localPosition.x < 0.17f)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if (transform.localPosition.x > 0.17f)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }

    }
    IEnumerator showMessage()
    {
        message.SetActive(true);
        yield return new WaitForSeconds(5);
        message.SetActive(false);
    }
    public void showCandle()
    {

        flame.SetActive(true);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(0.313f, -0.3f, 0.45f), ref velocity, smoothTime * Time.deltaTime);
    }
    public void hideCandle()
    {

        flame.SetActive(false);
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0.099f, -1f, -0.046f), moveSpeed * Time.deltaTime);


        if (showSmoke == true)
        {
            blow.Play();
            smoke.Play();
        }
        showSmoke = false;
    }
}
