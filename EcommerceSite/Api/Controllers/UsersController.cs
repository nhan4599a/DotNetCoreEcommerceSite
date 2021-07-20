using Api.Models;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using System.Collections.Generic;
using Api.Helper;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UsersController()
        {
            db = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("all")]
        public ApiJsonResponseModel<IEnumerable<UserModel>> GetAll()
        {
            return new ApiJsonResponseModel<IEnumerable<UserModel>>
            {
                Data = db.AspNetUsers.MapToUserModel()
            };
        }
    }
}
