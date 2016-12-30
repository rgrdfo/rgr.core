using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectRatingProperties
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public string Balcony { get; set; }
        public string EntranceDoor { get; set; }
        public string Kitchen { get; set; }
        public string KitchenDescription { get; set; }
        public string Ladder { get; set; }
        public string Loggia { get; set; }
        public long? CommonState { get; set; }
        public string WindowsDescription { get; set; }
        public string UtilityRooms { get; set; }
        public string Floor { get; set; }
        public string Ceiling { get; set; }
        public string Furniture { get; set; }
        public string Wc { get; set; }
        public string Wcdescription { get; set; }
        public int? Rating { get; set; }
        public string Walls { get; set; }
        public string Carpentry { get; set; }
        public string Vestibule { get; set; }
        public bool? Multilisting { get; set; }
        public long? BuildingClass { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}
