using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Utils;

namespace TextRPG
{

    public enum AttackType
    {
        NormalAttack,
        BombAttack
    }

    public class AttackEventArgs: EventArgs
    {
        public AttackType attackType;
        public float damage;
        public bool success;
        public Character attacker;
        public Character victim;
        public AttackEventArgs(float damage, bool success, Character attacker, Character victim, AttackType attackType = AttackType.NormalAttack)
        {
            this.damage = damage;
            this.success = success;
            this.attacker = attacker;
            this.victim = victim;
            this.attackType = attackType;
        }
    }

    public abstract class Character
    {
        public float ATTACK_SUCCESSFUL_THRESHOLD = 1.5f;
        public float STRENGTH_MULTIPLIER = 1.4f;
        public float ARMOR_MULTILIER = 2f;
        public float MAX_HEALTH = 100f;

        public enum HealthState
        {
            FullHealth = 0,
            Injured = 1,
            Dead = 2
        }
        protected HealthState _healthState;
       
        public CharacterStats Stats { get; protected set; }
        public float HealthPoints { get; private set; }
        public string Name { get; private set; }

        public bool IsDead
        {
            get
            {
                if (_healthState == HealthState.Dead)
                    return true;
                return false;
            }
        }

        
        public event EventHandler DiedHappened;
        public event EventHandler<AttackEventArgs> AttackHappened;

        public Character(string name)
        {
            HealthPoints = MAX_HEALTH;
            Name = name;
        }

        public void ModifyHealth(float amount)
        {
            HealthPoints += amount;
            if(HealthPoints >= MAX_HEALTH)
            {
                _healthState = HealthState.FullHealth;
                HealthPoints = MAX_HEALTH;
            }
            else if(HealthPoints <= 0)
            {
                _healthState = HealthState.Dead;
                HealthPoints = 0;
            }
            else
            {
                _healthState = HealthState.Injured;
            }
            if (HealthPoints <= 0)
                Die();
        }

        public virtual void Attack(Character victim)
        {
            bool success = (Stats.dexerity / victim.Stats.dexerity + Stats.accuracy) > ATTACK_SUCCESSFUL_THRESHOLD;
            float damage = MathF.Max(Stats.strength * STRENGTH_MULTIPLIER - victim.Stats.armorClass * ARMOR_MULTILIER, 1f);

            if (!success)
                victim.ModifyHealth(-damage);

            AttackHappened?.Invoke(this, new AttackEventArgs(damage, success, this, victim));
        }

        protected void InvokeAttackHappened(object sender, AttackEventArgs attackEventArgs)
        {
            AttackHappened?.Invoke(sender, attackEventArgs);
        }

        public void Die()
        {
            OnDied();
        }

        private void OnDied()
        {
            EventHandler handler = DiedHappened;
            handler?.Invoke(this, EventArgs.Empty);
        }
    
        public string[] Summary()
        {
            string[] statSummary = Stats.Summary();
            string[] info = new string[statSummary.Length + 3];
            info[0] = string.Format("Name: {0}", Name);
            info[1] = string.Format("Health: ({0}/{1})", HealthPoints, MAX_HEALTH);

            for(int i=0; i < statSummary.Length; i++)
            {
                info[i + 2] = statSummary[i];
            }
            info[info.Length - 1] = _healthState.ToString();
            return info;
        }
    
    }
}
