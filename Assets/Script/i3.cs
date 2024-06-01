using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeLightIntensity : MonoBehaviour
{
    public GameObject lightsContainer; // Objeto vacío que contiene las luces
    public float newIntensity = 1.0f; // La nueva intensidad que deseas asignar a las luces
    public float maxIntensity = 5.0f; // Intensidad máxima permitida

    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable == null)
        {
            Debug.LogError("XRBaseInteractable component is missing.");
        }
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        ChangeLightsIntensity();
    }

    public void ChangeLightsIntensity()
    {
        if (lightsContainer != null)
        {
            Light[] lights = lightsContainer.GetComponentsInChildren<Light>();
            if (lights.Length > 0)
            {
                foreach (Light light in lights)
                {
                    if (light != null)
                    {
                        light.intensity = Mathf.Clamp(newIntensity, 0, maxIntensity);
                    }
                    else
                    {
                        Debug.LogWarning("One of the lights in the list is null!");
                    }
                }
            }
            else
            {
                Debug.LogWarning("No lights found in the container!");
            }
        }
        else
        {
            Debug.LogWarning("Lights container is not assigned!");
        }
    }
}
