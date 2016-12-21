using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Controllers.Estate
{
    public enum CheckCode : byte
    {
        /// <summary>
        /// Объект размещён успешно
        /// </summary>
        Ok,

        /// <summary>
        /// Объект переведён в статус "Активный", но существует объект, имеющий совпадения по некоторым полям. Существовавший ранее объект переведён в статус "Черновик"
        /// </summary>
        Replace,

        /// <summary>
        /// Номер квартиры состоит из нулей
        /// </summary>
        ZeroNumber,

        /// <summary>
        /// Существует объект с совпадениями. Замещение невозможно.
        /// </summary>
        ObjectExists,

        /// <summary>
        /// Существует договорной объект с совпадениями. Замещение невозможно.
        /// </summary>
        ContractObjectExists
    }
    public static class EstateUtils
    {
        //public static async Task<CheckCode> CheckAsync(this EstateObjects Source, rgrContext db)
        //{

        //    var curAddress = Source.Addresses.First();
        //    var matchedAddress = await db.Addresses
        //                                .Include(e => e.Object)
        //                                .FirstOrDefaultAsync(a =>
        //                                            a.StreetId == curAddress.StreetId &&
        //                                            a.House == curAddress.House &&
        //                                            a.Block == curAddress.Block &&
        //                                            a.Flat == curAddress.Flat);

        //    var matchedObject = matchedAddress?.Object;
        //    if (matchedObject == null)
        //    {
        //        if (curAddress.Flat.All(c => c == '0'))
        //            return CheckCode.ZeroNumber;

        //        return CheckCode.Ok;
        //    }


        //}
    }
}
