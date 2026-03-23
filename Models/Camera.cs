namespace APBDcw2.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public bool HasVideoRecording { get; set; }

    public Camera(int id, string name, int megapixels, bool hasVideoRecording) : base(id, name)
    {
        Megapixels = megapixels;
        HasVideoRecording = hasVideoRecording;
    }

    public override string ToString()
    {
        return base.ToString() + $", MP: {Megapixels}, Video: {HasVideoRecording}";
    }
}