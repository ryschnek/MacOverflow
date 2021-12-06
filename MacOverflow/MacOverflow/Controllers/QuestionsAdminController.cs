using MacOverflow.Logic.StoredDataModels;
using MacOverflow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.Controllers
{
    public class QuestionsAdminController : Controller
    {
        public IActionResult Index()
        {
            var vm = new QuestionsAdminViewModel();

            vm.Questions = StoredQuestion.Load().Where(i => i.IsApproved == false).ToList();

            return View(vm);
        }

        public IActionResult Approve(Guid id)
        {
            var question = StoredQuestion.LoadById(id);

            question.IsApproved = true;

            question.Update();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            StoredQuestion.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
