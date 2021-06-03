using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Common;
using TextRPG.Utils;

namespace TextRPG
{
    public enum HealthState
    {
        FullHealth,
        Injured,
        Dead
    }
    public struct Stats
    {
        public readonly float armorClass;
        public readonly float strength;
        public readonly float dexerity;
        public readonly float accuracy;

        public Stats(float armorClass, float strength, float dexerity, float accuracy)
        {
            this.armorClass = armorClass;
            this.strength = strength;
            this.dexerity = dexerity;
            this.accuracy = accuracy;
        }

        public string[] Summary()
        {
            string[] info = new string[5];
            info[0] = "Stats:";
            info[1] = string.Format("ArmorClass: {0,-10}", armorClass);
            info[2] = string.Format("Strength:   {0,-10}", strength);
            info[3] = string.Format("Dexterity:  {0,-10}", dexerity);
            info[4] = string.Format("Accuracy:   {0,-10}", accuracy);
            return info;
        }
    }

    public class OnAttackEventArgs : EventArgs
    {
        public float damage;
        public bool success;
        public Character attacker;
        public Character victim;
        public OnAttackEventArgs(float damage, bool success, Character attacker, Character victim)
        {
            this.damage = damage;
            this.success = success;
            this.attacker = attacker;
            this.victim = victim;
        }
    }

    public class OnHealthModifiedEventArgs : EventArgs
    {
        public Character player;
        public float amount;
        public HealthState healthState;
        public OnHealthModifiedEventArgs(Character player, float amount, HealthState healthState)
        {
            this.player = player;
            this.amount = amount;
            this.healthState = healthState;
        }
    }

    public class Character : GameEntity
    {
        public static int MAX_HEALTH = 100;
        public float Health { get; private set; }
        public HealthState healthState;
        public Stats stats;

        public event EventHandler<OnAttackEventArgs> OnAttack;
        public event EventHandler<OnHealthModifiedEventArgs> OnHealthModified;

        public Character(string name, char symbol, Vector2 position, Stats stats) : base(name, symbol, position)
        {
            Health = 100f;
            this.stats = stats;
        }

        public void Attack(Character victim)
        {
            bool success = stats.dexerity + stats.accuracy > victim.stats.dexerity;
            float damage = success ? (stats.strength - victim.stats.armorClass) : 0;
            if (success)
                victim.ModifyHealth(-damage);

            if (OnAttack != null) OnAttack.Invoke(this, new OnAttackEventArgs(damage, success, this, victim));
        }

        public void ModifyHealth(float amount)
        {
            Health += amount;
            if(Health >= MAX_HEALTH)
            {
                healthState = HealthState.FullHealth;
                Health = MAX_HEALTH;
            }
            else if(Health <= 0)
            {
                healthState = HealthState.Dead;
                Health = 0;
            }
            else
            {
                healthState = HealthState.Injured;
            }
            if (Health <= 0)
                Die();

            if (OnHealthModified != null) OnHealthModified.Invoke(this, new OnHealthModifiedEventArgs(this, amount, healthState));
        }

        private void Die()
        {
            Destroy(this);
        }
    
    }
}
