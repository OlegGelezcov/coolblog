using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.ViewModel {
    public class PostViewModel {

        public int PostId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string NickName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsReaded { get; set; }

        
    }
}
