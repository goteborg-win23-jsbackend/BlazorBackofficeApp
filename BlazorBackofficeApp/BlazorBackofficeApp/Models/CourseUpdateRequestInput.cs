namespace BlazorBackofficeApp.Models
{
    public class CourseUpdateRequestInput
    {
        public string Id { get; set; }
        public string ImageUri { get; set; }
        public string ImageHeaderUri { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsDigital { get; set; }
        public List<string> Categories { get; set; }
        public string Title { get; set; }
        public string Ingress { get; set; }
        public int StarRating { get; set; }
        public string Reviews { get; set; }
        public string LikesInPercent { get; set; }
        public string Likes { get; set; }
        public string Hours { get; set; }
        public List<Author> Authors { get; set; }
        public Prices Prices { get; set; }
        public Content Content { get; set; }
    }

    public class AuthorUpdate
    {
        public string Name { get; set; }
    }

    public class PricesUpdate
    {
        public string Currency { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }

    public class Content
    {
        public string Description { get; set; }
        public List<string> Includes { get; set; }
        public List<ProgramDetail> ProgramDetails { get; set; }
    }

    public class ProgramDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
