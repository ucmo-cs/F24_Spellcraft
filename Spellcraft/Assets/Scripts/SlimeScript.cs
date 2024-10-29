using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource Audio;
    public GameObject ScoreManager;
    public float distance = 0.5f;
    public float moveSpeed = 3f;
    Vector3 target;
    void Awake()
    {
        Audio = gameObject.GetComponent<AudioSource>();
        target = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("moveSlime");
        ScoreManager = GameObject.Find("ScoreManager");
    }

    void Update()
    {
        if(target.x < -7f)
            target.x = -7f;
        else if(target.x > 7.2f)
            target.x = 7.2f;
        if(target.y < -3.8f)
            target.y = -3.8f;
        else if(target.y > 4.5f)
            target.y = 4.5f;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    IEnumerator moveSlime()
    {
        switch(Random.Range(0, 3)) {
            case 0:
                //rb.MovePosition(new Vector2(transform.position.x + distance, transform.position.y));
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + distance, transform.position.y), moveSpeed * Time.deltaTime);
                target = new Vector3(transform.position.x - distance, transform.position.y);
                break;
            case 1:
                //rb.MovePosition(new Vector2(transform.position.x - distance, transform.position.y));
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - distance, transform.position.y), moveSpeed * Time.deltaTime);
                target = new Vector3(transform.position.x + distance, transform.position.y);
                break;
            case 2:
                //rb.MovePosition(new Vector2(transform.position.x, transform.position.y + distance));
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + distance), moveSpeed * Time.deltaTime);
                target = new Vector3(transform.position.x, transform.position.y - distance);
                break;
            case 3:
                //rb.MovePosition(new Vector2(transform.position.x, transform.position.y - distance));
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - distance), moveSpeed * Time.deltaTime);
                target = new Vector3(transform.position.x, transform.position.y + distance);
                break;
        }
        yield return new WaitForSeconds(3f);
        StartCoroutine("moveSlime");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fire") {
            ScoreManager.GetComponent<ScoreManagerScript>().score += 1;
            ScoreManager.GetComponent<ScoreManagerScript>().sounder();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
