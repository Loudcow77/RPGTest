using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    // means any class can get this value but we can only set it here
    public int currentHealth { get; private set; } 

    public Stat damage;
    public Stat armour;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            takeDamage(10);
        }
    }

    public void takeDamage(int damage)
    {
        // weakens damage based on armour value
        damage -= armour.getValue();
        // makes it so damage doesnt go negative
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        // subtract current health by damage modifier
        currentHealth -= damage;
        // Do something if players health hits 0
        if(currentHealth <= 0)
        {
            die();
        }
    }

    public virtual void die()
    {

    }
}
