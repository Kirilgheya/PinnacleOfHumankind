namespace GameUI.Artificial
{
    public class ShipComponents
    {
        public string Name;

        public bool active = true;

        public double SpeedIncrement = 0;
        
        public int Firepower = 0;
        public bool Flack = false;
        public int range = 1;
        public int cargoSpace = 1;
        public int mass = 10;
        public int shield = 10;

        public ShipComponents(string _name, double _SpeedIncrement = 0, int _Firepower = 0, bool _Flack = false, int _range = 1, int _carcoSpace = 1, int _mass = 10, int _shield = 10)
        {
            Name = _name;
            SpeedIncrement = _SpeedIncrement;
            Firepower = _Firepower;
            Flack = _Flack;
            range = 1;
            cargoSpace = _carcoSpace;
            mass = _mass;
            shield = _shield;
            

        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals((obj as ShipComponents).Name);
        }
    }
}