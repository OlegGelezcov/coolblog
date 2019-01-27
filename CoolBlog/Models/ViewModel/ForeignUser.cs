using BlogModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.ViewModel {
    public class ForeignUser {
        public User User { get; set; }
        public bool IsSubsribed { get; set; }
    }
}
