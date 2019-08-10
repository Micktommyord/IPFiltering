using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace IPFiltering.Library
{
    public class IPFilter
    {
        private Dictionary<string, int> IpAddressList = new Dictionary<string, int>();
        public static int ParseAddressToGetCIDR(string iPInput)
        {
            if (iPInput.Split("/").Length == 2)
                return IPNetwork.Parse(iPInput).Cidr;
            else
                return IPNetwork.Parse(iPInput, CidrGuess.ClassFull).Cidr;
        }

        public static string ConvertToBytesString(string ipInput)
        {
            return String.Join("", ( // join segments
                    ipInput.Split('.').Select( // split segments into a string[]

                    // take each element of array, name it "x",
                    //   and return binary format string
                    x => Convert.ToString(Int32.Parse(x), 2).PadLeft(8, '0')

                    // convert the IEnumerable<string> to string[],
                    // which is 2nd parameter of String.Join
                    )).ToArray());
        }

        public static bool FindAddressInList(string iPBytesString, int cidr)
        {
            throw new NotImplementedException();
        }
    }
}
