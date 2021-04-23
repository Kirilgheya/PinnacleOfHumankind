namespace GameUI.Artificial
{
    public class ShipComponents
    {
        public string Name;

        public bool active = true;

        public double SpeedIncrement = 0;
        
        public int Firepower = 0;
        public bool Flack = false;

        public ShipComponents(string _name, double _SpeedIncrement = 0, int _Firepower = 0, bool _Flack = false)
        {
            Name = _name;
            SpeedIncrement = _SpeedIncrement;
            Firepower = _Firepower;
            Flack = _Flack;
        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals((obj as ShipComponents).Name);
        }
    }
}