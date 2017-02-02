using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.API.Controllers.Estate
{
    public enum ValidationCode : byte
    {
        /// <summary>
        /// Объект прошёл валидацию успешно
        /// </summary>
        Ok,

        /// <summary>
        /// Объект прошёл валидацию, но существует другой объект, имеющий совпадения по некоторым полям. Существовавший ранее объект следует перевести в статус "Черновик"
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
        ContractObjectExists,

        /// <summary>
        /// Требуется указать номер и дату договора
        /// </summary>
        ContractRequired
    }
    public static class EstateUtils
    {
        public static async Task<ValidationCode> ValidateAsync(this EstateObjects Source, rgrContext db)
        {

            var curAddress = Source.Addresses.First();
            var matchedAddress = await db.Addresses
                                        .Include(e => e.Object)
                                        .FirstOrDefaultAsync(a =>
                                                    a.StreetId == curAddress.StreetId &&
                                                    a.House == curAddress.House &&
                                                    a.Block == curAddress.Block &&
                                                    a.Flat == curAddress.Flat);

            var matchedObject = (matchedAddress?.Object.Status == 1) ? matchedAddress?.Object : null;
            if (matchedObject == null)
            {
                if (curAddress.Flat.All(c => c == '0'))
                    return ValidationCode.ZeroNumber;

                return ValidationCode.Ok;
            }

            var addt = db.ObjectAdditionalProperties.First(a => a.ObjectId == Source.Id);

            if (addt.AgreementType != 266)
                return ValidationCode.ObjectExists;

            if (db.ObjectAdditionalProperties.First(a => a.ObjectId == matchedObject.Id).AgreementType == 266)
                return ValidationCode.ContractObjectExists;

            if (!string.IsNullOrEmpty(addt.AgreementNumber) || addt.AgreementStartDate != null)
                return ValidationCode.Replace;

            return ValidationCode.ContractRequired;
        }
    }
}
