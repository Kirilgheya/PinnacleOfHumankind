namespace GameUI.Artificial
{
    public class ShipComponents
    {
        public string Name;

        public bool active = true;

        public ShipComponents(string _name)
        {
            Name = _name;
        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals((obj as ShipComponents).Name);
        }
    }
}