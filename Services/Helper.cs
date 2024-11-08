using System.Security.Cryptography;
using System.Text;

namespace WebApp.Services;

public static class Helper{
    public static string HmacSha512(string plaintext, string key){
        HashAlgorithm algorithm = new HMACSHA512(Encoding.ASCII.GetBytes(key));
        return Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(plaintext)));
    }
}