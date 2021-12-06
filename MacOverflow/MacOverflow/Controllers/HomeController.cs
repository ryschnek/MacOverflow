using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MacOverflow.Models;
using MacOverflow.Logic.StoredDataModels;
using Microsoft.AspNet.Identity;
using MacOverflow.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MacOverflow.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var vm = new HomeViewModel();

            if (User.Identity.IsAuthenticated)
            {
                vm.AllTopics = StoredTopic.Load();
                var appUserTopics = StoredAppUserInTopic.LoadById(Guid.Parse(User.Identity.GetUserId()));
                var myTopicIds = appUserTopics.Select(i => i.TopicId).ToList();

                for(var i = 0; i < vm.AllTopics.Count; i++)
                {
                    if (myTopicIds.Contains(vm.AllTopics[i].TopicId))
                    {
                        vm.MyTopics.Add(vm.AllTopics[i]);
                        vm.AllTopics[i].IsSubscribed = true;
                    }
                    else
                    {
                        vm.AllTopics[i].IsSubscribed = false;
                    }
                }
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddSubscribedTopic(Guid topicId)
        {
            var UserInTopic = new StoredAppUserInTopic(Guid.NewGuid(), Guid.Parse(User.Identity.GetUserId()), topicId, DateTime.UtcNow);

            UserInTopic.Insert();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RemoveTopic(Guid id)
        { 
            StoredAppUserInTopic.Delete(id, Guid.Parse(User.Identity.GetUserId()));

            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult AddTopic(Guid id)
        {
            var storedAppUserInTopic = new StoredAppUserInTopic(Guid.NewGuid(), id, Guid.Parse(User.Identity.GetUserId()), DateTime.UtcNow);
           
            storedAppUserInTopic.Insert();

            return RedirectToAction("Index");

        }
    }
}
