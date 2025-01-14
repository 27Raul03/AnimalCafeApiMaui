namespace WebAPI.Models
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public byte[]? AnimalPhoto { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Health { get; set; }
    }
}
