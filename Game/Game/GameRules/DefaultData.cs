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
                    Name = "Pen",
                    Description = "Good for stabbing",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Books",
                    Description = "Big and Heavy",
                    ImageURI = "item_book.PNG",
                    Range = 0,
                    Damage = 8,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Duster",
                    Description = "Sweep em out",
                    ImageURI = "item_duster.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "School Bag",
                    Description = "A useful weapon",
                    ImageURI = "item_bag_rough.png",
                    Range = 0,
                    Damage = 12,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Shoes",
                    Description = "Run fast",
                    ImageURI = "item_shoe_koradupe_filler.png",
                    Range = 0,
                    Damage = 4,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Lunch Box",
                    Description = "Handy and a weapon",
                    ImageURI = "item_lunchbag_rough.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Desk",
                    Description = "How did you pick that up?",
                    ImageURI = "item_desk_fillersketch.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Chair",
                    Description = "Please don't throw that!",
                    ImageURI = "item_chair_fillersketch.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                 new ItemModel {
                    Name = "Water Bottle",
                    Description = "Feeling Good",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 6,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Chalk",
                    Description = "Useless as hell",
                    ImageURI = "item_chalk_rough.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Glasses",
                    Description = "Really good for seeing",
                    ImageURI = "pen.png",
                    Range = 10,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Yard Stick",
                    Description = "Great for long distance",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Binder",
                    Description = "Useless like the chalk",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Refrigerator",
                    Description = "You are a god",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Microphone",
                    Description = "Scream out your lungs",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Pack of Gum",
                    Description = "Something to chew on",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Pencil",
                    Description = "Useful now",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Binder Paper",
                    Description = "What can you do with this?",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Energy Drink",
                    Description = "Go Fast",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Hat",
                    Description = "Blocks out the sun",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Sun Glasses",
                    Description = "Can't see anything",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Flag Pole",
                    Description = "Best weapon in game",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Fan",
                    Description = "Cool",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Gym Shirt",
                    Description = "Smelly",
                    ImageURI = "pen.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},
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
                    Job = CharacterJobEnum.Nerd,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = " A geek girl",
                    Level = 1,
                    MaxHealth = 5,
                    Speed=2,
                    ImageURI = "character__nerd.png",
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Athlete",
                    Job = CharacterJobEnum.Athlete,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = "Not a runner? Now you are..",
                    Level = 1,
                    MaxHealth = 5,
                    Speed=2,
                    ImageURI = "character_athlete.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Goth",
                    Job = CharacterJobEnum.Goth,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = "Don't get in my way",
                    Level = 1,
                    MaxHealth = 8,
                     Speed=3,
                    ImageURI = "goth.png",
                    Head = HeadString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Skater",
                    Job = CharacterJobEnum.Skater,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = "I still got a lot of shoes left",
                    Level = 4,
                    Speed=1,
                    MaxHealth = 38,
                    ImageURI = "skater.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Procastinator",
                    Job = CharacterJobEnum.Procrastinator,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = "Come and get me",
                     Speed=5,
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "procrastinator.png",
                     Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Class Clown",
                    Job = CharacterJobEnum.ClassClown,
                    PlayerType = PlayerTypeEnum.Character,
                    Description = "You dont fear me ? then you'll die braver than most!",
                    Level = 5,
                    Speed=6,
                    MaxHealth = 43,
                    ImageURI = "classclown.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
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