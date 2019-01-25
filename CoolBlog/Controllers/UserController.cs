using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogModel.Data;
using CoolBlog.Models;
using CoolBlog.Models.Services;
using CoolBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CoolBlog.Controllers
{
    public class UserController : Controller
    {
        private UserService userService;

        public UserController(UserService userService ) {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View(userService.GetAllUsers().ToViewModels());
        }

        public async Task<IActionResult> TestSubs() {
            var user = userService.GetUser("oleggel");
            var blog = await userService.GetUserBlogAsync(user);
            var subscriptions = await userService.GetUserSubscriptionsAsync(user);
            var readedPosts = await userService.GetUserReadedPostsAsync(user);

            return Content($"blog: {blog != null}, user subs: {subscriptions != null}, readed {readedPosts != null}");
        }

        public async Task<IActionResult> AddBlog() {
            var user = userService.GetUser("oleggel");
            var blog = await userService.AddBlogAsync(user);
            return Content($"blog.User != null: {blog.User != null}, blog.UserId: {blog.UserId}, user.Blog != null: {user.Blog != null}");
        }
        
        public IActionResult AddUser() {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel user) {
            if(!ModelState.IsValid) {
                ModelState.AddModelError(nameof(user), "Model is invalid");
                return View();
            } 

            if(userService.HasUser(user.Nickname)) {
                ModelState.AddModelError("DuplicateNickname", "Already has user with such nickname");
                return View();
            }

            var newUser = await userService.AddUser(user);
            return View("ViewUser", newUser.ToViewModel());
        }
    }
}