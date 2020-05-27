using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject effect;
    public GameObject[] childEffects;
    public bool enemy = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Structure")
        {
            Instantiate(effect,transform.position,Quaternion.identity);

            for (int i = 0; i < childEffects.Length; i++)
            {
                childEffects[i].transform.SetParent(null);
            }

            Destroy(gameObject);
        }

        if (other.tag == "Player" && enemy == true)
        {
            other.GetComponent<PlayerStats>().health -= 10f;
            

            Destroy(gameObject);
        }

        if (other.tag == "Enemy" && enemy == false)
        {
            other.GetComponent<EnemyStats>().health -= 10f;
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
