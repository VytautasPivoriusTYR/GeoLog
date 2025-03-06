namespace GeoLog.Models
{
    public class BoreholeLayer
    {
        public Guid Guid { get; set; }
        public string Description { get; set; }
        public double Thickness { get; set; }

        public BoreholeLayer(string description, double thickness)
        {
            Description = description;
            Thickness = thickness;
            Guid = Guid.NewGuid();
        }
    }
}
