namespace OnlineChat.HttpResults.Exceptions
{
    public class NotFoundHttpException: HttpException
    {
        public NotFoundHttpException ( object o ): base ( "The object with id " + o + " doesn't exist"){}
    }
}