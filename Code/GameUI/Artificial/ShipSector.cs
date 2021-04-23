using System.Collections.Generic;
using System.Linq;

namespace GameUI.Artificial
{
    public class ShipSector
    {
        public int x;
        public int y;

        public List<ShipComponents> SectorComponents = new List<ShipComponents>();

        public int HP;

        public void allocateDamage(int damage)
        {
            this.HP = HP - damage;

            int n = damage;

           foreach(ShipComponents s in SectorComponents.Where(x => x.active).ToList())
            {
                if(n > 0)
                {
                    s.active = false;

                    n--;
                }
                else
                {
                    return;
                }

            }
        }


        public ShipSector(string c, int _HP = 1)
        {
            SectorComponents.Add(new ShipComponents(c, 1, 1));
            HP = _HP;
        }
    }
}