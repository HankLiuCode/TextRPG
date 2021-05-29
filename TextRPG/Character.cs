using System;
using System.Collections.Generic;
using System.Text;
using TextRPG.Utils;

namespace TextRPG
{

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
       
        protected Stats _stats;
        protected float _healthPoints;

        public string Name { get; private set; }
        public List<string> Actions { get; private set; }

        public bool IsDead
        {
            get
            {
                if (_healthState == HealthState.Dead)
                    return true;
                return false;
            }
        }
        public event EventHandler Died;

        public Character(string name)
        {
            Actions = new List<string>();
            _healthPoints = MAX_HEALTH;
            Name = name;
        }

        public void TakeDamage(float damage)
        {
            if (_healthState == HealthState.Dead)
            {
                Actions.Add(string.Format("{0} is already dead", Name));
                return;
            }
            _healthPoints -= damage;
            _healthState = HealthState.Injured;
            Actions.Add(string.Format("{0} Took: {1} Damage", Name, damage));

            if (_healthPoints <= 0)
                Die();
            Actions.Add(string.Format("Health: ({0}/{1}) ", _healthPoints, MAX_HEALTH));
        }

        public void TakeDamageWithStats(Stats attackerStats)
        {
            if (_healthState == HealthState.Dead)
            {
                Actions.Add(string.Format("{0} is already dead", Name));
                return;
            }

            bool canTakeDamage = (attackerStats.dexerity / _stats.dexerity + attackerStats.accuracy) > ATTACK_SUCCESSFUL_THRESHOLD;
            float damage = MathF.Max(attackerStats.strength * STRENGTH_MULTIPLIER - attackerStats.armorClass * ARMOR_MULTILIER, 1f);
            if (canTakeDamage)
            {
                TakeDamage(damage);
            }
            else
            {
                Actions.Add(string.Format("Missed!"));
            }
        }

        public virtual void Attack(Character character)
        {
            if (_healthState == HealthState.Dead)
                return;

            character.TakeDamageWithStats(_stats);
            Actions.Add(string.Format("{0} Attacked {1}", Name, character.Name));
        }

        public string[] GetActions(bool clear)
        {
            string[] actions = Actions.ToArray();
            if (clear)
                Actions.Clear();
            return actions;
        }

        public void Die()
        {
            _healthState = HealthState.Dead;
            _healthPoints = 0;
            OnDied();
            Actions.Add(string.Format("{0} is Dead", Name));
        }

        private void OnDied()
        {
            EventHandler handler = Died;
            handler?.Invoke(this, EventArgs.Empty);
        }
    
        public string[] Summary()
        {
            string[] statSummary = _stats.Summary();
            string[] info = new string[statSummary.Length + 3];
            info[0] = string.Format("Name: {0}", Name);
            info[1] = string.Format("Health: ({0}/{1})", _healthPoints, MAX_HEALTH);

            for(int i=0; i < statSummary.Length; i++)
            {
                info[i + 2] = statSummary[i];
            }
            info[info.Length - 1] = _healthState.ToString();
            return info;
        }
    
    }
}
