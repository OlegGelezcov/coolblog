using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogModel.Data {
    public class User {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }

        public Blog Blog { get; set; }

        public ICollection<UserBlogSubscription> Subscriptions { get; set; }
        
        public ICollection<ReadedUserPost> ReadedPosts { get; set; }

    }
}
