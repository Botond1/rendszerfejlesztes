using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Models
{
    /// <summary>Csak a bemutató gridhez szükséges DTO.</summary>
    public class Fabric
    {
        public int Id { get; set; }
        public string Name { get; set; }           // pl. „Twill Navy”
        public string ThumbnailUrl { get; set; }   // „~/DesktopModules/MVC/…/Resources/fabric/82_thumb.jpg”
    }
}
