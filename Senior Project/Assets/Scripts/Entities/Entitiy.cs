﻿using Senior.Components;
using Senior.Inputs;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    [RequireComponent(typeof(HeroController))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Stats))]
    public abstract class Entitiy : MonoBehaviour
    {

        public Stats StatsComponent { get; set; }                   // Contains the stats of the character
        private Rigidbody rb;

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            StatsComponent = GetComponent<Stats>();
        }


        // Called when the entity dies
        public virtual void Die()
        {
            
        }

        // Called when the entity gets damaged
        public virtual void GetDamaged(float damage)
        {
            
        }

        // Similar to the damaged method, but gets it's own method for ease of use.
        public virtual void GetHealed(float damage)
        {
            
        }

        // Called when you want the entity to get fully healed.
        public virtual void FullHeal()
        {
            StatsComponent.HealthCurrent = StatsComponent.mM;
            Debug.Log(Name + " Health: " + Health.ToString());
        }


    }
}