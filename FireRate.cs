using UnityEngine;

public class FireRate : MonoBehaviour
{
    //The interval you want your player to be able to fire.
    private float _rate;

    //The actual time the player will be able to fire.
    private float _nextFire;

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        //If the player is pressing fire AND The current time is > than when I want them to fire
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            //If the player fired, reset the NextFire time to a new point in the future.
            _nextFire = Time.time + _rate;

            //Weapon firing logic goes here.            
            Debug.Log("Firing once every 1s");
        }
    }

}
