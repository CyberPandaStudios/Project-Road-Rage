using UnityEngine;

public class CarChanger : MonoBehaviour
{

    CarDisplay disCar;

    [Header ("Scriptable Objects")]
    [SerializeField] private GameObject[] cars;

    [Header("Display Scripts")]

    private int currentIndex;

    private void Start(){
        disCar = FindObjectOfType<CarDisplay>();
        cars = Resources.LoadAll<GameObject>("PlayerPrefabs");
        ChangeCar(0);
    }

    public void prevCar(){
        if(currentIndex != 0){
            currentIndex--;
            ChangeCar(currentIndex);
        }
        Debug.Log(currentIndex);
    }

    public void nextCar(){
        if(currentIndex != cars.Length - 1){
            currentIndex++;
            ChangeCar(currentIndex);
        }
        Debug.Log(currentIndex);
    }

    public void ChangeCar(int _change)
    {
        if (currentIndex < 0) currentIndex = cars.Length - 1;
        else if (currentIndex > cars.Length - 1) currentIndex = 0;
        
        disCar.DisplayCar(cars[currentIndex]);
    }
}