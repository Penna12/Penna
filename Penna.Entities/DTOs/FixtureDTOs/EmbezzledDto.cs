using Microsoft.AspNetCore.Mvc.Rendering;
using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class EmbezzledDto
    {
        public FixtureEmbezzled FixtureEmbezzled { get; set; }
        public Fixture Fixture { get; set; }
        public List<Fixture> Fixtures { get; set; }
        public IEnumerable<SelectListItem> AppUsers { get; set; }
    }
}
