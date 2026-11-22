using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForceDestroyOldProjectilesFlagScript : MonoBehaviour
{
    public delegate void DestroyOldProjectilesFlag();
    List<GameObject> listOfProjectilesThatAreInTheScene = new List<GameObject>();
    
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //we have to get a list of every single projectile in the scene and erase them ourselves
        //I'm so so sorry for anyone who is seeing this. I'm well aware that making all the GameObjects Destroy is worse than taking an already fired bullet and returning it to the enemy script
        //I could add that! I don't feel like it at the moment
        
    }
    private void OnActivateThisFlag()
    {
        
        //_TEMP2();
        //for (int i = 0; i <= GameObject.FindGameObjectsWithTag("ProjectileTag").Length; ++i) //make sure not to miss by one!
        //{
        //i++ means it begins the loop with adding
        //++i means that it ends the loop with adding
        //imagine 10 members
        //begins at 0, so it begins the loop by calling 1?
        //    if (i == 0)
        //    {
        //        Debug.Log("My assumption was true! it begins by calling 0!");
        //    }
        // }
        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("ProjectileTag"))
        {
            listOfProjectilesThatAreInTheScene.Add(projectile); //this is bad
            
        }
        //instead, each projectile is going to update forceDestroyFlag with itself, and then it will just flush this list
        //I don't have time to do that! Plus it would make coupling weird
        foreach (GameObject projectile in listOfProjectilesThatAreInTheScene)
        {
            //why am I even doing the previous step
            Destroy(projectile);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Destroying all projectiles...");
        OnActivateThisFlag();

        //empty for now
    }
}
