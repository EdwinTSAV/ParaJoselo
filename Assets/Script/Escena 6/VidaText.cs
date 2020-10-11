using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaText : MonoBehaviour
{
    private int Vidas = 3;
    public Text PuntajeVida;
    // Start is called before the first frame update
    public int GetVida()
    {
        return Vidas;
    }

    // Update is called once per frame
    public void QuitarVida (int Vidas)
    {
        this.Vidas -= Vidas;
        PuntajeVida.text = "Vidas: " + GetVida();
    }
}
