namespace GeoLog.Models
{
    public class Borehole
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public List<BoreholeLayer> Layers { get; set; } = new List<BoreholeLayer>();

        public Borehole(string name)
        {
            Name = name;
        }
    }
}
