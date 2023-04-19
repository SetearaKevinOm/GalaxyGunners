using System.Collections;
using System.Collections.Generic;
using Kevin;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlienFighters : EnemyBase
{
    public Transform alienLaser;
    public GameObject alienProjectile;
    public int randomShootDelay;
    public GameObject forceField;
    public bool randomForceFields;

    [Header("Hacky AI")] 
    public bool hack;
    public bool goingLeft;
    public bool goingRight;
    public float speed;
    public float moveRate;
    public float timer;
    
    public void Start()
    {
        AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        //if(randomForceFields) ForceFieldRandomizer();
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        int coinFlip = Random.Range(0, 100);
        if (coinFlip < 50)goingLeft = true;
        else goingRight = true;
        
        randomShootDelay = Random.Range(2, 5);
        speed = Random.Range(5f, 10f);
        ShootPlayer();
    }

    /*private void ForceFieldRandomizer()
    {
        forceField = GetComponentInChildren<ForceField>().gameObject;
        int coinFlip = Random.Range(0, 101);
        if (coinFlip <= 49)
        {
            forceField.SetActive(true);
        }
        else
        {
            forceField.SetActive(false);
        }
    }*/

    private void ShootPlayer()
    {
        GameObject go = Instantiate(alienProjectile, alienLaser.position,
            Quaternion.LookRotation(alienLaser.transform.forward));
        go.GetComponent<EnemyProjectile>().balisticsTransform = alienLaser;
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(randomShootDelay);
        ShootPlayer();
    }
    public void Update()
    {
        transform.LookAt(GameManager.Instance.shipCollisionBox.transform.position);
        if (!hack) return;
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            ChangeDirection();
            timer = 0f;
        }
        
        if (goingLeft) Moving(Vector3.left);
        if (goingRight) Moving(Vector3.right);
    }

    private void ChangeDirection()
    {
        if (goingLeft)
        {
            goingLeft = false;
            goingRight = true;
        }
        else if (goingRight)
        {
            goingLeft = true;
            goingRight = false;
        }
    }
    private void Moving(Vector3 direction)
    {
        transform.position += direction * (Time.deltaTime * speed);
    }
}
