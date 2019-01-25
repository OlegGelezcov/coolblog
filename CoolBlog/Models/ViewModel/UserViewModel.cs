using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models.ViewModel {
    public class UserViewModel {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Job Position")]
        public string Position { get; set; }

        
    }
}
