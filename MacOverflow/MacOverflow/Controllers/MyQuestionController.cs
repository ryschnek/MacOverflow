using MacOverflow.Logic.StoredDataModels;
using MacOverflow.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacOverflow.Controllers
{
    public class MyQuestionController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new QuestionIndexViewModel();

            if (User.Identity.IsAuthenticated)
            {
                vm.MyQuestions = StoredQuestion.LoadByUserId(Guid.Parse(User.Identity.GetUserId()));

                vm.Topics = StoredTopic.Load();

                for (int i = 0; i < vm.MyQuestions.Count; i++)
                {
                    vm.MyQuestions[i].Topic = vm.Topics.Where(k => k.TopicId == vm.MyQuestions[i].TopicId).ToList().First();
                }
            }
            
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new QuestionCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(QuestionCreateViewModel vm)
        {
            if (ModelState.IsValid && Guid.Empty != vm.Question.TopicId)
            {
                var topic = StoredTopic.LoadById(vm.Question.TopicId);

                if (topic.ModCommentApproval)
                {
                    vm.Question.IsApproved = false;
                }
                else
                {
                    vm.Question.IsApproved = true;
                }

                vm.Question.Likes = 0;
                vm.Question.QuestionId = Guid.NewGuid();
                vm.Question.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Question.User = User.Identity.Name;
                vm.Question.CreatedOnDt = DateTime.UtcNow;
                vm.Question.LastEditedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Question.LastEditedOnDt = DateTime.UtcNow;

                vm.Question.Insert();
            }
            else
            {
                return View(vm);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var vm = new QuestionEditViewModel();

            vm.Question = StoredQuestion.LoadById(id);

            vm.Question.Topic = StoredTopic.LoadById(id);

            vm.Topics = StoredTopic.Load();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(QuestionEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Question.LastEditedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Question.LastEditedOnDt = DateTime.UtcNow;

                vm.Question.Update();
            }
            else
            {
                vm.Topics = StoredTopic.Load();
                return View(vm);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            StoredQuestion.Delete(id);
            StoredLike.DeleteQuestionLike(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Like(Guid id)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Dislike(Guid id)
        {
            return RedirectToAction("Index");
        }
    }
}
