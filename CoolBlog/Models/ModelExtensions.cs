using BlogModel.Data;
using CoolBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBlog.Models {
    public static class ModelExtensions {

        public static UserViewModel ToViewModel(this User user) {
            return new UserViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Nickname = user.NickName,
                Position = user.Position
            };
        }

        public static IEnumerable<UserViewModel> ToViewModels(this IEnumerable<User> users) {
            foreach(var user in users ) {
                yield return user.ToViewModel();
            }
        }

        public static PostViewModel ToViewModel(this Post post, bool isReaded)
            => new PostViewModel {
                Title = post.Title,
                Content = post.Content,
                Created = post.CreatedDate,
                NickName = post.Blog?.User?.NickName ?? string.Empty,
                IsReaded = isReaded,
                PostId = post.PostId
            };

        public static IEnumerable<PostViewModel> ToViewModels(this IEnumerable<Tuple<Post, bool>> posts ) {
            foreach(var post in posts) {
                yield return post.Item1.ToViewModel(post.Item2);
            }
        }
    }
}
 