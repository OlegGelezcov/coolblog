using System;
using System.Collections.Generic;
using System.Text;

namespace BlogModel.Data {
    public class ReadedUserPost {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
