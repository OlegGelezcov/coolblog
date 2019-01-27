using BlogModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.Services {
    public class BlogService {

        private BlogDbContext dbContext;
        private UserService userService;


        public BlogService(BlogDbContext dbContext, UserService userService) {
            this.dbContext = dbContext;
            this.userService = userService;
        }


        public async Task AddPostAsync(User user, Blog blog, Post post ) {
            await dbContext.Entry(blog).Collection(b => b.Posts).LoadAsync();
            blog.Posts.Add(post);
            await dbContext.SaveChangesAsync();

            await MakePostReaded(user, post);
        }

        public async Task MakePostReaded(User user, Post post) {
            var readedPosts = await userService.GetUserReadedPostsAsync(user);
            readedPosts.Add(new ReadedUserPost {
                Post = post
            });
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetUserPostsAsync(string nickname) {
            List<Post> results = new List<Post>();

            var user = userService.GetUser(nickname);
            if(user == null) {
                throw new ArgumentException($"{nameof(nickname)}");
            }
            var blog = await userService.GetUserBlogAsync(user);
            if(blog == null ) {
                throw new NullReferenceException(nameof(blog));
            }

            var entry = dbContext.Entry(blog).Collection(b => b.Posts);
            if(false == entry.IsLoaded) {
                await entry.LoadAsync();
            }
            //return blog.Posts;
            results.AddRange(blog.Posts);

            var subscriptions = await userService.GetUserSubscriptionsAsync(user);

            foreach(var subscription in subscriptions) {
                var refBlog = dbContext.Entry(subscription).Reference(s => s.Blog);
                if(!refBlog.IsLoaded) {
                    await refBlog.LoadAsync();
                }
                var subscribedBlog = subscription.Blog;
                var refPosts = dbContext.Entry(subscribedBlog).Collection(b => b.Posts);
                if(!refPosts.IsLoaded) {
                    await refPosts.LoadAsync();
                }
                results.AddRange(subscription.Blog.Posts);
            }

            foreach(var post in results ) {
                var refBlog = dbContext.Entry(post).Reference(p => p.Blog);
                if(!refBlog.IsLoaded) {
                    await refBlog.LoadAsync();
                }
                var blogUserRef = dbContext.Entry(post.Blog).Reference(b => b.User);
                if(!blogUserRef.IsLoaded) {
                    await blogUserRef.LoadAsync();
                }
            }

            return results.OrderByDescending(p => p.CreatedDate);
        }


        public async Task<bool> IsPostReadedByUser(User user, Post post) {
            var readedPosts = await userService.GetUserReadedPostsAsync(user);

            foreach(var readedPost in readedPosts) {
                var postRef = dbContext.Entry(readedPost).Reference(rp => rp.Post);
                if(!postRef.IsLoaded) {
                    await postRef.LoadAsync();
                }
                if(readedPost.PostId == post.PostId) {
                    return true;
                }
            }
            return false;
        }

    }
}
