namespace WebAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKM { get; set; }
        public string? WalkImageUrl { get; set; }

        // relations to the other entities

        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }

        public Guid RegionId { get; set; }
        public Region Region { get; set; }


    }
}
