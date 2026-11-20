using NUnit.Framework;
using SCoW.Core;

namespace SCoW.Tests
{
    public sealed class SmokeTests
    {
        [Test]
        public void CoreAssemblyIsReachable()
        {
            Assert.AreEqual("SCoW.Core", Marker.AssemblyName);
        }
    }
}
