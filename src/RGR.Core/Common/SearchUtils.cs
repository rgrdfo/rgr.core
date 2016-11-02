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

namespace RGR.Core.Common
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
                    Explanation = Explain(r.SearchUrl, uriParser, valParser)
                });

            return JsonConvert.SerializeObject(ExplainedRequests);
        }

        //генерация описания запроса на основе строки запроса
        private static string Explain(string query, Parser uriParser, Parser valParser)
        {
            var sb = new StringBuilder();
            var dict = new Dictionary<string, string>();

            //Парсинг строки запроса, исключение пробелов и разделителей
            var tokens = uriParser.Parse(query).Where(t => t.Category != Category.Separator && t.Category != Category.Space).ToList();

            //Построение словаря
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Category == Category.Equals)
                {
                    //Незатейливая проверка, что текущий ключ не пустой
                    if (i + 2 < tokens.Count)
                        if (tokens[i + 2].Category == Category.Equals)
                            continue;

                    //Добавление в словарь ключа и его значения
                    dict.Add(tokens[i - 1].Lexeme.ToUpper(), tokens[i + 1].Lexeme);
                }
            }

            # region Расшифровка словаря
            if (dict.ContainsKey("OBJTYPE"))
            {
                switch (dict["OBJTYPE"])
                {
                    #region Тип объекта
                    case "0":
                        sb.Append("Квартиры");
                        break;

                    case "1":
                        sb.Append("Комнаты");
                        break;

                    case "2":
                        sb.Append("Дома");
                        break;

                    case "3":
                        sb.Append("Участки");
                        break;

                    case "4":
                        sb.Append("Офисная недвижимость");
                        break;

                    case "5":
                        sb.Append("Гаражи / парковочные места");
                        break;
                        #endregion
                }
                sb.Append(' ');
            }

            if (dict.ContainsKey("PRICEFROM"))
            {
                sb.Append($"по цене от {dict["PRICEFROM"]:### ### ##0}₽ ");
            }

            if (dict.ContainsKey("PRICETO"))
            {
                string prefix = (dict.ContainsKey("PRICEFROM")) ? "" : prefix = "по цене ";
                sb.Append($"{prefix}до {dict["PRICETO"]:### ### ##0}₽, ");
            }

            if (dict.ContainsKey("LIVINGSQUAREFROM"))
            {
                sb.Append($"жилой площадью от {dict["LIVINGSQUAREFROM"]:### ###.00} м² ");
            }

            if (dict.ContainsKey("LIVINGSQUARETO"))
            {
                string prefix = (dict.ContainsKey("LIVINGSQUAREFROM")) ? "" : "жилой площадью ";
                sb.Append($"{prefix}до {dict["LIVINGSQUARETO"]:### ###.00} м² ");
            }

            if (dict.ContainsKey("MINFLOOR"))
            {
                bool maxSetted = dict.ContainsKey("MAXFLOOR");

                string prefix  = maxSetted ? "на" : "не ниже";
                string postfix = maxSetted ? "-" : " этажа";

                sb.Append($", {prefix} {dict["MINFLOOR"]}{postfix}");
            }

            if (dict.ContainsKey("MAXFLOOR"))
            {
                bool minSetted = dict.ContainsKey("MINFLOOR");

                string prefix  = minSetted ? "" : ", не выше ";
                string postfix = minSetted ? "этажах" : "этажа";

                sb.Append($"{prefix}{dict["MAXFLOOR"]} {postfix} ");
            }

            if (dict.ContainsKey("MINHOUSEFLOORS"))
            {
                string postfix = dict.ContainsKey("MAXHOUSEFLOORS") ? " этажей" : "";
                sb.Append($"в доме от {dict["MINHOUSEFLOORS"]}{postfix}");
            }

            if (dict.ContainsKey("MAXHOUSEFLOORS"))
            {
                string prefix = dict.ContainsKey("MINHOUSEFLOORS") ? "в доме до " : "";
                sb.Append($"{prefix}{dict["MAXHOUSEFLOORS"]} этажей, ");
            }

            if (dict.ContainsKey("WC"))
            {
                sb.Append(dict["WC"].ToUpper() == "SEP" ? ", с раздельным санузлом" : ", со смежным санузлом");
            }



            #endregion

            return sb.ToString();
        }
    }
}
