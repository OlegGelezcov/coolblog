using BlogModel.Data;
using CoolBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.Services {
    public class UserService {

        private BlogDbContext db;

        public UserService(BlogDbContext dbContext) {
            db = dbContext;
        }

        public IEnumerable<User> GetAllUsers() {
            return db.Users;
        }


        public bool HasUser(string nickname) {
            var user = db.Users.FirstOrDefault((usr) => usr.NickName == nickname);
            return (user != null);
        }

        public User GetUser(string nickName ) {
            var user =  db.Users.FirstOrDefault(usr => usr.NickName == nickName);
            return user;
        }

        public async Task<Blog> GetUserBlogAsync(User user) {
            await db.Entry(user).Reference(u => u.Blog).LoadAsync();
            return user.Blog;
        }

        public async Task<ICollection<UserBlogSubscription>> GetUserSubscriptionsAsync(User user) {
            await db.Entry(user).Collection(u => u.Subscriptions).LoadAsync();
            return user.Subscriptions;
        }

        public async Task<ICollection<ReadedUserPost>> GetUserReadedPostsAsync(User user) {
            await db.Entry(user).Collection(u => u.ReadedPosts).LoadAsync();
            return user.ReadedPosts;
        }



        public async Task<Blog> AddBlogAsync(User user) {
            Blog blog = new Blog {
            };
            user.Blog = blog;
            //await db.Blogs.AddAsync(blog);
            //await db.SaveChangesAsync();
            //blog.User = user;
            await db.SaveChangesAsync();
            return blog;
        }

        public async Task<User> AddUser(UserViewModel model) {
            User newUser = new User {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position,
                NickName = model.Nickname
            };

            await db.Users.AddAsync(newUser);

            /*
            Blog blog = new Blog {
                 
            };
            await db.Blogs.AddAsync(blog);

            UserBlogSubscription subcription = new UserBlogSubscription {
                Blog = blog,
                User = newUser
            };

            await db.Subscriptions.AddAsync(subcription);
            */

            await db.SaveChangesAsync();

            return GetUser(newUser.NickName);

        }
    }
}
