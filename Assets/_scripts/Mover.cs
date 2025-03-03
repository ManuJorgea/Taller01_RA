using UnityEngine;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{
    public GameObject imagenLose;

    public Slider slider;
    float mov, rot;
    public float velMov, velRot;
    Animator anim;
    public int vida = 10;

    private void Start()
    {
       imagenLose.SetActive(false);

       anim = GetComponent<Animator>();
       slider.maxValue = vida;
       slider.value = vida;
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            Desplazamiento();
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            Rotacion();
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }
    }
    void Desplazamiento()
    {
        mov = Input.GetAxis("Vertical") * velMov * Time.deltaTime;
        transform.Translate(0, 0, mov);
    }
    void Rotacion()
    {
        rot = Input.GetAxis("Horizontal") * velRot * Time.deltaTime;
        transform.Rotate(0, rot, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("ArmaCPU"))
        {
            anim.SetTrigger("GetHit");

            vida -= 1;
            slider.value = vida;

            if (vida <= 0)
            {
                imagenLose.SetActive(true);
            }
        }
    }
}
