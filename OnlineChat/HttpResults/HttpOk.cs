using Microsoft.AspNetCore.Mvc;

namespace OnlineChat.HttpResults
{
    public class HttpOk
    {
        public JsonResult ToJson () {
            return new JsonResult(this) { StatusCode = 200 };
        }
    }
}