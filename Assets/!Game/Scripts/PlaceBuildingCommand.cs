using UnityEngine;

public class PlaceBuildingCommand : ICommand
{
    private Building _building;
    private int _buildingPrice;
    public PlaceBuildingCommand(Building building, int price)
    {
        _building = building;
        _buildingPrice = price;
    }

    public void Execute()
    {
        _building.Place();
    }

    public void Undo()
    {
        if (_building != null)
        {
            GameObject.Destroy(_building.gameObject);
        }   
        PlayerWallet.Instance.Add(_buildingPrice);
    }
}