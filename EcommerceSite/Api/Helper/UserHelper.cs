using Api.Models;
using SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helper
{
    public static class UserHelper
    {
        public static IEnumerable<UserModel> MapToUserModel(this IEnumerable<AspNetUser> users)
        {
            return users.Select(item => MapToUserModelModel(item));
        }

        public static UserModel MapToUserModelModel(this AspNetUser user)
        {
            return new UserModel { UserId = user.Id, UserName = user.Email };
        }
    }
}
