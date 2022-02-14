using Game.Models;
using System.Collections.Generic;

namespace Game.Helpers
{
    /// <summary>
    /// Helper for ability functionality
    /// </summary>
    public static class AbilityHelper
    {
        public static bool AddAbilitiesBasedOnJob(Dictionary<AbilityEnum, int> AbilityTracker, CharacterJobEnum job, int level)
        {
            if (AbilityTracker == null)
            {
                return false;
            }

            if (level < 1)
            {
                return false;
            }

            switch (job)
            {
                case CharacterJobEnum.Cleric:

                    foreach (var item in AbilityEnumHelper.GetListCleric)
                    {
                        if (AbilityTracker.ContainsKey(AbilityEnumHelper.ConvertStringToEnum(item)))
                        {
                            AbilityTracker[AbilityEnumHelper.ConvertStringToEnum(item)] += level;
                        }
                        else
                        {
                            AbilityTracker.Add(AbilityEnumHelper.ConvertStringToEnum(item), level);
                        }
                    }
                    break;

                case CharacterJobEnum.Fighter:
                    foreach (var item in AbilityEnumHelper.GetListFighter)
                    {
                        if (AbilityTracker.ContainsKey(AbilityEnumHelper.ConvertStringToEnum(item)))
                        {
                            AbilityTracker[AbilityEnumHelper.ConvertStringToEnum(item)] += level;
                        }
                        else
                        {
                            AbilityTracker.Add(AbilityEnumHelper.ConvertStringToEnum(item), level);
                        }
                    }
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    foreach (var item in AbilityEnumHelper.GetListOthers)
                    {
                        if (AbilityTracker.ContainsKey(AbilityEnumHelper.ConvertStringToEnum(item)))
                        {
                            AbilityTracker[AbilityEnumHelper.ConvertStringToEnum(item)] += level;
                        }
                        else
                        {
                            AbilityTracker.Add(AbilityEnumHelper.ConvertStringToEnum(item), level);
                        }
                    }
                    break;
            }

            return true;
        }
    }
}