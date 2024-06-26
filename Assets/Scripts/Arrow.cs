using UnityEngine;

namespace Archer
{

    public class Arrow : MonoBehaviour
    {

        private Rigidbody arrowRigidbody;
        private bool hit;

        private void Awake()
        {
            // Establecer las referencias de Rigidbody (para detener la flecha) y AudioSource (para el sonido de impacto)
            arrowRigidbody = GetComponent<Rigidbody>();
           
        }

        // El rigidbody de la flecha es tipo Trigger, para que no colisione
        private void OnTriggerEnter(Collider other)
        {
            // La flecha s�lo produce da�o y ruido en el primer impacto
            if (hit) {
                return;
            }

            // Si impacta con el jugador, lo ignoramos
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return;
            }

            hit = true;

            // Reproducir el impacto de la flecha

            // Hacemos que la flecha sea hija del objeto contra el que impacta, para que se mueva con el
            this.transform.SetParent(other.transform);

            // Hacemos que la flecha sea kinematica para que no responda a nuevas aceleraciones (se quede clavada)
            this.GetComponent<Rigidbody>().isKinematic = true;

            // Miramos a ver si el objeto contra el que ha impacto la flecha tiene un componente Enemy...
            if (other.GetComponentInParent<Enemy>() != null)
            {
                // ... Y si lo tiene, le hacemos da�o (la siguiente comprohaci�n es equivalente a hacer if (enemy != null) { enemy.Hit(); }
                other.GetComponentInParent<Enemy>().Hit();
            }

            

        }

    }

}