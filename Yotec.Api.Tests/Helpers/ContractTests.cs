using System;
using NUnit.Framework;
using Yotec.Api.Helpers;

namespace Yotec.Api.Tests.Helpers
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void NotNull_NotNull_Pass()
        {
            var arg = "argValue";

            Assert.DoesNotThrow(() => { Contract.NotNull(arg, nameof(arg)); });
        }

        [Test]
        public void NotNull_Null_ThrowException()
        {
            string arg = null;

            Assert.Throws<ArgumentNullException>(() => { Contract.NotNull(arg, nameof(arg)); });
        }

        [Test]
        public void NotNull_Empty_ThrowException()
        {
            string arg = "";

            Assert.Throws<ArgumentNullException>(() => { Contract.NotNull(arg, nameof(arg)); });
        }
    }
}
