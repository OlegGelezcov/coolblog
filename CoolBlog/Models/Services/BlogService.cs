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
            return blog.Posts;
        }


    }
}
