using BlogModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.ViewModel {
    public class UserHomeViewModel {
        public User User { get; set; }
        public IEnumerable<ForeignUser> ForeignUsers { get; set; }
    }
}
