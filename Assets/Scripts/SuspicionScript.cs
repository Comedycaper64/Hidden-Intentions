using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionScript : MonoBehaviour
{
    private Slider thisSlider;
    private PlayerMovement player;
    [SerializeField] private GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        thisSlider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisSlider.value > 99)
        {
            player.canMove = false;
            gameOverScreen.SetActive(true);
        }
    }

    public void AddSuspicion(int addition)
    {
        thisSlider.value += addition;
    }

    public float GetCurrentValue()
    {
        return thisSlider.value;
    }
}
