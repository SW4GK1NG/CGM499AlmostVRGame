using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public Rigidbody m_Fire;
    public Rigidbody m_Water;
    public Rigidbody m_Lightning;
    public Transform m_FireTransform;
    public float m_LaunchForce;
    public float FireCooldownTime;
    public Text SpellType;

    Rigidbody m_Shell;
    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        m_Shell = m_Fire;
        canShoot = true;
        MasterControl.Instance.Health = 10;
        SpellType.text = "Fire";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) {
            m_Shell = m_Fire;
            SpellType.text = "Fire";
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            m_Shell = m_Lightning;
            SpellType.text = "Leaf";
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            m_Shell = m_Water;
            SpellType.text = "Water";
        }

        if (Input.GetButtonDown("Fire1")) {
            Fire();
            StartCoroutine(ShotCD());
        }

    }

    void Fire ()
    {

        if(!canShoot) return;
        Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
        canShoot = false;

    }

    IEnumerator ShotCD() {
        yield return new WaitForSeconds(FireCooldownTime);
        canShoot = true;
        StopAllCoroutines();
    }
}
