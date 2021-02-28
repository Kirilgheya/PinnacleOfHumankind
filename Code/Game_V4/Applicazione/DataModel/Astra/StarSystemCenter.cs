using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Astra
{
    public abstract class StarSystemCenter
    {
        protected Star[] stars;
        protected double[] deltaFromBarycenter;

        public double getRelLuminosity()
        {
            double maxRel;

            maxRel = stars.Max(x => x.relluminosity);

            return maxRel;
        }

        public Star[] getStars()
        {

            return stars;
        }

        public void setBarycenter()
        {
            deltaFromBarycenter = new double[stars.Length];
            if (stars.Length > 1)
            {
                Star localStar = stars[0];
                double a, m1, m2, r1, r2;

                a = Converter.UA_to_Km(3);

                m1 = (localStar.Mass);

                localStar = stars[1];
                m2 = (localStar.Mass);

                r1 = a / (1 + (m1 / m2));

                r2 = a - r1;

                deltaFromBarycenter[0] = r1;
                deltaFromBarycenter[1] = r2;
                //r = (a) / (1 + (m1/m2))
                //r = a * (m2)/(m1+m2)
            }
            else
            {

                deltaFromBarycenter[0] = 0;
            }
        }

        public double[] getDistances()
        {

            return this.deltaFromBarycenter;
        }

        protected abstract void childAddStar(Star _star, int _index);
        public abstract int canHaveMore();

        public void addStar(Star _star)
        {
            int nextIndex = this.canHaveMore();
            if (nextIndex >= 0)
            {
                this.childAddStar(_star, nextIndex);
            }
        }

        public abstract string getFullName();

        public abstract string getStarSystemInformation();
    }
}
