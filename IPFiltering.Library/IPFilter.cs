using IPNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IPFiltering.Library
{
    public class IPFilter
    {
        public static IPList iplist;

        public static int ParseAddressToGetCIDR(string iPInput)
        {
            if (iPInput.Split("/").Length == 2)
                return IPNetwork.Parse(iPInput).Cidr;
            else
                return IPNetwork.Parse(iPInput, CidrGuess.ClassFull).Cidr;
        }

        public static void CreateTestList()
        {
            iplist = new IPList();
            iplist.AddRange("192.168.0.1", "192.168.0.156");
        }

        public static bool FindAddressInList(string IPAddress)
        {
            return iplist.CheckNumber(IPAddress);
        }

        public static bool IsAddressValid(string ipToValidate)
        {
            var IPWithCidr = ipToValidate.Split("/");
            if (IPWithCidr.Length == 2)
            {
                int cidr = int.Parse(IPWithCidr[1]);
                if (cidr < 0 || cidr > 32)
                    return false;
            }

            var ipSegments = IPWithCidr[0].Split(".");

            if (ipSegments.Length < 4)
                return false;

            foreach(var segment in ipSegments)
            {
                var intSegment = int.Parse(segment);
                if (intSegment < 0 || intSegment > 255)
                    return false;
            }

            return true;
        }

        public static void AddIPAddress(string iPAddressToAdd)
        {
            if (IsAddressValid(iPAddressToAdd))
                iplist.Add(iPAddressToAdd);
        }

        public static void AddIPRange(string iPRangeToAdd, int iPMaskLevel)
        {
            var fullAddress = string.Join('/', new string[] { iPRangeToAdd, iPMaskLevel.ToString() } );
            if (IsAddressValid(fullAddress))
                iplist.Add(iPRangeToAdd, iPMaskLevel);
        }

        public static void AddIPRange(string iPRangeToAdd)
        {
            if (IsAddressValid(iPRangeToAdd))
            {
                var splitAddress = iPRangeToAdd.Split("/");
                if (splitAddress.Length == 2)
                {
                    int cidr = int.Parse(splitAddress[1]);
                    iplist.Add(splitAddress[0], cidr);
                }
            }
        }
    }
}
