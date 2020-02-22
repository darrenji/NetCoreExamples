using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.SimpleCQRS.Lib
{
    public interface IReadModelFacade
    {
        IEnumerable<InventoryItemListDto> GetInventoryItems();
        InventoryItemDetailDto GetInventoryItemDetails(Guid id);
    }

    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return BullShitDatabase.list;
        }

        public InventoryItemDetailDto GetInventoryItemDetails(Guid id)
        {
            return BullShitDatabase.details[id];
        }
    }

    public class InventoryItemDetailDto
    {
        public Guid Id;
        public string Name;
        public int CurrentCount;
        public int Version;

            public InventoryItemDetailDto(Guid id, string name, int currentCount, int version)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
            Version = version;
        }
    }

    public class InventoryItemListDto
    {
        public Guid Id;
        public string Name;

        public InventoryItemListDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public static class BullShitDatabase
    {
        public static Dictionary<Guid, InventoryItemDetailDto> details = new Dictionary<Guid, InventoryItemDetailDto>();
        public static List<InventoryItemListDto> list = new List<InventoryItemListDto>();
    }

    public class InventoryListView : Handles<InventoryItemCreated>, Handles<InventoryItemRenamed>, Handles<InventoryItemDeactivated>
    {
        public void Handle(InventoryItemCreated message)
        {
            BullShitDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));
        }

        public void Handle(InventoryItemRenamed message)
        {
            var item = BullShitDatabase.list.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
        }

        public void Handle(InventoryItemDeactivated message)
        {
            BullShitDatabase.list.RemoveAll(x=>x.Id==message.Id);
        }
    }

    public class InventoryItemDetailView : Handles<InventoryItemCreated>, Handles<InventoryItemDeactivated>, Handles<InventoryItemRenamed>, Handles<ItemsRemovedFromInventory>, Handles<ItemsCheckedInToInventory>
    {
        public void Handle(InventoryItemCreated message)
        {
            BullShitDatabase.details.Add(message.Id, new InventoryItemDetailDto(message.Id, message.Name,0,0));
        }

        public void Handle(InventoryItemDeactivated message)
        {
            BullShitDatabase.details.Remove(message.Id);
        }

        public void Handle(InventoryItemRenamed message)
        {
            InventoryItemDetailDto d = GetDetailsItem(message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;
        }

        private InventoryItemDetailDto GetDetailsItem(Guid id)
        {
            InventoryItemDetailDto d;

            if(!BullShitDatabase.details.TryGetValue(id, out d))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }

            return d;
        }

        public void Handle(ItemsRemovedFromInventory message)
        {
            InventoryItemDetailDto d = GetDetailsItem(message.Id);
            d.CurrentCount -= message.Count;
            d.Version = message.Version;
        }

        public void Handle(ItemsCheckedInToInventory message)
        {
            InventoryItemDetailDto d = GetDetailsItem(message.Id);
            d.CurrentCount += message.Count;
            d.Version = message.Version;
        }
    }
}
