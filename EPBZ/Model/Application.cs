namespace Model
{
    public class Application
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public bool Gender { get; set; } 

        public string Contact { get; set; }

        public string PreferredLocation { get; set; }

        public decimal? ECTC { get; set; }

        public decimal? CCTC { get; set; }

        public int? Notice { get; set; }
    }

    public class AddApplicationModel
    {
        public Application application { get; set; }
        public List<Education> educations { get; set; }
        public List<Work>? works { get; set; }
        public List<Language>? languages { get; set; }
        public List<Skills>? skills { get; set; }
    }

}
