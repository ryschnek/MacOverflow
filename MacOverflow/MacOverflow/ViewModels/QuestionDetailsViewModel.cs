using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class QuestionDetailsViewModel
    {
        public StoredQuestion Question { get; set; }

        public List<StoredResponse> Responses { get; set; }

        public List<StoredLike> Likes { get; set; }
        public string reply { get; set; }
    }
}
