using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.HttpResults.Exceptions
{
    public class HttpException
    {
        public string Text { get; set; }
        
        public HttpException ( string text ){
            Text = text;
        }
        
        public JsonResult ToJson () {
            return new JsonResult(this) { StatusCode = 400 };
        }
    }
}