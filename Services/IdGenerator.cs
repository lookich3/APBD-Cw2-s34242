namespace APBDcw2.Services;

public class IdGenerator
{
    private int _currentId = 1;

    public int GetNextId()
    {
        return _currentId++;
    }
}