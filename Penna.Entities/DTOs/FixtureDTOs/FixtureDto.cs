using Penna.Entities.Models;
using System.Collections.Generic;

namespace Penna.Entities.DTOs
{
    public class FixtureDto
    {
        public Fixture Fixture { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }
}
