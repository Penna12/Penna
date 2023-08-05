using Penna.Core.Entities;

namespace Penna.Entities.DTOs
{
    public class GenerateApartmentParamsDto : IDto
    {
        public int FloorCount { get; set; }

        public int StartFloorNo{ get; set; }

        public int NumberOfHousesOnEachFloor { get; set; }

        public int StartApartmentNo { get; set; }

        public float Gross { get; set; }

        public float Net { get; set; }

        public float Gabari { get; set; }
    }
}
