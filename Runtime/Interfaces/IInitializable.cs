namespace CippSharp.Core.Utils
{
    /// <summary>
    /// Purpose: to class that have or need an initialization, when it is completed
    /// </summary>
    public interface IInitializable
    {
        bool IsInitialized { get; }
    }
}
