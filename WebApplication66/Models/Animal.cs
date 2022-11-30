using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication66.Models
{
    public class QueryParams
    {
        public string[] par { get; set; }
    }

    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SkinColorId { get; set; }
        public SkinColor SkinColor { get; set; }

        public int KindOfAnimalId { get; set; }
        public KindOfAnimal KindOfAnimal { get; set;}

        public int LocationId { get; set; }
        public Location Location { get; set;}

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }

    public class SkinColor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class KindOfAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Region
    {
        public int Id { get; set; }
        public int Code { get; set; }
    }
}
