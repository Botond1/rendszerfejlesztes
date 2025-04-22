using System;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Models
{
    /// <summary>
    /// Az öltöny kosárhoz adásához használt adatmodell
    /// </summary>
    public class SuitCartItem
    {
        public int FabricId { get; set; }
        public string Style { get; set; }
        public string Lapel { get; set; }
        public int Buttons { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraPrice { get; set; }
        public string SnapshotUrl { get; set; }
        public string FabricName { get; set; }
        public string HandkerchiefOption { get; set; }
        public string HandkerchiefId { get; set; }
        public string HandkerchiefName { get; set; }
    }
} 