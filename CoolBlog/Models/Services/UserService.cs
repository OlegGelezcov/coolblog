using BlogModel.Data;
using CoolBlog.Models.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.Services {
    public class UserService {

        private BlogDbContext db;
        private ILogger logger;

        public UserService(BlogDbContext dbContext, ILogger<UserService> logger) {
            db = dbContext;
            this.logger = logger;
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
            var entry = db.Entry(user).Reference(u => u.Blog);
            if(false == entry.IsLoaded) {
                await entry.LoadAsync();
            }
            return user.Blog;
        }

        public async Task<ICollection<UserBlogSubscription>> GetUserSubscriptionsAsync(User user) {
            var entry = db.Entry(user).Collection(u => u.Subscriptions);
            if(false == entry.IsLoaded) {
                await entry.LoadAsync();
            }
            return user.Subscriptions;
        }

        public async Task<ICollection<ReadedUserPost>> GetUserReadedPostsAsync(User user) {
            var entry = db.Entry(user).Collection(u => u.ReadedPosts);
            if(false == entry.IsLoaded) {
                await entry.LoadAsync();
            }
            return user.ReadedPosts;
        }

        private async Task LoadSubscribtionBlogAsync(UserBlogSubscription subscription) {
            if(subscription.Blog == null ) {
                var tracking = db.Entry(subscription).Reference(s => s.Blog);
                if(!tracking.IsLoaded) {
                    await tracking.LoadAsync();
                }
            }
        }

        public async Task<bool> IsSubscribedAsync(User sourceUser, User targetUser ) {
            //logger.LogInformation("IsSubscribedAsync");
            var subscriptions = await GetUserSubscriptionsAsync(sourceUser);
            //logger.LogInformation($"subscriptions length => {subscriptions.Count}");
            foreach(var subscription in subscriptions) {
                //logger.LogInformation($"check subscription user {subscription.User.UserId}:{subscription.UserId} with target {targetUser.UserId}");
                await LoadSubscribtionBlogAsync(subscription);
                logger.LogInformation($"source usr id: {sourceUser.UserId}, targ usr id: {targetUser.UserId}, subs usr id: {subscription.UserId}");
                if (subscription.Blog.UserId == targetUser.UserId) {                    
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SubscribeToUserAsync(User sourceUser, User targetUser ) {
            bool alreadySubscribed = await IsSubscribedAsync(sourceUser, targetUser);
            
            if(false == alreadySubscribed) {
                var targetBlog = await GetUserBlogAsync(targetUser);
                if(targetBlog == null ) {
                    await AddBlogAsync(targetUser);
                    targetBlog = await GetUserBlogAsync(targetUser);
                    if(targetBlog == null ) {
                        throw new NullReferenceException(nameof(targetBlog));
                    }
                }
                var subscriptions = await GetUserSubscriptionsAsync(sourceUser);
                subscriptions.Add(new UserBlogSubscription {
                    Blog = targetBlog
                });
                await db.SaveChangesAsync();
                return true;
            }
            return false;
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
