using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Common.DTO
{
    public class NewEstateDTO
    {
        public long EstateId { get; set; }
        public long CreatorId { get; set; }

        //Общее
        public short EstateType { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public bool HasParking { get; set; }

        //Информация по дому
        public long HouseType { get; set; }
        public string BuildingMaterial { get; set; }
        public string CellingMaterial { get; set; }
        public int? BuildYear { get; set; }
        public byte FloorCount { get; set; }
        public bool Guard { get; set; }
        public bool Alarm { get; set; }
        public bool Concierge { get; set; }
        public bool TerritoryClosed { get; set; }
        public bool ElevatorPassenger { get; set; }
        public bool ElevatorCargo { get; set; }

        //Информация по квартире
        public byte FloorNumber { get; set; }
        public byte RoomsCount { get; set; }
        public byte RoomsForSaleCount { get; set; }
        public long RoomsType { get; set; }
        public float CommonArea { get; set; }
        public float RoomsArea { get; set; }
        public float KitchenArea { get; set; }
        public string Kitchen { get; set; }
        public long State { get; set; }
        public bool SeparatedWC { get; set; }
        public byte BalconiesCount { get; set; }
        public byte LogiasCount { get; set; }
        public string WindowsLookAt { get; set; }
        
        //Описание
        public string Description { get; set; }

        //Фотографии
        public Dictionary<string, byte[]> Photos { get; set; }

        //Видео
        public string YouTubeLink { get; set; }

        //Условия сделки
        public double Price { get; set; }
        public bool Negotiable { get; set; }//Торг уместен
        public bool Hypothec { get; set; }
        public int Comission { get; set; }
        public long PropertyType { get; set; }
        public long ContractType { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public bool BonusIsAbsolute { get; set; }
        public double Bonus { get; set; }

        //Контакты
        public string ContactUser { get; set; }
        public string Client { get; set; }
    }
}
