

namespace FeedIndex.Deezer
{
    internal class ArrayResponse<T> where T : ApiDocument
    {
        public T[]? data { get; set; }
    }
}
