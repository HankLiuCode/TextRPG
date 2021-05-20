
namespace TextRPG
{
    public abstract class LivingEntity
    {
        public bool IsDead { get; protected set; }
        public abstract bool AttackSuccessful(float attackerDexterity, float attackerAccuracy);
        public abstract void TakeDamage(float strength);
        public abstract float TakeExperience();

        public abstract void PrintStatus();
    }
}
