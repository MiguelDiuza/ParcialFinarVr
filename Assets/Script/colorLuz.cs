using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    public List<Light> lightsToChange; // Lista de luces que deseas cambiar
    public Color newColor; // El nuevo color que deseas asignar a las luces

    public void ChangeLightsColor()
    {
        if (lightsToChange != null && lightsToChange.Count > 0)
        {
            foreach (Light light in lightsToChange)
            {
                if (light != null)
                {
                    light.color = newColor;
                }
                else
                {
                    Debug.LogWarning("One of the lights in the list is null!");
                }
            }
        }
        else
        {
            Debug.LogWarning("Lights to change list is not assigned or empty!");
        }
    }
}
