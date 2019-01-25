using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogModel.Data {
    public class UserBlogSubscription {

        public int UserId { get; set; }
        public User User { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
