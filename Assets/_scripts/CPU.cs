using UnityEngine;
using UnityEngine.UI;

public class CPU : MonoBehaviour
{
    public Slider slider;
    public int vida = 10;
    public float velDezplaza;
    public float velRot;
    public Transform player1pos;
    Vector3 vectorDiferencia;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        slider.maxValue = vida;
        slider.value = vida;
    }

    void Update()
    {
        if (Vector3.Distance(player1pos.position,this.transform.position) < 1)
        {
            Caceria();
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
        }
    }

    void Caceria()
    {
        vectorDiferencia = player1pos.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(vectorDiferencia), velRot * Time.deltaTime);


        if (Vector3.Distance(player1pos.position, this.transform.position) > 0.08)
        {
            this.transform.Translate(0, 0, velDezplaza * Time.deltaTime);
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("Attack");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("ArmaPlayer"))
        {
            anim.SetTrigger("GetHit");
            vida -= 1;
            slider.value = vida;
        }
    }
}
