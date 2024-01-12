using Newtonsoft.Json.Linq;

namespace Dota2GSI.Nodes
{
    /// <summary>
    /// Class representing building information.
    /// </summary>
    public class Building : Node
    {
        /// <summary>
        /// Building health.
        /// </summary>
        public readonly int Health;

        /// <summary>
        /// Maximum building health.
        /// </summary>
        public readonly int MaxHealth;

        internal Building(JObject parsed_data = null) : base(parsed_data)
        {
            Health = GetInt("health");
            MaxHealth = GetInt("max_health");
        }

        public override string ToString()
        {
            return $"[" +
                $"Health: {Health}, " +
                $"MaxHealth: {MaxHealth}" +
                $"]";
        }
    }
}