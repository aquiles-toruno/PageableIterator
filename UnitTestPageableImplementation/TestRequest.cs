using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageableIteratorImplementation.Applications;
using System.Diagnostics;
using System.Linq;

namespace UnitTestPageableImplementation
{
    [TestClass]
    public class TestRequest
    {
        [TestMethod]
        public void GetPageByPage()
        {
            var postApp = new PostApp();

            var posts = postApp.GetPosts();
            var pages = posts.AsPages(20);

            foreach (var page in pages)
            {
                Debug.WriteLine($"Page number: {page.Number}| Values count: {page.Count}");
                Assert.IsTrue(page.Number < 6 ? page.Values.Count() == 20 : page.Values.Count() == 0);

                foreach (var post in page.Values)
                {
                    Assert.IsNotNull(post);
                    Debug.WriteLine($"Post ID: {post.id}| Post title: {post.title}");
                }
            }
        }

        [TestMethod]
        public void GetAllRecords()
        {
            var postApp = new PostApp();

            var posts = postApp.GetPosts();

            foreach (var post in posts)
            {
                Debug.WriteLine($"Post ID: {post.id}| Post title: {post.title}");
            }

            Assert.IsTrue(posts.Count() == 100);
        }
    }
}
