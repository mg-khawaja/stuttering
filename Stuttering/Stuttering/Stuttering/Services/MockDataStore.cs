using Stuttering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stuttering.Services
{
    public class MockDataStore : IDataStore<StutterReadItem>
    {
        readonly List<StutterReadItem> items;

        public MockDataStore()
        {
            items = new List<StutterReadItem>()
            {
                new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Triggers of Stuttering", Description="This is an item description." },
                new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Emotional Reactions", Description="This is an item description.", Image = "warning.png", Type = StutterreadItemType.ImageText },
                new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Stuttering Varies", Image = "warning.png", Type = StutterreadItemType.Image },
                new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Nothing is Wrong with Stutters", Description="This is an item description." },
                new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Management", Description="This is an item description." },
                //new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Facts about Stuttering", Description="This is an item description." }
                //new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Motivational Quotes", Description="This is an item description." }
                //new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "The Journey", Description="This is an item description." }
                //new StutterReadItem { Id = Guid.NewGuid().ToString(), Label = "Hierarchy", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(StutterReadItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(StutterReadItem item)
        {
            var oldItem = items.Where((StutterReadItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((StutterReadItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<StutterReadItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<StutterReadItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}