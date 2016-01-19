using System.Collections.Generic;
using Assets.Scripts.Entities;
using Seniors.Skills.Projectiles;
using UnityEngine;

namespace Seniors.Skills.Andrew
{
    public class AndrewAttack : Skill
    {
        public List<Entity> enemies = new List<Entity>(); 
        public Entity target = null;
        public float kickRange = 1f;
        public Bomb KickBomb;
        public override void ActivateDown()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) return;

            Vector3 directionToTarget = target.transform.position - transform.position;
            if (directionToTarget.magnitude > 1f)
                anim.SetTrigger("Attack");
            else
                anim.SetTrigger("SecAttack");
        }

        public override void Update()
        {
            if (hc.CanMove && !sc.IsBusy)
            {
                // if there is an enemy in range, turn to look at it
                if (enemies.Count > 0)
                {
                    target = FindNearestTarget();
                    Vector3 directionToTarget = target.transform.position - transform.position;
                    hc.RotateBasedOnMovement = false;
                    hero.transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(directionToTarget),
                        Time.deltaTime*stats.RotationSpeedBase);
                }
                else
                {
                    hc.RotateBasedOnMovement = true;
                }
            }
        }

        // from the enemies list, find the closest target to look at
        private Entity FindNearestTarget()
        {
            float closestDist = Mathf.Infinity;
            Entity closestEntitiy = null;
            for (int i = 0; i < enemies.Count; i++)
            {
                float dist = (transform.position - enemies[i].transform.position).sqrMagnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestEntitiy = enemies[i];
                }
            }
            return closestEntitiy;
        }

        //When an enemy comes into range, add it into the enemies list
        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity != null)
            {
                if ((hero.enemyFactions & entity.currentFaction) == entity.currentFaction)
                {
                    if (!enemies.Contains(entity))
                        enemies.Add(entity);
                }
            }
        }

        // When an enemy exits the trigger, remove it from the list
        void OnTriggerExit(Collider other)
        {
            Entity entitiy = other.gameObject.GetComponent<Entity>();

            if (entitiy != null)
            {
                if ((hero.enemyFactions & entitiy.currentFaction) == entitiy.currentFaction)
                    enemies.Remove(entitiy);
            }
        }

        public override void RaiseEvent(string eventName)
        {
            Debug.Log(eventName);
            switch (eventName)
            {
                case "Attack_Shoot":
                    ShootProjectile();
                    break;
                case "Attack_Kick":
                    Bomb bomb = Instantiate(KickBomb, hero.transform.position + 1 * hero.transform.forward, Quaternion.identity) as Bomb;
                    bomb.owner = hero;
                    break;
            }
        }

    }
}