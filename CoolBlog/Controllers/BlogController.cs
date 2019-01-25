using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogModel.Data;
using CoolBlog.Models.Services;
using CoolBlog.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CoolBlog.Controllers
{
    public class BlogController : Controller
    {
        private UserService userService;
        private BlogService blogService;


        public BlogController(UserService userService, BlogService blogService) {
            this.userService = userService;
            this.blogService = blogService;
        }

        public IActionResult AddPost(string nickname) {

            User user = userService.GetUser(nickname);
            if(user == null ) {
                throw new ArgumentException($"user nick {nickname} not founded");
            }
            return View(new PostViewModel {  NickName = user.NickName });
        }

        [HttpPost]
        public IActionResult AddPost(PostViewModel model ) {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}