using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HayMachineSwitcher : MonoBehaviour, IPointerClickHandler
{
    
    public GameObject blueHayMachine;
    public GameObject yellowHayMachine;
    public GameObject redHayMachine;

    private int selectedIndex;


    public void OnPointerClick(PointerEventData eventData) 
    {
        selectedIndex++; 
        selectedIndex %= Enum.GetValues(typeof(HayMachineColor)).Length; // use modulus to loop through 3 vals

        GameSettings.hayMachineColor = (HayMachineColor)selectedIndex; // set chosen color

        // enable desired color and disable the others
        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                blueHayMachine.SetActive(true);
                yellowHayMachine.SetActive(false);
                redHayMachine.SetActive(false);
                break;

            case HayMachineColor.Yellow:
                blueHayMachine.SetActive(false);
                yellowHayMachine.SetActive(true);
                redHayMachine.SetActive(false);
                break;

            case HayMachineColor.Red:
                blueHayMachine.SetActive(false);
                yellowHayMachine.SetActive(false);
                redHayMachine.SetActive(true);
                break;
        }
    }
}
