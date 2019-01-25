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
        public async Task<IActionResult> AddPost(PostViewModel model ) {
            User user = userService.GetUser(model.NickName);
            if (user == null) {
                throw new ArgumentException($"user nick {model.NickName} not founded");
            }

            var blog = await userService.GetUserBlogAsync(user);

            await blogService.AddPostAsync(
                user,
                blog,
                new Post {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedDate = DateTime.UtcNow
                });

            
            return RedirectToAction("Posts", new { nickname = model.NickName });
        }

        public async Task<IActionResult> Posts(string nickname ) {
            var posts = await blogService.GetUserPostsAsync(nickname);
            var viewModels = posts.ToViewModels();
            ViewData["nickname"] = nickname;
            return View(viewModels);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}