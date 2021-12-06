using MacOverflow.Logic.StoredDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.ViewModels
{
    public class ResponsesAdminViewModel
    {
        public List<StoredResponse> Responses { get; set; }

        public List<StoredQuestion> Questions { get; set; }

        public Guid SelectedResponse { get; set; }
    }
}
