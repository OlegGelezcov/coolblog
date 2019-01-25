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


    }
}
