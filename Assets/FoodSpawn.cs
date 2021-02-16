using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodSpawn : MonoBehaviour
{

    public bool hadSushi = false, hadDango = false, hadMilkTea = false, hadRamen = false;
    [Serializable]
    public class FoodSprites
    {
        public GameObject[] foodSprites;
    }
    public FoodSprites food;


    void Update()
    {
        
        if (PlayerController.pelletCount == 20 && hadSushi == false)
        {
            hadSushi = true;
            Instantiate(food.foodSprites[0], transform.position, Quaternion.identity);
        }
        if (PlayerController.pelletCount == 40 && hadDango == false)
        {
            hadDango = true;
            Instantiate(food.foodSprites[1], transform.position, Quaternion.identity);
        }
        if (PlayerController.pelletCount == 60 && hadMilkTea == false)
        {
            hadMilkTea = true;
            Instantiate(food.foodSprites[2], transform.position, Quaternion.identity);
        }
        if (PlayerController.pelletCount == 80 && hadRamen == false)
        {
            hadRamen = true;
            Instantiate(food.foodSprites[3], transform.position, Quaternion.identity);
        }
    }

}
