using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Models
{
    // Fabric modell
    [TableName("SuitCustomizer_Fabrics")]
    [PrimaryKey("FabricId", AutoIncrement = true)]
    [Cacheable("Fabrics", CacheItemPriority.Default, 20)]
    //
    public class Fabric
    {
        public int FabricId { get; set; } = -1;
        public string Name { get; set; }
        public string Category { get; set; } // Standard, Luxury, Exclusive
        public string HexColor { get; set; }
        public string ThumbnailPath { get; set; }
        public bool IsActive { get; set; } = true;
        public int ExtraPrice { get; set; } = 0; // Felár forintban
    }

    // Öltöny szabás
    [TableName("SuitCustomizer_Styles")]
    [PrimaryKey("StyleId", AutoIncrement = true)]
    [Cacheable("Styles", CacheItemPriority.Default, 20)]
    public class Style
    {
        public int StyleId { get; set; } = -1;
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ExtraPrice { get; set; } = 0;
    }

    // Zseb típus
    [TableName("SuitCustomizer_Pockets")]
    [PrimaryKey("PocketId", AutoIncrement = true)]
    [Cacheable("Pockets", CacheItemPriority.Default, 20)]
    public class Pocket
    {
        public int PocketId { get; set; } = -1;
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ExtraPrice { get; set; } = 0;
    }

    // Zsebkendő
    [TableName("SuitCustomizer_PocketSquares")]
    [PrimaryKey("SquareId", AutoIncrement = true)]
    [Cacheable("PocketSquares", CacheItemPriority.Default, 20)]
    public class PocketSquare
    {
        public int SquareId { get; set; } = -1;
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ExtraPrice { get; set; } = 3900; // Alapértelmezett felár: 3900 Ft
    }

    // Teljes öltöny konfiguráció
    [TableName("SuitCustomizer_Configurations")]
    [PrimaryKey("ConfigId", AutoIncrement = true)]
    [Cacheable("SuitConfigurations", CacheItemPriority.Default, 20)]
    [Scope("ModuleId")]
    public class SuitConfiguration
    {
        public int ConfigId { get; set; } = -1;
        public int UserId { get; set; }
        public int FabricId { get; set; }
        public int StyleId { get; set; }
        public int PocketId { get; set; }
        public int? SquareId { get; set; } // Opcionális
        public int TotalPrice { get; set; }
        public string Note { get; set; }
        public int ModuleId { get; set; }
        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;
    }
} 