using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    // Constructors
    public void Init(float speed)
    {
        // makeRandomOrder();
        order = new List<DrinkIngredient.IngredientType>();
        order.Add(DrinkIngredient.IngredientType.GreenTea);
        order.Add(DrinkIngredient.IngredientType.Fruit);
        this.speed = speed;

        int spriteNum = Random.Range(0, 7);
        this.GetComponent<SpriteRenderer>().sprite = allNormalSprites[spriteNum];
    }

    public void Init(List<DrinkIngredient.IngredientType> special, float speed, int appearance)
    {
        order = special;
        this.speed = speed;

        if (appearance > 6 || appearance < 0)
        {
            Debug.Log("Appearance: index out of range");
            return;
        }

        this.GetComponent<SpriteRenderer>().sprite = allSpecialSprites[appearance];
    }
    
    [NonSerialized] public bool moving = false;

    // private variables
    private float timer = 20f;
    private float speed = 0f;
    private bool orderComplete = false;
    [SerializeField] List<DrinkIngredient.IngredientType> order = new();
    [SerializeField] Sprite[] allNormalSprites;
    [SerializeField] Sprite[] allSpecialSprites;

    // get functions
    public bool isTimerUp()
    {
        if (timer > 0)
            return false;
        return true;
    }

    public void getOrder()
    {
        string result = "";

        if (order[0] == DrinkIngredient.IngredientType.GreenTea)
            result += "green tea +";
        if (order[0] == DrinkIngredient.IngredientType.BlackTea)
            result += "black tea +";
        if (order[1] == DrinkIngredient.IngredientType.Milk)
            result += "milk +";
        if (order[1] == DrinkIngredient.IngredientType.Fruit)
            result += "fruit +";
        if (order[1] == DrinkIngredient.IngredientType.Plain)
            result += "plain +";
        if (order[2] == DrinkIngredient.IngredientType.Boba)
            result += " boba";
        if (order[2] == DrinkIngredient.IngredientType.Jelly)
            result += " jelly";
        if (order[2] == DrinkIngredient.IngredientType.NoTopping)
            result += " no topping";

        Debug.Log(result);
    }
    
    public void makeRandomOrder()
    {
        int r = Random.Range(0, 2);
        if (r == 0)
            order.Add(DrinkIngredient.IngredientType.GreenTea);
        else
            order.Add(DrinkIngredient.IngredientType.BlackTea);

        r = Random.Range(0, 3);
        if (r == 0)
            order.Add(DrinkIngredient.IngredientType.Milk);
        else if (r == 1)
            order.Add(DrinkIngredient.IngredientType.Fruit);
        else
            order.Add(DrinkIngredient.IngredientType.Plain);

        r = Random.Range(0, 3);
        if (r == 0)
            order.Add(DrinkIngredient.IngredientType.Boba);
        else if (r == 1)
            order.Add(DrinkIngredient.IngredientType.Jelly);
        else
            order.Add(DrinkIngredient.IngredientType.NoTopping);
    }

    public bool checkOrder(List<DrinkIngredient.IngredientType> order)
    {
        if (this.order.Count != order.Count)
        {
            return false;
        }

        int count = this.order.Count;
        for (int i = 0; i < count; i++)
        {
            if (this.order[i] != order[i])
                return false;
        }

        return true;
    }

    public bool isOrderComplete()
    {
        return orderComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position += Time.deltaTime * speed * Vector3.left;
        }
        
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            timer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Drink Container")
        {
            if (checkOrder(collision.gameObject.GetComponent<DrinkContainer>().getIngredients()))
            {
                orderComplete = true;
                Debug.Log("order complete");
            }
        }
    }
}
