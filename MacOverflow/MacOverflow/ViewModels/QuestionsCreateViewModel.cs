using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class QuestionCreateViewModel
    {
        public StoredQuestion Question { get; set; }

        public List<StoredTopic> StoredTopics { get; set; } 

        public QuestionCreateViewModel()
        {
            StoredTopics = StoredTopic.Load();
        }
    }
}
