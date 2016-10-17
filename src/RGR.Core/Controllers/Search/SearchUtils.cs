using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using Microsoft.AspNetCore.Http;
//using Danko.TextJobs;
using Eastwing.Parser;
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
        public static string GetSavedRequests(DbSet<SearchRequests> Requests)
        {
            var uriParser = new Parser()
            {
                Letters = "+-АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_%,()",
                Separators = "?&",
                Brackets = ""
            };

            var valParser = new Parser()
            {
                Letters = "",
                Digits = "",
                Separators = ",",
                Brackets = ""
            };

            var ExplainedRequests = Requests.Select(r =>
                new ExplainedRequest
                {
                    Title = r.Title,
                    Explaination = Explain(r.SearchUrl, uriParser, valParser)
                });

            return JsonConvert.SerializeObject(ExplainedRequests);
        }

        private static string Explain(string query, Parser uriParser, Parser valParser)
        {
            var sb = new StringBuilder();

            var tokens = uriParser.Parse(query).Where(t => t.Category != Category.Separator && t.Category != Category.Equals).ToArray();

                //case ("objtype"):
                //    if (tokens[i + 1].Category == Category.Integer)
                //    {
                //        switch (tokens[i + 1].Lexeme)
                //        {
                //            #region Тип объекта
                //            case "0":
                //                sb.Append("Квартиры");
                //                break;

                //            case "1":
                //                sb.Append("Комнаты");
                //                break;

                //            case "2":
                //                sb.Append("Дома");
                //                break;

                //            case "3":
                //                sb.Append("Участки");
                //                break;

                //            case "4":
                //                sb.Append("Офисная недвижимость");
                //                break;

                //            case "5":
                //                sb.Append("Гаражи / парковочные места");
                //                break;
                //                #endregion
                //        }
                //        sb.Append(' ');
                //    }
                //    break;

                //case ("pricefrom"):
                //    if (tokens[i + 1].Category == Category.Integer)
                //    {
                //        if (!sb.ToString().Contains("ценой"))
                //            sb.Append("ценой ");

                //        sb.Append($"от {tokens[i + 1]:### ### ##0}₽");
                //    }
                //    break;

                //case ("priceto"):
                //    if (tokens[i + 1].Category == Category.Integer)
                //    {
                //        if (!sb.ToString().Contains("ценой"))
                //            sb.Append("ценой ");

                //        sb.Append($"до {tokens[i + 1]:### ### ##0}₽");
                //    }
                //    break;
        }
    }
}
