using System.Collections.Generic;
using NUnit.Framework;
using Yotec.Api.Helpers;

namespace Yotec.Api.Tests.Helpers
{
    [TestFixture]
    public class SerializeHelperTests
    {
        [Test]
        public void Serialize_SomeObject_Success()
        {
            var objs = new Dictionary<object, string>
            {
                {
                    new List<int> { 1, 2, 3 },
                    "[1,2,3]"
                },
                {
                    new Dictionary<string, int> { { "Title1", 1 }, { "Title2", 2 }, { "Title3", 3 } },
                    "{\"title1\":1,\"title2\":2,\"title3\":3}"
                }
            };

            foreach (var obj in objs)
            {
                Assert.AreEqual(SerializeHelper.Serialize(obj.Key), obj.Value);
            }
        }
    }
}
