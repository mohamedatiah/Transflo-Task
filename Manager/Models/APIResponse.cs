using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
namespace TransfloDriver.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
        public APIResponse InvalidPropertyResponse(string propertyName)
        {
            this.Result = null;
            this.ErrorMessages.Add ( "Invalid " + propertyName );
            this.IsSuccess = false;
            this.StatusCode =HttpStatusCode.BadRequest;
            return this;
        }
        public APIResponse RequiredPropertyResponse(string propertyName)
        {
            this.Result = null;
            this.ErrorMessages.Add(propertyName + " Is Required");

            this.IsSuccess = false;
            this.StatusCode =HttpStatusCode.BadRequest;
            return this;
        }
        public APIResponse InvalidData()
        {
            this.Result = null;
            this.ErrorMessages.Add("Invalid Data");
            this.IsSuccess = false;
            this.StatusCode = HttpStatusCode.BadRequest;
            return this;
        }
        public APIResponse DeletedSuccessfullyResponse()
        {
            this.Result = null;
            this.ErrorMessages.Add("Deleted Successfully");

            this.IsSuccess = true;
            this.StatusCode =HttpStatusCode.OK;
            return this;
        }
        public APIResponse UpdatedSuccessfullyResponse(object Updatedenttity)
        {
            this.Result = Updatedenttity;
            this.IsSuccess = true;
            this.StatusCode = HttpStatusCode.OK;
            return this;
        }
        public APIResponse CreatedSuccessfullyResponse(string id)
        {
            this.Result = "Created Successfully with Id=" + id;
            this.IsSuccess = true;
            this.StatusCode =HttpStatusCode.OK;
            return this;
        }
    }
}
