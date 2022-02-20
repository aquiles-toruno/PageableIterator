using PageableIterator;
using PageableIteratorImplementation.DTO;

namespace PageableIteratorImplementation.Applications
{
    public class PostApp
    {
        public Pageable<PostDto> GetPosts() => new MyPageableCollection().ToSyncCollection();
    }
}
