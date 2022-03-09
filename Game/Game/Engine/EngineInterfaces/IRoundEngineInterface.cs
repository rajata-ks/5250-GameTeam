using System.Collections.Generic;

using Game.Models;

namespace Game.Engine.EngineInterfaces
{
    public interface IRoundEngineInterface
    {
        ITurnEngineInterface Turn { get; set; }

        bool ClearLists();
        bool NewRound();
        int AddMonstersToRound();
        bool EndRound();
        bool PickupItemsForAllCharacters();
        bool PickupDroppedItemsForAllCharacters();
        bool SetCurrentAttacker(PlayerInfoModel player);
        bool SetCurrentDefender(PlayerInfoModel player);

        RoundEnum RoundNextTurn();
        PlayerInfoModel GetNextPlayerTurn();
        List<PlayerInfoModel> RemoveDeadPlayersFromList();
        List<PlayerInfoModel> OrderPlayerListByTurnOrder();
        List<PlayerInfoModel> MakePlayerList();
        PlayerInfoModel GetNextPlayerInList();
        bool PickupItemsFromPool(PlayerInfoModel character);
        bool PickupItemsFromDroppedItems(PlayerInfoModel character);
        bool GetItemFromPoolIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation);
        bool GetItemFromDroppedListIfBetter(PlayerInfoModel character, ItemLocationEnum setLocation);
        ItemModel SwapCharacterItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem);
        ItemModel SwapCharacterItemWithDroppedItem(PlayerInfoModel character, ItemLocationEnum setLocation, ItemModel PoolItem);
        bool RemoveCharacterBuffs();
        List<PlayerInfoModel> PlayerList();
    }
}
