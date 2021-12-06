
using MacOverflow.Logic.StoredDataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MacOverflow.ViewModels
{
    public class HomeViewModel
    {
        public List<StoredTopic> MyTopics { get; set; } = new List<StoredTopic>();

        public List<StoredTopic> AllTopics { get; set; } = new List<StoredTopic>();

        public StoredTopic SelectedTopic { get; set; }
    }
}