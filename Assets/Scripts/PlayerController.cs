using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 10, maxEnergy = 10;
    int currentHealth, currentEnergy;
    [SerializeField]
    int teleportEnergyCharge = 3;
    bool isJump = false;
    [SerializeField]
    float moveSpeed = 10, attackSpeed = 10, attackRange = 3;
    [SerializeField]
    float jumpForce = 5.0f;
    Rigidbody mRigidbody;
    Vector3 jump;
    bool isAttacking, isTeleport;
    RaycastHit hit;
    [SerializeField]
    LineRenderer teleportLineBeam;
    [SerializeField]
    GameObject attackObject;
    // Start is called before the first frame update
    void Start()
    {
        attackObject.SetActive(false);
        isAttacking = false;
        isTeleport = false;
        isJump = false;
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        mRigidbody = GetComponent<Rigidbody>();
        teleportLineBeam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //player move
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var move = new Vector3(h, 0, 0);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // attack
        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            attackObject.SetActive(true);
        }
        if(isAttacking && Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            attackObject.SetActive(false);
        }
        var torque = Input.GetAxis("Fire1");
        mRigidbody.AddTorque(transform.up * attackSpeed * torque);

        if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isTeleport)
        {
            mRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isJump = true;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if (!isTeleport && currentEnergy > teleportEnergyCharge)
            {
                //teleport
                isTeleport = true;
                teleportLineBeam.gameObject.SetActive(true);
            }
            else
            {
                isTeleport = false;
                teleportLineBeam.gameObject.SetActive(false);
            }
        }
        
        if (!isTeleport && Input.GetKeyDown(KeyCode.A))
        {
            //attack
        }
        if(isTeleport)
        {
            Teleport();
        }
    }

    public void SubtractHealth(int subNumber)
    {
        currentHealth -= subNumber;
        if(currentHealth <= 0)
        {
            // player die
        }
    }
    
    void Teleport()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 vtDir = hit.point - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), vtDir);
            //Debug.Log("Raycast " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            teleportLineBeam.SetPositions(new Vector3[] { transform.position, hit.point });
            RaycastHit rayHit;
            Vector3 rayDirect = (-transform.position + hit.point).normalized;
            float distance = (-transform.position + hit.point).magnitude;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(transform.position, rayDirect, out rayHit, distance))
                {
                    if (LayerMask.LayerToName(rayHit.collider.gameObject.layer).Equals("Cloud"))
                    {
                        //move player position to cloud
                        Debug.Log("to cloud");
                        transform.position = rayHit.collider.transform.GetChild(0).position;
                    }
                }
                teleportLineBeam.gameObject.SetActive(false);
                isTeleport = false;
                currentEnergy -= teleportEnergyCharge;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("ground") || collision.gameObject.tag.Equals("cloud"))
        {
            isJump = false;
        }
        else if (collision.gameObject.tag.Equals("bonus"))
        {
            int bonusType = collision.gameObject.GetComponent<BonusController>().bonusType;
            switch(bonusType)
            {
                case 0:
                    Debug.Log("player add health");
                    //add health
                    Destroy(collision.gameObject);
                    break;
                case 1:
                    Debug.Log("player add coins");
                    //add coins
                    Destroy(collision.gameObject);
                    break;
                default:
                    Debug.Log("player add energy");
                    //add energy
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}
