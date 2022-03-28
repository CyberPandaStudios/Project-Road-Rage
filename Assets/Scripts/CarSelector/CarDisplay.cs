using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour 
{
    [Header("Car Name")]
    [SerializeField] private Text carName; 
    
    [Header("Car Stats")]
    [SerializeField] private Image carSpeed; 
    [SerializeField] private Image carControl; 
    [SerializeField] private Image carDamage; 

    [Header("Car Model")]
    [SerializeField] private Transform carHolder; 

    private GameObject currentCar = null;

    public void DisplayCar(GameObject car)
    {
        if(currentCar != null){
            Destroy(currentCar);
            currentCar = Instantiate(car, carHolder.position, carHolder.rotation, carHolder);
        }else
            currentCar = Instantiate(car, carHolder.position, carHolder.rotation, carHolder);
        

        /*
        carName.text = _car.carName;

        carSpeed.fillAmount = _car.speed / 100; 
        carControl.fillAmount = _car.control / 100;
        carDamage.fillAmount = _car.damage / 100; 

        if (carHolder.childCount > 0)
            Destroy(carHolder.GetChild(0).gameObject);
    */
    }

}