namespace ViecLam.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public DateTime BlogDate { get; set; }
        public string BlogDetail {  get; set; }
    }
}
