namespace GameUI.Artificial
{
    public class ShipComponents
    {
        public string Name;

        public bool active = true;

        public double SpeedIncrement = 0;
        public double Firepower = 0;

        public ShipComponents(string _name, double _SpeedIncrement = 0, double _Firepower = 0)
        {
            Name = _name;
            SpeedIncrement = _SpeedIncrement;
            Firepower = _Firepower;
        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals((obj as ShipComponents).Name);
        }
    }
}