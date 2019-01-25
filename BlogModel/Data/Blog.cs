using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogModel.Data {
    public class Blog {
        public int BlogId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<UserBlogSubscription> Subscriptions { get; set; }
    }
}
