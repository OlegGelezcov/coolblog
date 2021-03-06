﻿using System;
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

        public async Task<IActionResult> UserHome(string nickname) {
            var myUser = userService.GetUser(nickname);
            var allUsers = userService.GetAllUsers().Where(u => u.UserId != myUser.UserId);
            ViewData["nickname"] = myUser.NickName;
            List<ForeignUser> foreignUsers = new List<ForeignUser>();
            foreach(var user in allUsers) {
                bool isSubscribed = await userService.IsSubscribedAsync(myUser, user);
                
                foreignUsers.Add(new ForeignUser {
                    User = user,
                    IsSubsribed = isSubscribed
                });
            }
            return View(new UserHomeViewModel { User = myUser, ForeignUsers = foreignUsers });
        }

        public async Task<IActionResult> Subscribe(string source, string target) {
            var sourceUser = userService.GetUser(source);
            var targetUser = userService.GetUser(target);
            if(sourceUser == null ) {
                throw new Exception($"User with nick {source} is null");
            }
            if(targetUser == null) {
                throw new Exception($"User with nick {target} is null");
            }
            bool isSuccess = await userService.SubscribeToUserAsync(sourceUser, targetUser);
            if(!isSuccess) {
                throw new Exception($"Error of subscribe {source} => {target}");
            }
            return RedirectToAction("UserHome", new { nickname = source });
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