using APBDcw2.Models;

namespace APBDcw2.Services;

public class EquipmentService
{
    private readonly List<Equipment> _equipmentList = new();

    public void AddEquipment(Equipment equipment)
    {
        _equipmentList.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentList;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentList
            .Where(equipment => equipment.Status == EquipmentStatus.Available)
            .ToList();
    }

    public Equipment? GetById(int id)
    {
        return _equipmentList.FirstOrDefault(equipment => equipment.Id == id);
    }

    public bool MarkAsUnavailable(int equipmentId)
    {
        var equipment = GetById(equipmentId);

        if (equipment == null)
        {
            return false;
        }

        if (equipment.Status == EquipmentStatus.Rented)
        {
            return false;
        }

        equipment.Status = EquipmentStatus.Unavailable;
        return true;
    }
}