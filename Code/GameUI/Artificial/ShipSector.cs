using MainGame.Applicazione.Engine;
using System;
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
            if(this.HP == 0)
            {
                return;
            }
            this.HP = HP - damage;

            if(this.HP < 0)
            {
                this.HP = 0;
            }

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

        public static Random r = SimulationEngine.random;

        public ShipSector(string c, int _HP = 1)
        {
            
            SectorComponents.Add(new ShipComponents(c, 1, 1, r.Next(0, 10) < 5));
            //HP = _HP;
            HP = r.Next(1, 5);
        }
    }
}