using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public float powerUpDropChance = 1f;
    public ScoreCounter scoreCounter;

    protected bool calledShipDestroyed = false;
    protected BoundsCheck bndCheck;

    void start()
    {
        // GameObject scoreGO = GameObject.Find("ScoreCounter");
        // scoreCounter = scoreGO.GetComponent<ScoreCounter>();

        scoreCounter = FindObjectOfType<ScoreCounter>();
    
        if (scoreCounter == null)
        {
            Debug.LogError("ScoreCounter component not found in the scene.");
        }
    }
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    public Vector3 pos 
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }
    void Update()
    {
        if (scoreCounter == null)
        {
            GameObject scoreGO = GameObject.Find("ScoreCounter");
            if (scoreGO != null)
            {
                scoreCounter = scoreGO.GetComponent<ScoreCounter>();
                Debug.Log("ScoreCounter reassigned in Update.");
            }
            else
            {
                Debug.LogError("ScoreCounter GameObject not found in scene.");
            }
        }
        
        Move(); 

        if(bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            Destroy(gameObject);
        }

        // if(!bndCheck.isOnScreen)
        // {
        //     if(pos.y < bndCheck.camHeight - bndCheck.radius)
        //     {
        //         Destroy(gameObject);
        //     }
        // }  
    }
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    void OnCollisionEnter(Collision coll)
    {
        if (scoreCounter == null)
        {
            Debug.LogError("ScoreCounter is null before collision logic.");
        }
        GameObject otherGO = coll.gameObject;
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if(p != null)
        {
            if(bndCheck.isOnScreen)
            {
                health -= Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;
                if(health <= 0)
                {
                    if(!calledShipDestroyed)
                    {
                        calledShipDestroyed = true;
                        Main.SHIP_DESTROYED(this);
                        scoreCounter.score += 100;
                    }
                    Destroy(this.gameObject);
                }
            }
            Destroy(otherGO);
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

}
