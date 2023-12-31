using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;

namespace RealEstate.Utilities
{
    public static class PriceConvertor
    {
        public static string ToDollar(this int value)=> value.ToString("#,0");
        public static string ToDollar(this double value) => value.ToString("#,0");

    }
}
