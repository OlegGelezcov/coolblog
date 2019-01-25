using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.ViewModel {
    public class PostViewModel {

        [Editable(false, AllowInitialValue = true)]
        public string NickName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
