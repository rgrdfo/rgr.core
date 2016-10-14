using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using Microsoft.AspNetCore.Http;
using Danko.TextJobs;
using Newtonsoft.Json;
using RGR.Core.Controllers.Account;
using Microsoft.EntityFrameworkCore;
using System.Text;

using static System.Net.WebUtility;

namespace RGR.Core.Controllers.Search
{
    public struct ExplainedRequest
    {
        public string Title;
        public string Explanation;
    }

    public static class SearchUtils
    {
        //public static string GetSavedRequests(DbSet<SearchRequests> Requests)
        //{

        //    var ExplainedRequests = Requests.Select(r =>
        //        new ExplainedRequest
        //        {
        //            Title = r.Title,
        //            Explanation = Explain(r.SearchUrl)
        //        });

        //    return JsonConvert.SerializeObject(ExplainedRequests);
        //}

        //private static string Explain(string query)
        //{
        //    var Lexemes = query.Split('?', '=', '&');
        //    Url
        //}
    }
}
