using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    [SerializeField] GameObject trampaEnemigosizq,trampaEnemigosder, puertaABloquear,luzAzul;
    [SerializeField] AudioSource finalMusic, backMusic, doorOpen;
    [SerializeField] Animator puertaizq, puertadch;
    bool activated = false;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if (trampaEnemigosizq.activeInHierarchy) return;
            puertaizq.SetTrigger("abrir");
            doorOpen.Play();
            finalMusic.Play();
            backMusic.Stop();
            Invoke("activarEnemigosIzq", 1f);
            puertaABloquear.SetActive(true);
            luzAzul.SetActive(false);
        }
    }

    private void Update() {
        if (trampaEnemigosizq.transform.childCount == 0 && !activated){
            activated = true;
            doorOpen.Play();
            puertadch.SetTrigger("abrir");
            Invoke("activarEnemigosDch", 1f);
        }
    }

    void activarEnemigosIzq(){
        trampaEnemigosizq.SetActive(true);
    }

    void activarEnemigosDch(){
        trampaEnemigosder.SetActive(true);
    }

    
}
