using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Books",
                    Description = "Big and Heavy",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Pen",
                    Description = "Can be used for stabbing",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 3,
                    Value = 1,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Duster",
                    Description = "Find a better weapon",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 6,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "School Bag",
                    Description = "Can be used as armor",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 5,
                    Value = 5,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Shoes",
                    Description = "Run Fast!",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 4,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Lunch Box",
                    Description = "Cute lunch ruined",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Desk",
                    Description = "Tons of Damage",
                    ImageURI = "pen.png",
                    Range = 1,
                    Damage = 10,
                    Value = 10,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Chair",
                    Description = "Great damage",
                    ImageURI = "pen.png",
                    Range = 3,
                    Damage = 6,
                    Value = 5,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Chalk",
                    Description = "Useless as hell",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 1,
                    Value = 1,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Nerd",
                    Description = " A geek girl",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "character__nerd.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Athlete",
                    Description = "Not a runner? Now you are..",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "character_athlete.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Goth",
                    Description = "Don't get in my way",
                    Level = 1,
                    MaxHealth = 8,
                    ImageURI = "character_athlete.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Skater",
                    Description = "I still got a lot of shoes left",
                    Level = 4,
                    MaxHealth = 38,
                    ImageURI = "character_athlete.png"
                },

                new CharacterModel {
                    Name = "Procastinator",
                    Description = "Come and get me",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "character_athlete.png"
                },

                new CharacterModel {
                    Name = "Class Clown",
                    Description = "You dont fear me ? then you'll die braver than most!",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "character_athlete.png"
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Zombified Math Teacher",
                    Description = "Too smart for you.",
                    ImageURI = "zombieTeacher.png",
                },

                new MonsterModel {
                    Name = "Zombified Lunch Lady",
                    Description = "Don't skip lunch, or else.",
                    ImageURI = "zombieTeacher.png",
                },

                new MonsterModel {
                    Name = "Zombified Janitor",
                    Description = "Cleans up the defeated.",
                    ImageURI = "zombieTeacher.png",
                },

                new MonsterModel {
                    Name = "Zombified Security Guard",
                    Description = "Make sure you have your hall pass...",
                    ImageURI = "zombieTeacher.png",
                },

                new MonsterModel {
                    Name = "Ghoul Vice-Principle",
                    Description = "Second in command. Just as dangerous.",
                    ImageURI = "zombieTeacher.png",
                },

                new MonsterModel {
                    Name = "Ghoul Principle",
                    Description = "Rules the school with an iron fist.",
                    ImageURI = "zombieTeacher.png",
                },
            };

            return datalist;
        }
    }
}