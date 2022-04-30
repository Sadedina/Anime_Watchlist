namespace AnimeBusiness
{
    public class WPFTable
    {
        public string Date { get; set; }
        public string Title { get; set; }
        public string Episode { get; set; }
        public string Watch { get; set; }
        public string Ratings { get; set; }
        public string Status { get; set; }
        public string More { get; set; }

        public override string ToString() =>
            this.Title;
    }
}