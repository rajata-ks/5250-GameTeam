namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Fighters hit hard and have fight abilities
        Fighter = 10,

        // Clerics defend well and have buff abilities
        Cleric = 12,

        Nerd = 14,

        Athlete = 16,

        Goth = 18,

        Skater = 20,

        Procrastinator = 22,

        ClassClown = 24,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a string for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default string
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Cleric";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}