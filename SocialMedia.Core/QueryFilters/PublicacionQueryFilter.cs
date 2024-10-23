namespace SocialMedia.Core.QueryFilters
{
    public class PublicacionQueryFilter
    {
        public int? UserId { get; set; }

        public DateTime? Fecha  { get; set; }

        public string? Descripcion { get; set; }


        public int PageSize { get; set; }


        public int PageNumber { get; set; }
    }
}