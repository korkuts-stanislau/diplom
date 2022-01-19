namespace MagikAPI.Models.HelperModels
{
    public class MessageWrapper<T>
    {
        public T Entity { get; set; }
        public string Error { get; set; }

        public MessageWrapper(T entity, string error)
        {
            Entity = entity;
            Error = error;
        }
    }
}
