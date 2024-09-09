using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 10, maxEnergy = 10;
    int currentHealth, currentEnergy;
    int playerScore = 0;
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
        playerScore = 0;
        UIManager.uiManagerInstance.ShowScore(playerScore);
        attackObject.SetActive(false);
        isAttacking = false;
        isTeleport = false;
        isJump = false;
        currentHealth = maxHealth;
        UIManager.uiManagerInstance.ShowHealth(currentHealth, maxHealth);
        currentEnergy = maxEnergy;
        UIManager.uiManagerInstance.ShowEnergy(currentEnergy, maxEnergy);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        mRigidbody = GetComponent<Rigidbody>();
        teleportLineBeam.gameObject.SetActive(false);
    }

    public void AddScore()
    {
        playerScore++;
        UIManager.uiManagerInstance.ShowScore(playerScore);
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
        if(Input.GetMouseButtonDown(0) && !isAttacking && !isTeleport && !isJump)
        {
            isAttacking = true;
            attackObject.SetActive(true);
        }
        if(isAttacking && Input.GetMouseButtonUp(0))
        {
            RigidbodyConstraints oldConstrain = mRigidbody.constraints;
            mRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            isAttacking = false;
            attackObject.SetActive(false);
            mRigidbody.constraints = oldConstrain;
            Debug.Log("player stop attack");
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
            if (!isTeleport && currentEnergy >= teleportEnergyCharge)
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
        UIManager.uiManagerInstance.ShowHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
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
                UIManager.uiManagerInstance.ShowEnergy(currentEnergy, maxEnergy);
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
            int bonusAddNumber = collision.gameObject.GetComponent<BonusController>().bonusAddNumber;
            switch (bonusType)
            {
                case 0:
                    Debug.Log("player add health");
                    currentHealth += bonusAddNumber;
                    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                    UIManager.uiManagerInstance.ShowHealth(currentHealth, maxHealth);
                    //add health
                    Destroy(collision.gameObject);
                    break;
                //case 1:
                //    Debug.Log("player add coins");
                //    playerScore += bonusAddNumber;
                //    //add coins
                //    Destroy(collision.gameObject);
                //    break;
                default:
                    Debug.Log("player add energy");
                    currentEnergy += bonusAddNumber;
                    currentEnergy = Mathf.Clamp(currentEnergy,0,maxEnergy);
                    UIManager.uiManagerInstance.ShowEnergy(currentEnergy, maxEnergy);
                    //add energy
                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}
