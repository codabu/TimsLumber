using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimsLumber.Models.SeedData
{
    internal class SeedLumberItems : IEntityTypeConfiguration<LumberItem>
    {
        public void Configure(EntityTypeBuilder<LumberItem> entity)
        {
            entity.HasData(
                new LumberItem { LumberItemId = 1, NominalSize = "1x2", ActualSize = "3/4\" × 1-1/2\"", PricePerFt = 0.55},
                new LumberItem { LumberItemId = 2, NominalSize = "1x3", ActualSize = "3/4\" × 2-1/2\"", PricePerFt = 0.65},
                new LumberItem { LumberItemId = 3, NominalSize = "1x4", ActualSize = "3/4\" × 3-1/2\"", PricePerFt = 0.75},
                new LumberItem { LumberItemId = 4, NominalSize = "1x6", ActualSize = "3/4\" × 5-1/2\"", PricePerFt = 0.85},
                new LumberItem { LumberItemId = 5, NominalSize = "1x8", ActualSize = "3/4\" × 7-1/4\"", PricePerFt = 0.95},
                new LumberItem { LumberItemId = 6, NominalSize = "1x10", ActualSize = "3/4\" × 9-1/4\"", PricePerFt = 1.05},
                new LumberItem { LumberItemId = 7, NominalSize = "2x2", ActualSize = "1-1/2\" × 1-1/2\"", PricePerFt = 0.98},
                new LumberItem { LumberItemId = 8, NominalSize = "2x3", ActualSize = "1-1/2\" × 2-1/2\"", PricePerFt = 1.08},
                new LumberItem { LumberItemId = 9, NominalSize = "2x4", ActualSize = "1-1/2\" × 3-1/2\"", PricePerFt = 1.18},
                new LumberItem { LumberItemId = 10, NominalSize = "2x6", ActualSize = "1-1/2\" × 5-1/2\"", PricePerFt = 1.28},
                new LumberItem { LumberItemId = 11, NominalSize = "2x8", ActualSize = "1-1/2\" × 7-1/4\"", PricePerFt = 1.38},
                new LumberItem { LumberItemId = 12, NominalSize = "2x10", ActualSize = "1-1/2\" × 9-1/4\"", PricePerFt = 1.48}
            );
        }
    }
}
