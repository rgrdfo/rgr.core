namespace RGR.Core.Common.Enums
{
    public enum EstateTypes : short
    {
        /// <summary>
        /// Тип не задан
        /// </summary>
        [EnumDescription("Тип не задан")]
        Unset = 99,

        /// <summary>
        /// Квартира
        /// </summary>
        [EnumDescription("Квартира")]
        Flat = 2,

        /// <summary>
        /// Комната
        /// </summary>
        [EnumDescription("Комната")]
        Room = 1,

        /// <summary>
        /// Дом
        /// </summary>
        [EnumDescription("Дом")]
        House = 3,

        /// <summary>
        /// Земля
        /// </summary>
        [EnumDescription("Земля")]
        Land = 4,

        /// <summary>
        /// Коммерческая недвижимость
        /// </summary>
        [EnumDescription("Коммерческая недвижимость")]
        Office = 5,

        /// <summary>
        /// Гараж/парковочное место
        /// </summary>
        [EnumDescription("Парковочное место/гараж")]
        Garage = 6
    }

    public static class EnumExtender
    {
        public static string GetTypeName(this EstateTypes Source)
        {
            switch (Source)
            {
                case EstateTypes.Unset:
                    return "Неизвестный тип";

                case EstateTypes.Flat:
                    return "Квартира";

                case EstateTypes.Room:
                    return "Комната";

                case EstateTypes.House:
                    return "Дом";

                case EstateTypes.Land:
                    return "Участок";

                case EstateTypes.Office:
                    return "Бизнес-объект";

                case EstateTypes.Garage:
                    return "Гараж";

                default:
                    return "Неизвестный тип";
            }
        }
    }
}
