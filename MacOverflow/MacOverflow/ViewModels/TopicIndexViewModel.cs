using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class TopicIndexViewModel
    {
        public List<StoredTopic> MyTopics { get; set; } = new List<StoredTopic>();
    }
}
