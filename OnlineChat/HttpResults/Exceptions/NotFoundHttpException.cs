namespace OnlineChat.HttpResults.Exceptions
{
    public class NotFoundHttpException: HttpException
    {
        public NotFoundHttpException ( long id ): base ( "The object with " + id + " doesn't exist"){}
    }
}