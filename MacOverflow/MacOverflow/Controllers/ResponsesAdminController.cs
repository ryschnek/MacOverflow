using MacOverflow.Logic.StoredDataModels;
using MacOverflow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.Controllers
{
    public class ResponsesAdminController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ResponsesAdminViewModel();

            vm.Responses = StoredResponse.Load().Where(i => i.IsApproved == false).ToList();

            for(int i = 0; i < vm.Responses.Count; i++)
            {
                vm.Responses[i].ParentQuestion = StoredQuestion.LoadById(vm.Responses[i].ParentCommentId);
            }

            return View(vm);
        }

        public IActionResult Approve(Guid id)
        {
            var response = StoredResponse.LoadById(id);

            response.IsApproved = true;

            response.Update();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            StoredResponse.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
