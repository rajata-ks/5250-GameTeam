using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        public bool monstersTurn = false;

        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();


        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {

            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            _ = InitializeMapGrid();

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Populate the UI Map
            DrawMapGridInitialState();

            // Ask the Game engine to select who goes first
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            monstersTurn = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster;
            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();


            BindingContext = BattleEngineViewModel.Instance;
            MonsterListView.ItemsSource = BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList;
            UpdateCharacterMonsterUI();


            // Turn on Timer
            ConfigureTimerOn();
        }

        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        #region BattleMapMode

        /// <summary>
        /// Create the Initial Map Grid
        /// 
        /// All lcoations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        /// <summary>
        /// Draw the Map Grid
        /// Add the Players to the Map
        /// 
        /// Need to have Players in the Engine first, to then put on the Map
        /// 
        /// The Map Objects are all created with the map background image first
        /// 
        /// Then the actual characters are added to the map
        /// </summary>
        public void DrawMapGridInitialState()
        {
            // Create the Map in the Game Engine
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            _ = CreateMapGridObjects();

            _ = UpdateMapGrid();
            UpdateCharacterMonsterUI();

        }

        /// <summary>
        /// Walk the current grid
        /// check each cell to see if it matches the engine map
        /// check each cell to see if it matches the engine map
        /// Update only those that need change
        /// </summary>
        /// <returns></returns>
        public bool UpdateMapGrid()
        {
            var open = new PlayerInfoModel() { PlayerType = PlayerTypeEnum.openSpace };
            var empty = new PlayerInfoModel() { PlayerType = PlayerTypeEnum.Unknown };


            //set any open space to empty so that it can be re populated
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                if (data.Player.PlayerType == PlayerTypeEnum.openSpace)
                {
                    data.Player = empty;
                }
            }


            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                if (data.Player.PlayerType == PlayerTypeEnum.Character && BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum == BattleStateEnum.Battling)
                {
                    foreach (var openspace in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
                    {
                        if (BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.CalculateDistance(data, openspace) == 1
                            && BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.IsEmptySquare(openspace.Column, openspace.Row))
                        {
                            openspace.Player = open;
                        }
                    }
                }
            }

            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {

                // Use the ImageButton from the dictionary because that represents the player object
                var MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
                if (MapObject == null)
                {
                    return false;
                }

                var imageObject = (ImageButton)MapObject;

                // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
                //if (!imageObject.AutomationId.Equals(data.Player.Guid))
                //{
                    // The Image is different, so need to re-create the Image Object and add it to the Stack
                    // That way the correct monster is in the box.

                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    _ = MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = DetermineMapBackgroundColor(data);
                //}
            }

            return true;
        }

        public void UpdateCharacterMonsterUI()
        {
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Count <= 0)
            {
                return;
            }

            if (CharactersListView.ItemsSource == null)
            {
                return;
            }

            if (MonsterListView.ItemsSource == null)
            {
                return;
            }
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Count <= 0)
            {
                return;
            }
            
            //Update Characters
            List<object> characterList = new List<object>();
            foreach (var character in BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList)
            {
                if (character.MaxHealth > 0)
                {
                    character.HealthPercent = (float)((float)character.CurrentHealth / character.MaxHealth);
                    character.AbilityPercent = (float)((float)character.AbilityProgress / 3);
                    characterList.Add(character);
                }
            }
            CharactersListView.ItemsSource = characterList;

            //Update Monsters
            List<object> monsterList = new List<object>();
            foreach (var monster in BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList)
            {
                if (monster.MaxHealth > 0)
                {
                    monster.HealthPercent = (float)((float)monster.CurrentHealth / monster.MaxHealth);
                    //monster.Name = monster.CurrentHealth.ToString() + " " + monster.MaxHealth.ToString();
                    monsterList.Add(monster);

                }
            }
            MonsterListView.ItemsSource = monsterList;
        }
        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryFrameName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Frame", data.Row, data.Column);
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryStackName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Stack", data.Row, data.Column);
        }

        /// <summary>
        /// Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryImageButtonName(MapModelLocation data)
        {
            // Look up the Frame in the Dictionary
            return string.Format("MapR{0}C{1}ImageButton", data.Row, data.Column);
        }

        /// <summary>
        /// Populate the Map
        /// 
        /// For each map position in the Engine
        /// Create a grid object to hold the Stack for that grid cell.
        /// </summary>
        /// <returns></returns>
        public bool CreateMapGridObjects()
        {
            // Make a frame for each location on the map
            // Populate it with a new Frame Object that is unique
            // Then updating will be easier

            foreach (var location in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                var data = MakeMapGridBox(location);

                // Add the Box to the UI

                MapGrid.Children.Add(data, location.Column, location.Row);
            }

            // Set the Height for the MapGrid based on the number of rows * the height of the BattleMapFrame

            var height = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount * 50;

            BattleMapDisplay.MinimumHeightRequest = height;
            BattleMapDisplay.HeightRequest = height;

            return true;
        }

        /// <summary>
        /// Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetMapGridObject(string name)
        {
            _ = MapLocationObject.TryGetValue(name, out var data);
            return data;
        }

        /// <summary>
        /// Make the Game Map Frame 
        /// Place the Character or Monster on the frame
        /// If empty, place Empty
        /// </summary>
        /// <param name="mapLocationModel"></param>
        /// <returns></returns>
        public Frame MakeMapGridBox(MapModelLocation mapLocationModel)
        {
            if (mapLocationModel.Player == null)
            {
                mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
            }

            var PlayerImageButton = DetermineMapImageButton(mapLocationModel);

            var PlayerStack = new StackLayout
            {
                Padding = 0,
                Style = (Style)Application.Current.Resources["BattleMapImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = DetermineMapBackgroundColor(mapLocationModel),
                Children = {
                    PlayerImageButton
                },
            };

            _ = MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
            _ = MapGridObjectAddStack(PlayerStack, mapLocationModel);

            var MapFrame = new Frame
            {
                Style = (Style)Application.Current.Resources["BattleMapFrame"],
                Content = PlayerStack,
                AutomationId = GetDictionaryFrameName(mapLocationModel)
            };

            return MapFrame;
        }

        /// <summary>
        /// This add the ImageButton to the stack to kep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddImage(ImageButton data, MapModelLocation MapModel)
        {
            var name = GetDictionaryImageButtonName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);

            return true;
        }

        /// <summary>
        /// This adds the Stack into the Dictionary to keep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddStack(StackLayout data, MapModelLocation MapModel)
        {
            var name = GetDictionaryStackName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);
            return true;
        }

        /// <summary>
        /// Set the Image onto the map
        /// The Image represents the player
        /// 
        /// So a charcter is the character Image for that character
        /// 
        /// The Automation ID equals the guid for the player
        /// This makes it easier to identify when checking the map to update thigns
        /// 
        /// The button action is set per the type, so Characters events are differnt than monster events
        /// </summary>
        /// <param name="MapLocationModel"></param>
        /// <returns></returns>
        public ImageButton DetermineMapImageButton(MapModelLocation MapLocationModel)
        {
            var data = new ImageButton
            {
                Style = (Style)Application.Current.Resources["BattleMapPlayerSmallStyle"],
                Source = MapLocationModel.Player.ImageURI,

                // Store the guid to identify this button
                AutomationId = MapLocationModel.Player.Guid
            };

            switch (MapLocationModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    //dont have any function with character click. Always false.
                        data.IsEnabled = false;
                    
                    data.Clicked += (sender, args) => SetSelectedCharacter(MapLocationModel);
                    break;
                case PlayerTypeEnum.Monster:
                    data.IsEnabled = !monstersTurn;

                    data.Clicked += (sender, args) => SetSelectedMonster(MapLocationModel);
                    break;
                case PlayerTypeEnum.openSpace:
                    data.IsEnabled = !monstersTurn;

                    data.Clicked += (sender, args) => SetSelectedEmpty(MapLocationModel);
                    data.BackgroundColor = Color.Green;
                    // Use the blank cell
                    data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    data.IsEnabled = false;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Set the Background color for the tile.
        /// Monsters and Characters have different colors
        /// Empty cells are transparent
        /// </summary>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public Color DetermineMapBackgroundColor(MapModelLocation MapModel)
        {
            string BattleMapBackgroundColor;
            switch (MapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
                case PlayerTypeEnum.Monster:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
                case PlayerTypeEnum.openSpace:
                    BattleMapBackgroundColor = "BattleMapDeathColor";
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }

            var result = (Color)Application.Current.Resources[BattleMapBackgroundColor];
            return result;
        }

        #region MapEvents
        /// <summary>
        /// Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {

            ClearMessages();
            var action = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Move;

            NextAction(action, true, true);
            var curr = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            var currentMover = curr;
            var curLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(currentMover);
            var location = curLocation;
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(currentMover);

            if (currentMover.PlayerType == PlayerTypeEnum.Character)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MovePlayerOnMap(location, data);
                location = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(currentMover);
                if (location == null)
                {
                    location = new MapModelLocation() { };
                }
                BattleMessages.Text = currentMover.Name + " moved to Row " + location.Row.ToString() + " Column " + location.Column.ToString();
                UpdateCharacterMonsterUI();
            }

            monstersTurn = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster;

            var showNextTurnButton = !(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster);
            hideTurnButtons(showNextTurnButton);

            _ = UpdateMapGrid();

            return true;
        }

        /// <summary>
        /// Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedMonster(MapModelLocation data)
        {
            ClearMessages();
            var action = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            NextAction(action);

            var attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.AttackerName;
            var message = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;


            BattleMessages.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;
            monstersTurn = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster;

            var showNextTurnButton = !(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster);
            hideTurnButtons(showNextTurnButton);

            _ = UpdateMapGrid();

            return true;

        }

        /// <summary>
        /// Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            ClearMessages();
            BattleMessages.IsVisible = true;
            BattleMessages.Text = data.Player.Name;
            return true;
        }
        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Draw the Map
            _ = UpdateMapGrid();

        }


        /// <summary>
        /// Move Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveButton_Clicked(object sender, EventArgs e)
        {
            //currently do nothing
        }

        /// <summary>
        /// Special Ability Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpecialButton_Clicked(object sender, EventArgs e)
        {
            ClearMessages();
            var action = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Ability;

            NextAction(action);

            var attacker = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.AttackerName;
            var message = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;


            BattleMessages.Text = message;
            monstersTurn = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster;
            var showNextTurnButton = !(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster);
            hideTurnButtons(showNextTurnButton);
            
            _ = UpdateMapGrid();
            UpdateCharacterMonsterUI();

        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public bool NextAction(ActionEnum action = ActionEnum.Unknown, bool setAttackersDefenders = true, bool auto = false)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            if (setAttackersDefenders == true)
            {
                SetAttackerAndDefender(auto);
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                action = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction;
            }

            // Hold the current state
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = action;
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {

                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return false;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender(bool auto = false)
        {
            var re = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            var before = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack
                    if (auto == true)
                    {
                        BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Move;
                    }
                    // for now just auto selecting
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage != "")
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleMessages.Text, BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage);
            }
            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleMessages.Text, BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage);
            }

            //htmlSource.Html = BattleEngineViewModel.Instance.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();
            DrawMapGridInitialState();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            _ = UpdateMapGrid();
            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            Special.IsVisible = false;
            //Rest.IsVisible = false;
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            // BattlePlayerInfomationBox.IsVisible = false;
        }

        public void hideTurnButtons(bool hide)
        {
            NextTurn.IsVisible = !hide;
            Special.IsVisible = hide;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartBattleButton.IsVisible = true;
                    break;

                case BattleStateEnum.NewRound:
                    _ = UpdateMapGrid();
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    hideTurnButtons(!monstersTurn);
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();

                    // Show the Game Over Display
                    GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    //BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    hideTurnButtons(!monstersTurn);
                    //Rest.IsVisible = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    break;
            }
        }

        #region TimerExample
        // Check Timer on or off
        public bool TimerStateOn;

        // The time interval for checking sync state in miliseconds
        public int TimerDuration = 100000;   // 5 minutes

        // Holds the time when the game needs to end
        public DateTime TimerEndTime;

        // Is the Timer Enabed
        public bool IsEnableTimer = false;

        /// <summary>
        /// Turn on the Timer
        /// 
        /// Fire the Start Timer every duration
        /// Fire the Start Timer every duration
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ConfigureTimerOn()
        {
            TimerStateOn = true;
            IsEnableTimer = false;
            TimerDuration = 99999;
            TimerEndTime = DateTime.Now.AddMilliseconds(TimerDuration);
            StartTimerEvent();
            return true;
        }

        /// <summary>
        /// Turn on the Timer
        /// 
        /// Fire the Start Timer every duration
        /// 
        /// </summary>
        /// <returns></returns>
        public bool StartTimerEvent()
        {

            // Only enable the Timer if it is enabled
            if (IsEnableTimer)
            {

                _ = TimerEvent();
            }

            return true;
        }

        /// <summary>
        /// The Timer Event
        /// </summary>
        /// <returns></returns>
        public bool TimerEvent()
        {
            // Start a Timer that fires every duraction 
            Device.StartTimer(TimeSpan.FromMilliseconds(TimerDuration), () =>
            {
                TimerStateOn = true;

                return TimeRemaining();
            });

            // Return from the method
            return true;
        }

        /// <summary>
        /// Checks to see if Time is Remaining or not
        /// </summary>
        /// <returns></returns>
        public bool TimeRemaining()
        {
            // Time up
            if (DateTime.Now > TimerEndTime)
            {
                GameUIDisplay.IsVisible = false;
                TimeUpDisplay.IsVisible = true;
                TimerStateOn = false;
                _ = StopTimerEvent();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Turn of the Timer
        /// </summary>
        /// <returns></returns>
        public bool StopTimerEvent()
        {
            TimerStateOn = false;

            return true;
        }

        #endregion TimerExample

        public void NextTurn_Clicked(object sender, EventArgs e)
        {
            ClearMessages();

            NextAction(ActionEnum.Unknown);
            var showNextTurnButton = !(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster);
            monstersTurn = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn().PlayerType == PlayerTypeEnum.Monster;
            hideTurnButtons(showNextTurnButton);
            UpdateMapGrid();
            UpdateCharacterMonsterUI();
        }
    }
}


