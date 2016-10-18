using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace RGR.Core.Views.Helpers
{
    public class SearchBar
    {
        public static HtmlString Render()
        {
            string s = File.ReadAllText("Views/Shared/_Searchbar.html");            
            return new HtmlString(s);
        }
    }
}
