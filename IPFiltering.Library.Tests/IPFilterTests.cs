using NUnit.Framework;
using System.Net;

namespace IPFilter.Library.Tests
{
    [TestFixture]
    public class IPFilterTests
    {
        [Test]
        public void IPNetwork_Can_Parse_IPStrings([Values("192.168.1.3", "192.168.1.3/24")] string IPInput)
        {
            // Act
            int cIDR = IPFiltering.Library.IPFilter.ParseAddressToGetCIDR(IPInput);

            // Assert
            Assert.AreEqual(24, cIDR);
        }

        [Test]
        public void IPNetwork_Converts_To_Bytes([Values("192.168.1.3", "192.168.1.3/24")] string IPInput)
        {
            var ipStrings = IPInput.Split("/");
            // Act
            var IpBytesString = IPFiltering.Library.IPFilter.ConvertToBytesString(ipStrings[0]);

            // Assert
            Assert.AreEqual("11000000101010000000000100000011", IpBytesString);
        }


        [Test]
        public void Find_IP_Address_In_List_True([Values("192.168.1.3", "192.168.1.3/24")] string IPInput)
        {
            // Arrange
            var ipStrings = IPInput.Split("/");
            var IPBytesString = IPFiltering.Library.IPFilter.ConvertToBytesString(ipStrings[0]);
            var cidr = IPFiltering.Library.IPFilter.ParseAddressToGetCIDR(IPInput);

            //Act
            bool result = IPFiltering.Library.IPFilter.FindAddressInList(IPBytesString, cidr);

            // Assert
            Assert.IsTrue(result);
        }
    }
}