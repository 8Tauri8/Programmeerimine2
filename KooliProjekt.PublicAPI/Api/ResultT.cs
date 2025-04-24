
namespace KooliProjekt.PublicAPI.Api
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }

        public Result(T data, string error = null)
        {
            Data = data;
            Error = error;
        }

        // Add this HasError property
        public bool HasError => !string.IsNullOrEmpty(Error);
    }
}
