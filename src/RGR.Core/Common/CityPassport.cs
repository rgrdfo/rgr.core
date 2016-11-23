using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGR.Core.Common
{
    internal class CityPassport
    {
        private rgrContext db;

        internal CityPassport (rgrContext db)
        {
            this.db = db;
        }

        public CityPassportDTO GetPassport(long CityId)
        {
            var dto = new CityPassportDTO();

            var city = db.GeoCities.FirstOrDefault(c => c.Id == CityId);
            if (city == null)
                throw new ArgumentException("Передан несуществующий индекс города");

            dto.Name = city.Name;

            dto.Districts = db.GeoDistricts
                .Where(g => g.CityId == CityId)
                .ToDictionary(g => g.Id, g => g.Name);

            dto.Areas = db.GeoResidentialAreas
                .Where(g => dto.Districts.ContainsKey(g.DistrictId))
                .GroupBy(g => g.DistrictId)
                .ToDictionary(g => g.Key, g => g.ToDictionary(a => a.Id, a => a.Name));

            dto.Streets = db.GeoStreets
                .Where(g => dto.Areas.ContainsKey(g.AreaId))
                .GroupBy(g => g.AreaId)
                .ToDictionary(g => g.Key, g => g.ToDictionary(s => s.Id, s => s.Name));

            return dto;
        }
    }

    public class CityPassportDTO
    {
        public string Name { get; set; } = "";
        public Dictionary<long, string> Districts  { get; set; } = new Dictionary<long, string>();
        public Dictionary<long, Dictionary<long, string>> Areas { get; set; } = new Dictionary<long, Dictionary<long, string>>();
        public Dictionary<long, Dictionary<long, string>> Streets { get; set; } = new Dictionary<long, Dictionary<long, string>>();
    }
}
