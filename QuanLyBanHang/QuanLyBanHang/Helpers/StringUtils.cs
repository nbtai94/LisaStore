using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyBanHang.Helpers
{
    public class StringUtils
    {
        public static string Md5(string strInput)
        {
            MD5 md5 = MD5.Create();
            byte[] input = Encoding.Default.GetBytes(strInput);
            byte[] output = md5.ComputeHash(input);
            string ret = BitConverter.ToString(output).Replace("-", "");
            return ret;
        }
    }
}