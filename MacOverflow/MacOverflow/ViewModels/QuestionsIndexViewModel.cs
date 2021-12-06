using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class QuestionIndexViewModel
    {
        public List<StoredQuestion> MyQuestions { get; set; }

        public List<StoredTopic> Topics { get; set; }
        
    }
}
