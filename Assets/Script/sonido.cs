using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SonidoTocar : MonoBehaviour
{
    public GameObject objetoParaEncender; // Objeto que se encenderá al tocar
    public AudioClip audioAlAgarrar; // Audio que se reproducirá al agarrar
    public AudioClip audioAlColisionar; // Audio que se reproducirá al colisionar
    private XRGrabInteractable grabInteractable;
    private AudioSource audioSourceAgarrar; // AudioSource para el audio de agarrar
    private AudioSource audioSourceColisionar; // AudioSource para el audio de colisión
    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSourceAgarrar = gameObject.AddComponent<AudioSource>();
        audioSourceColisionar = gameObject.AddComponent<AudioSource>();

        if (objetoParaEncender != null)
        {
            objetoParaEncender.SetActive(false); // Asegúrate de que el objeto esté inicialmente desactivado
        }
        else
        {
            Debug.LogWarning("El objeto para encender no ha sido asignado");
        }

        // Configurar el AudioSource
        if (audioAlAgarrar != null)
        {
            audioSourceAgarrar.clip = audioAlAgarrar;
            audioSourceAgarrar.playOnAwake = false;
            audioSourceAgarrar.enabled = false; // Inicialmente desactivado
        }

        if (audioAlColisionar != null)
        {
            audioSourceColisionar.clip = audioAlColisionar;
            audioSourceColisionar.playOnAwake = false;
        }

        // Iniciar la corrutina para activar el script después de 5 segundos
        StartCoroutine(ActivateAfterDelay(5.0f));
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateScript();
    }

    private void ActivateScript()
    {
        isActivated = true;

        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnHandGrab);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable no encontrado en el objeto");
        }
    }

    private void OnHandGrab(XRBaseInteractor interactor)
    {
        if (isActivated)
        {
            if (objetoParaEncender != null)
            {
                objetoParaEncender.SetActive(true);
            }

            if (audioSourceAgarrar != null)
            {
                audioSourceAgarrar.enabled = true; // Activar el AudioSource
                audioSourceAgarrar.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActivated && audioSourceColisionar != null)
        {
            audioSourceColisionar.PlayOneShot(audioAlColisionar);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null && isActivated)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnHandGrab);
        }
    }
}
