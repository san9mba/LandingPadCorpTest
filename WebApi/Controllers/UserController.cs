using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using WebApi.Models.Users;
using WebApi.Validators;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            return ExecuteWitValidation<UserModelValidator, UserModel>(model, () =>
            {
                var user = _createUserService.Create(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            });
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            return ExecuteWitValidation<UserModelValidator, UserModel>(model, () =>
            {
                var user = _updateUserService.Update(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            });
        }
        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            return ExecuteWithTryCatch(() =>
            {
                _deleteUserService.Delete(userId);
                return Found();
            });
        }
        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
                return DoesNotExist();
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            var users = _getUserService.GetUsers(skip, take, type, name, email)
                            .Select(q => new UserData(q))
                            .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllUsers()
        {
            return ExecuteWithTryCatch(() =>
            {
                _deleteUserService.DeleteAll();
                return Found();
            });
        }

        [Route("list/tag")]
        [HttpGet]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            var users = _getUserService.GetUsersByTag(tag)
                            .Select(q => new UserData(q))
                            .ToList();
            return Found(users);
        }
    }
}