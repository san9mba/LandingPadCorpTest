using Common.Exceptions;
using FluentValidation;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage ExecuteWitValidation<TValidator, TModel>(TModel model, Func<HttpResponseMessage> func) where TValidator : AbstractValidator<TModel>
        {
            var validator = Activator.CreateInstance<TValidator>();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { message = errorMessages });
            }

            return ExecuteWithTryCatch(func);
        }

        public HttpResponseMessage ExecuteWithTryCatch(Func<HttpResponseMessage> func)
        {
            HttpResponseMessage response;
            try
            {
                response = func();
            }

            catch (EntityAlreadyExistsException duplicateException)
            {
                return InvalidRequest(duplicateException.Message);
            }
            catch (EntityNotFoundException notFountException)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound, new { message = notFountException.Message });
            }
            catch (Exception ex)
            {
                //todo logging exception and return default error message and hide details
                return ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = ex.Message, error =  ex.InnerException});
            }
            return response;
        }

        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage InvalidRequest(string message)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { message });
        }
    }
}