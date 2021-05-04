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

        public ShipComponents(string _name, double _SpeedIncrement = 0, int _Firepower = 0, bool _Flack = false, int _range = 1)
        {
            Name = _name;
            SpeedIncrement = _SpeedIncrement;
            Firepower = _Firepower;
            Flack = _Flack;
            range = 1;
        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals((obj as ShipComponents).Name);
        }
    }
}