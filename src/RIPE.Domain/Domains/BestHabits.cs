using System.Collections.Generic;

namespace RIPE.Domain.Domains
{
    public class BestHabits
    {
        public BestHabits(List<string> habits)
        {
            Habits = habits;
        }

        public List<string> Habits { get; set; }
    }
}
