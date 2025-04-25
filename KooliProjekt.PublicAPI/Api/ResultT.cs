using KooliProjekt.PublicAPI.Api;

public class Result<T> : Result
{
    public T Data { get; set; }

    public Result() { }

    public Result(T data)
    {
        Data = data;
    }
}
