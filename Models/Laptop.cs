namespace APBDcw2.Models;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Processor { get; set; }

    public Laptop(int id, string name, int ramGb, string processor) : base(id, name)
    {
        RamGb = ramGb;
        Processor = processor;
    }

    public override string ToString()
    {
        return base.ToString() + $", RAM: {RamGb} GB, CPU: {Processor}";
    }
}