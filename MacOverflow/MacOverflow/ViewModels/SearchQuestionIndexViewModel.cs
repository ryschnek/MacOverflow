using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class SearchQuestionIndexViewModel
    {
        public List<StoredQuestion> Questions { get; set; }

        public List<StoredTopic> Topics { get; set; }

        public string SortBy { get; set; } = "Likes High to Low";

        public string FilterRecommendedAudience { get; set; } = "";

        public bool FilterHigh { get; set; }

        public bool FilterMedium { get; set; }

        public bool FilterLow { get; set; }

        public bool FilterPeer { get; set; }

        public bool FilterTa { get; set; }

        public bool FilterProf { get; set; }

        public bool FilterAdmin { get; set; }

        public string FilterTopic { get; set; } = "";

        public string SearchTerm { get; set; } = ".";
    }
}

