namespace Api.Core.Domain.Common;

public abstract class ContpaqiResponse
{
}

public interface IContpaqiResponse<TModel>
{
    /// <summary>
    ///     Response model.
    /// </summary>
    public TModel Model { get; set; }
}
