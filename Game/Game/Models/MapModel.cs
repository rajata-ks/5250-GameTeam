using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// Represent the Map
    /// 
    /// The Cordinates
    /// What is at that location
    /// 
    /// </summary>
    public class MapModel
    {
        // The X axies Size
        public int MapXAxiesCount = 6;

        // The Y axies Size
        public int MapYAxiesCount = 6;

        // The Map Locations
        public MapModelLocation[,] MapGridLocation;

        public PlayerInfoModel EmptySquare = new PlayerInfoModel { PlayerType = PlayerTypeEnum.Unknown, ImageURI = "mapcell.png" };

        public MapModel()
        {
            // Create the Map
            MapGridLocation = new MapModelLocation[MapXAxiesCount, MapYAxiesCount];

            _ = ClearMapGrid();
        }

        /// <summary>
        /// Create an Empty Map
        /// </summary>
        /// <returns></returns>
        public bool ClearMapGrid()
        {
            //Populate Map with Empty Values
            for (var x = 0; x < MapXAxiesCount; x++)
            {
                for (var y = 0; y < MapYAxiesCount; y++)
                {
                    // Populate the entire map with blank
                    MapGridLocation[x, y] = new MapModelLocation { Row = y, Column = x, Player = EmptySquare };
                }
            }
            return true;
        }

        /// <summary>
        /// Initialize the Data Structure
        /// Add Characters and Monsters to the Map
        /// </summary>
        /// <param name="PlayerList"></param>
        /// <returns></returns>
        public bool PopulateMapModel(List<PlayerInfoModel> PlayerList)
        {
            _ = ClearMapGrid();

            var x = 0;
            var y = 0;
            foreach (var data in PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character))
            {
                MapGridLocation[x, y].Player = data;

                // If too many to fit on a row, start at the next row
                x++;
                if (x >= MapXAxiesCount)
                {
                    x = 0;
                    y++;
                }
            }

            x = 0;
            y = MapYAxiesCount - 1;
            foreach (var data in PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster))
            {
                MapGridLocation[x, y].Player = data;

                // If too many to fit on a row, start at the next row
                x++;
                if (x >= MapXAxiesCount)
                {
                    x = 0;
                    y--;
                }
            }

            return true;
        }

        /// <summary>
        /// Changes the Row and Column for the Player
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool MovePlayerOnMap(MapModelLocation data, MapModelLocation target)
        {
            if (data == null)
            {
                return false;
            }

            if (target == null)
            {
                return false;
            }

            if (target.Column < 0)
            {
                return false;
            }

            if (target.Row < 0)
            {
                return false;
            }

            if (target.Column >= MapXAxiesCount)
            {
                return false;
            }

            if (target.Row >= MapYAxiesCount)
            {
                return false;
            }

            MapGridLocation[target.Column, target.Row].Player = data.Player;

            // Clear out the old location
            MapGridLocation[data.Column, data.Row].Player = EmptySquare;

            return true;
        }

        /// <summary>
        /// Remove the Player from the Map
        /// Replaces with Empty Square
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RemovePlayerFromMap(PlayerInfoModel data)
        {
            if (data == null)
            {
                return false;
            }

            for (var x = 0; x < MapXAxiesCount; x++)
            {
                for (var y = 0; y < MapYAxiesCount; y++)
                {
                    if (MapGridLocation[x, y].Player.Guid.Equals(data.Guid))
                    {
                        MapGridLocation[x, y] = new MapModelLocation { Column = x, Row = y, Player = EmptySquare };
                        return true;
                    }
                }
            }

            // Not found
            return false;
        }

        /// <summary>
        /// Clear all Locations of the Selected Bool
        /// 
        /// Mike does not use this in the example battle grammar
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ClearSelection()
        {
            foreach (var data in MapGridLocation)
            {
                data.IsSelectedTarget = false;
            }

            return true;
        }

        /// <summary>
        /// Find the Player on the map
        /// Return their information
        /// 
        /// If they don't exist, return null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MapModelLocation GetLocationForPlayer(PlayerInfoModel player)
        {
            if (player == null)
            {
                return null;
            }

            foreach (var data in MapGridLocation)
            {
                if (data.Player.Guid.Equals(player.Guid))
                {
                    return data;
                }
            }

            return null;
        }

        /// <summary>
        /// Return who is at the location
        /// Could be Character, Monster or Empty
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PlayerInfoModel GetPlayerAtLocation(int x, int y)
        {
            return MapGridLocation[x, y].Player;
        }

        /// <summary>
        /// Is the location empty?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsEmptySquare(int x, int y)
        {
            var player = MapGridLocation[x, y].Player;

            // Unknown is Empty
            if (player.PlayerType == PlayerTypeEnum.Unknown)
            {
                return true;
            }

            // Occupied
            return false;
        }

        /// <summary>
        /// Return all Empty Locations on the map
        /// </summary>
        /// <returns></returns>
        public List<MapModelLocation> GetEmptyLocations()
        {
            var Result = new List<MapModelLocation>();

            foreach (var data in MapGridLocation)
            {
                if (data.Player.PlayerType == PlayerTypeEnum.Unknown)
                {
                    Result.Add(data);
                }
            }

            return Result;
        }

        /// <summary>
        /// Walk the Map and Find the Location that is close to the target
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public MapModelLocation ReturnClosestEmptyLocation(MapModelLocation Target)
        {
            MapModelLocation Result = null;

            var LowestDistance = int.MaxValue;

            foreach (var data in GetEmptyLocations())
            {
                var distance = CalculateDistance(data, Target);
                if (distance < LowestDistance)
                {
                    Result = data;
                    LowestDistance = distance;
                }
            }

            return Result;
        }

        /// <summary>
        /// Walk the Map and Find the Location within the attacker's  speed range (where distance is less than or equal to speed )
        /// max an attacker can move is eqaul to speed or less
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="nearLocation"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public MapModelLocation ReturnClosest(MapModelLocation Attacker, int speed, List<MapModelLocation> nearLocation)
        {
            MapModelLocation Result = null;
            foreach (var data in nearLocation)
            {
                var distance = CalculateDistance(data, Attacker);
                if (distance <= speed)
                {
                    Result = data;
                    return Result;
                }
            }
            return Result;
        }


        public SortedDictionary<int, List<MapModelLocation>> calculateDistances(MapModelLocation Target)
        {
            SortedDictionary<int, List<MapModelLocation>> distanceMap = new SortedDictionary<int, List<MapModelLocation>>(new AscendingComparer<int>());
            foreach (var data in GetEmptyLocations())
            {
                var distance = CalculateDistance(data, Target);
                if(distanceMap.ContainsKey(distance))
                {
                    List<MapModelLocation> listOfLocations = distanceMap[distance];
                    listOfLocations.Add(data);
                    distanceMap.Remove(distance);
                    distanceMap.Add(distance, listOfLocations);
                } else
                {
                    List<MapModelLocation> listOfLocations = new List<MapModelLocation>();
                    listOfLocations.Add(data);
                    distanceMap.Add(distance, listOfLocations);
                }
            }
            return distanceMap;
        }

        class AscendingComparer<T> : IComparer<T> where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return x.CompareTo(y);
            }
        }
        public int distanceToMonster(MapModelLocation character, MapModelLocation emptyLocation)
        {
            return CalculateDistance(character, emptyLocation);
        }

        /// <summary>
        /// See if the Attacker is next to the Defender by the distance of Range
        /// 
        /// If either the X or Y distance is less than or equal the range, then they can hit
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Defender"></param>
        /// <returns></returns>
        public bool IsTargetInRange(PlayerInfoModel Attacker, PlayerInfoModel Defender)
        {
            var locationAttacker = GetLocationForPlayer(Attacker);
            var locationDefender = GetLocationForPlayer(Defender);

            if (locationAttacker == null)
            {
                return false;
            }

            if (locationDefender == null)
            {
                return false;
            }

            // Get X distance in absolute value
            var distance = Math.Abs(CalculateDistance(locationAttacker, locationDefender));

            var AttackerRange = Attacker.GetRange();

            // Can Reach on X?
            if (distance <= AttackerRange)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculate distance between two map locations
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int CalculateDistance(MapModelLocation start, MapModelLocation end)
        {
            if (start == null)
            {
                return int.MaxValue;
            }

            if (end == null)
            {
                return int.MaxValue;
            }

            return Distance(start.Column, start.Row, end.Column, end.Row);
        }

        /// <summary>
        /// Calculate Distance between locations
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public int Distance(int x1, int y1, int x2, int y2)
        {
            return ((int)Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        }
    }
}