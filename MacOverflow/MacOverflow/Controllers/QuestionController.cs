using MacOverflow.Logic.StoredDataModels;
using MacOverflow.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MacOverflow.Controllers
{
    public class QuestionController : Controller
    {

        [HttpGet]
        public IActionResult Index(Guid filter, SearchQuestionIndexViewModel vm)
        {

            vm.Questions = StoredQuestion.Load().Where(i => i.IsApproved).ToList();

            vm.Questions = vm.Questions.OrderByDescending(i => i.Likes).ToList();

            vm.Topics = StoredTopic.Load();

            for(int i = 0; i < vm.Questions.Count; i++)
            {
                vm.Questions[i].Topic = StoredTopic.LoadById(vm.Questions[i].TopicId);
            }

            if(filter != Guid.Empty)
            {
                vm.Questions = vm.Questions.Where(i => i.TopicId == filter).ToList();
            }

            switch (vm.SortBy)
            {
                case "Likes High to Low":
                    vm.Questions = vm.Questions.OrderByDescending(i => i.Likes).ToList();
                    break;
                case "Likes Low to High":
                    vm.Questions = vm.Questions.OrderBy(i => i.Likes).ToList();
                    break;
                case "Time Posted Newer to Older":
                    vm.Questions = vm.Questions.OrderByDescending(i => i.CreatedOnDt).ToList();
                    break;
                case "Time Posted Older to Newer":
                    vm.Questions = vm.Questions.OrderBy(i => i.CreatedOnDt).ToList();
                    break;
            }

            if (vm.FilterHigh)
            {
                vm.Questions = vm.Questions.Where(i => i.ImportanceLevel != "High").ToList();
            }

            if (vm.FilterMedium)
            {
                vm.Questions = vm.Questions.Where(i => i.ImportanceLevel != "Medium").ToList();
            }

            if (vm.FilterLow)
            {
                vm.Questions = vm.Questions.Where(i => i.ImportanceLevel != "Low").ToList();
            }

            if (vm.FilterPeer)
            {
                vm.Questions = vm.Questions.Where(i => i.RecommendedAudience != "Peers").ToList();
            }

            if (vm.FilterTa)
            {
                vm.Questions = vm.Questions.Where(i => i.RecommendedAudience != "Teaching Assistants").ToList();
            }

            if (vm.FilterProf)
            {
                vm.Questions = vm.Questions.Where(i => i.RecommendedAudience != "Professors").ToList();
            }

            if (vm.FilterAdmin)
            {
                vm.Questions = vm.Questions.Where(i => i.RecommendedAudience != "University Administrators").ToList();
            }

            if (string.IsNullOrEmpty(vm.SearchTerm)) vm.SearchTerm = ".";

            var regex = new Regex(vm.SearchTerm.ToLower());

            vm.Questions = vm.Questions.Where(i => regex.IsMatch(i.Title.ToLower()) || regex.IsMatch(i.Topic.Topic.ToLower()) || regex.IsMatch(i.RecommendedAudience.ToLower()) || regex.IsMatch(i.ImportanceLevel.ToLower()) || regex.IsMatch(i.Question.ToLower())).ToList();
            
            return View(vm);
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var vm = new QuestionDetailsViewModel();

            vm.Question = StoredQuestion.LoadById(id);

            vm.Responses = StoredResponse.LoadByQuestionId(id).Where(j => j.IsApproved).OrderByDescending(i => i.Likes).ToList();

            vm.Likes = StoredLike.LoadByUser(Guid.Parse(User.Identity.GetUserId()));

            if(vm.Likes.Any(i => i.ResponseId == vm.Question.QuestionId)){
                if(vm.Likes.Where(i => i.ResponseId == vm.Question.QuestionId).First().IsLike)
                {
                    vm.Question.HasLiked = true;
                }
                else
                {
                    vm.Question.HasDisliked = true;
                }
            }

            for(var i = 0; i < vm.Responses.Count; i++)
            {
                if (vm.Likes.Any(j => j.ResponseId == vm.Responses[i].ResponseId))
                {
                    if (vm.Likes.Where(k => k.ResponseId == vm.Responses[i].ResponseId).First().IsLike)
                    {
                        vm.Responses[i].HasLiked = true;
                    }
                    else
                    {
                        vm.Responses[i].HasDisliked = true;
                    }
                }
            }
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Reply(QuestionDetailsViewModel vm)
        {
            var topic = StoredTopic.LoadById(vm.Question.TopicId);

            if (!string.IsNullOrEmpty(vm.reply))
            {
                var response = new StoredResponse();
                response.ResponseId = Guid.NewGuid();
                response.Response = vm.reply;
                response.ParentCommentId = vm.Question.QuestionId;
                response.Likes = 0;
                if (topic.ModResponseApproval)
                {
                    response.IsApproved = false;
                }
                else
                {
                    response.IsApproved = true;
                }

                response.User = User.Identity.Name;
                response.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());
                response.CreatedOnDt = DateTime.UtcNow;
                response.LastEditedOnDt = DateTime.UtcNow;

                response.Insert();
            }

            return RedirectToAction("Details", new { id = vm.Question.QuestionId });
        }

        [HttpGet]
        public IActionResult QuestionReaction(Guid id, string reaction)
        {
            var question = StoredQuestion.LoadById(id);
            var like = StoredLike.LoadByResponseUser(id, Guid.Parse(User.Identity.GetUserId()));

            if (reaction == "Liked")
            {
                if(like.LikeId == Guid.Empty)
                {
                    like = new StoredLike(Guid.NewGuid(), Guid.Parse(User.Identity.GetUserId()), id, true, DateTime.UtcNow);

                    question.Likes = question.Likes + 1;

                    like.Insert();

                    question.Update();
                }
                else
                {
                    like.IsLike = true;

                    question.Likes = question.Likes + 2;

                    like.Update();

                    question.Update();
                }
            }
            else if(reaction == "Disliked")
            {
                if (like.LikeId == Guid.Empty)
                {
                    like = new StoredLike(Guid.NewGuid(), Guid.Parse(User.Identity.GetUserId()), id, false, DateTime.UtcNow);

                    question.Likes = question.Likes - 1;

                    like.Insert();

                    question.Update();
                }
                else
                {
                    like.IsLike = false;

                    question.Likes = question.Likes - 2;

                    like.Update();

                    question.Update();
                }
            }
            else if(reaction == "Unliked")
            {
                like.Delete();

                question.Likes = question.Likes - 1;

                question.Update();
            }
            else if(reaction == "UnDisliked")
            {
                like.Delete();

                question.Likes = question.Likes + 1;

                question.Update();
            }

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
        public IActionResult ResponseReaction(Guid questionId, Guid id, string reaction)
        {
            var response = StoredResponse.LoadById(id);
            var like = StoredLike.LoadByResponseUser(id, Guid.Parse(User.Identity.GetUserId()));

            if (reaction == "Liked")
            {
                if (like.LikeId == Guid.Empty)
                {
                    like = new StoredLike(Guid.NewGuid(), Guid.Parse(User.Identity.GetUserId()), id, true, DateTime.UtcNow);

                    response.Likes = response.Likes + 1;

                    like.Insert();

                    response.Update();
                }
                else
                {
                    like.IsLike = true;

                    response.Likes = response.Likes + 2;

                    like.Update();

                    response.Update();
                }
            }
            else if (reaction == "Disliked")
            {
                if (like.LikeId == Guid.Empty)
                {
                    like = new StoredLike(Guid.NewGuid(), Guid.Parse(User.Identity.GetUserId()), id, false, DateTime.UtcNow);

                    response.Likes = response.Likes - 1;

                    like.Insert();

                    response.Update();
                }
                else
                {
                    like.IsLike = false;

                    response.Likes = response.Likes - 2;

                    like.Update();

                    response.Update();
                }
            }
            else if (reaction == "Unliked")
            {
                like.Delete();

                response.Likes = response.Likes - 1;

                response.Update();
            }
            else if (reaction == "UnDisliked")
            {
                like.Delete();

                response.Likes = response.Likes + 1;

                response.Update();
            }

            return RedirectToAction("Details", new { id = questionId });
        }
    }
}
