namespace APBDcw2.Models;

public class Projector : Equipment
{
    public int BrightnessLumens { get; set; }
    public string Resolution { get; set; }

    public Projector(int id, string name, int brightnessLumens, string resolution) : base(id, name)
    {
        BrightnessLumens = brightnessLumens;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return base.ToString() + $", Jasność: {BrightnessLumens} lm, Rozdzielczość: {Resolution}";
    }
}