using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour{

    public int health;
    int maxHealth;

    public HealthManager(int health){
        this.health = health;
        this.maxHealth = health;
    }

    public int getHealth(){
        return health;
    }

    public void setHealth(int health){
        this.health = health;
    }

    public void damage(int health){
        if(this.health - health < 0){
            this.health = 0;
        } else {
            this.health -= health;
        }
    }

    public void heal(int health){
        if(this.health + health > maxHealth){
            this.health = 100;
        } else {
            this.health += health;        
        }
    }
}
