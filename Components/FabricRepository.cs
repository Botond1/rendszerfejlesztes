using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetNuke.Common.Utilities;              // DataCache
using SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Models;

namespace SuitCustomizer.Dnn.Dnn.Team5050.SuitCustomizer.Components
{
    public class FabricRepository
    {
        private const string FolderRel =
            "~/DesktopModules/YourRFProject/module-suitcustomizer/" +
            "Dnn.Team5050.SuitCustomizer/Resources/scrape/fabric/";
        private const string CacheKey = "SC_AllFabrics";
        private readonly string _folderPath =
            System.Web.Hosting.HostingEnvironment.MapPath(FolderRel);

        public IEnumerable<Fabric> GetAll()
        {
            if (DataCache.GetCache(CacheKey) is IEnumerable<Fabric> cached)
                return cached;

            var fabrics = Directory
                .EnumerateFiles(_folderPath, "*_huge_c300.jpg")
                .Select(BuildFabric)
                .OrderBy(f => f.Id)
                .ToArray();
             
            DataCache.SetCache(CacheKey, fabrics, DateTime.Now.AddHours(1));
            return fabrics;
        }

        /* ---------- helpers ---------- */

        private Fabric BuildFabric(string hugePath)
        {
            var file = Path.GetFileNameWithoutExtension(hugePath);    // e.g. 82_huge_c300
            var id   = int.Parse(file.Split('_')[0]);

            var thumbName = $"{id}_thumb.jpg";
            var thumbPath = Path.Combine(_folderPath, thumbName);

            if (!File.Exists(thumbPath))
                ImageHelper.GenerateThumb(hugePath, thumbPath, 110, 110);

            return new Fabric
            {
                Id           = id,
                Name         = $"Fabric #{id}",          // placeholder
                ThumbnailUrl = FolderRel + thumbName
            };
        }
    }
} 