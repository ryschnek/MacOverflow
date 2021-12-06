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
    public class TopicController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new TopicIndexViewModel();

            if (User.Identity.IsAuthenticated)
            {
                vm.MyTopics = StoredTopic.LoadByUserId(Guid.Parse(User.Identity.GetUserId()));
            }
           
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new TopicCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TopicCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Topic.TopicId = Guid.NewGuid();
                vm.Topic.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Topic.CreatedOnDt = DateTime.UtcNow;
                vm.Topic.LastEditedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Topic.LastEditedOnDt = DateTime.UtcNow;

                vm.Topic.Insert();
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
            var vm = new TopicEditViewModel();

            vm.Topic = StoredTopic.LoadById(id);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TopicEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Topic.LastEditedByUserId = Guid.Parse(User.Identity.GetUserId());
                vm.Topic.LastEditedOnDt = DateTime.UtcNow;

                vm.Topic.Update();
            }
            else
            {
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            StoredAppUserInTopic.DeleteTopic(id);
            StoredTopic.Delete(id);
            StoredQuestion.DeleteTopic(id);

            return RedirectToAction("Index");
        }
    }
}
