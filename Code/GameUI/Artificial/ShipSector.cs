using System.Collections.Generic;

namespace GameUI.Artificial
{
    public class ShipSector
    {
        public int x;
        public int y;

        public List<ShipComponents> SectorComponents = new List<ShipComponents>();


        public ShipSector(string c)
        {
            SectorComponents.Add(new ShipComponents(c));
        }
    }
}