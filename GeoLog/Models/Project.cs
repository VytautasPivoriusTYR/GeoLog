namespace GeoLog.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Borehole> Boreholes { get; set; } = new List<Borehole>();

        public Project(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void AddBorehole(Borehole borehole)
        {
            if (borehole == null)
            {
                return;
            }

            Boreholes.Add(borehole);
            SortBoreholes();
        }

        private void SortBoreholes()
        {
            Boreholes = Boreholes.OrderBy(b => b.Name).ToList();
        }
    }
}
