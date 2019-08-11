using NUnit.Framework;
using System.Net;

namespace IPFilter.Library.Tests
{
    [TestFixture]
    public class IPFilterTests
    {
        [Test]
        public void Creates_Test_IPList()
        {
            // Arrange
            string iPToCheck = "192.168.0.10";
            //Act
            IPFiltering.Library.IPFilter.CreateTestList();

            //
            Assert.IsNotNull(IPFiltering.Library.IPFilter.iplist);
            Assert.IsTrue(IPFiltering.Library.IPFilter.iplist.CheckNumber(iPToCheck));
        }

        [Test]
        public void Is_IPAddress_Valid([Values("192.168.1.1", "192.168.1.1/24")] string IpToValidate)
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();
            bool result = false;

            // Act
            result = IPFiltering.Library.IPFilter.IsAddressValid(IpToValidate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Is_IPAddress_InValid([Values("192.1681.1", "192.168.1.257/24", "192.168.1.253/33")] string IpToValidate)
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();
            bool result = true;

            // Act
            result = IPFiltering.Library.IPFilter.IsAddressValid(IpToValidate);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Can_Add_Single_IPAddress([Values("192.168.0.200", "192.168.0.210")] string IPAddressToAdd)
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();

            // Act
            IPFiltering.Library.IPFilter.AddIPAddress(IPAddressToAdd);

            //Assert
            Assert.IsTrue(IPFiltering.Library.IPFilter.iplist.CheckNumber(IPAddressToAdd));
        }

        [Test]
        public void Can_Add_IPAddress_Range_Seperate_IP_CIDR()
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();

            string IpAddressToCheck = "192.168.1.12";
            string IPRangeToAdd = "192.168.1.1";
            int IPMaskLevel = 24;
            // Act
            IPFiltering.Library.IPFilter.AddIPRange(IPRangeToAdd, IPMaskLevel);

            //Assert
            Assert.IsTrue(IPFiltering.Library.IPFilter.iplist.CheckNumber(IpAddressToCheck));
        }
        [Test]
        public void Can_Add_IPAddress_Range_Joined_IP_CIDR()
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();

            string IpAddressToCheck = "192.168.9.12";
            string IPRangeToAdd = "192.168.9.1/24";

            // Act
            IPFiltering.Library.IPFilter.AddIPRange(IPRangeToAdd);

            //Assert
            Assert.IsTrue(IPFiltering.Library.IPFilter.iplist.CheckNumber(IpAddressToCheck));
        }

        [Test]
        public void IPNetwork_Can_Parse_IPStrings_For_Cidr([Values("192.168.1.3", "192.168.1.3/24")] string IPInput)
        {
            // Act
            int cIDR = IPFiltering.Library.IPFilter.ParseAddressToGetCIDR(IPInput);

            // Assert
            Assert.AreEqual(24, cIDR);
        }

        [Test]
        public void Find_IP_Address_In_List_True([Values("192.168.0.3", "192.168.0.5")] string IPInput)
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();
            bool result = false;

            // Act
            result = IPFiltering.Library.IPFilter.FindAddressInList(IPInput);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Find_IP_Address_In_List_False([Values("192.168.4.3", "192.168.4.5")] string IPInput)
        {
            // Arrange
            if (IPFiltering.Library.IPFilter.iplist == null)
                IPFiltering.Library.IPFilter.CreateTestList();
            bool result = true;

            // Act
            result = IPFiltering.Library.IPFilter.FindAddressInList(IPInput);

            // Assert
            Assert.IsFalse(result);
        }
    }
}