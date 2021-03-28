using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine.CreatureEngine
{
    public static class CreatureEngine
    {
        public static ChemicalElement primaryMeatCompisition;
        public static ChemicalElement SolutionComposition;

        static Random random = new Random();
        static Random random2 = new Random();
        static Random random3 = new Random();
        static Random random4 = new Random();
        static Random random5 = new Random();
        static Random random6 = new Random();

        public static List<Creature> getEcoSystem(Planet p)
        {
            List<Creature> eco = new List<Creature>();

            int n = new Random_Extension().Next(0, 25);

            while(n > 0)
            {
                eco.Add(GetRandomCreatures(p));
                n--;
            }

            return eco;
        }


        public static Creature GetRandomCreatures(Planet p)
        {
            Creature c = new Creature();

            Array values = Enum.GetValues(typeof(Alimentation));
         
            int a = random.Next(values.Length);

            Alimentation al = (Alimentation)values.GetValue(a);

            Array values2 = Enum.GetValues(typeof(Movement));
            random2 = new Random();
            a = random2.Next(values2.Length);
            Movement mov = (Movement)values2.GetValue(a);

            c.alimentation = al;
            c.movement = mov;

            c.Size = new Random_Extension().Next(1, 90);

            Array values3 = Enum.GetValues(typeof(MeasureUnit));         
            a = random3.Next(values3.Length);
            MeasureUnit mes = (MeasureUnit)values3.GetValue(random3.Next(a));
            c.measureSize = mes;

            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            List<KnownColor> nameList = names.ToList();
            List<KnownColor> toremove = new List<KnownColor>();
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("active")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("desktop")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("button")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("highlight")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("border")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("medium")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("control")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("aqua")).ToList());

            foreach (KnownColor cl in toremove)
            {
                nameList.Remove(cl);
            }


            KnownColor randomColorName = nameList[randomGen.Next(nameList.Count)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            c.color = randomColor;

            Array values4 = Enum.GetValues(typeof(Sociality));
            a = random4.Next(values4.Length);
            Sociality soc = (Sociality)values4.GetValue(random4.Next(a));
            c.sociality = soc;

            mouthType mou;
            do
            {
                Array values5 = Enum.GetValues(typeof(mouthType));
                a = random5.Next(values5.Length);
                mou = (mouthType)values5.GetValue(random5.Next(a));
            }
            while (mou == mouthType.fangs && c.alimentation == Alimentation.herbivore);

            
            
            c.mouth = mou;



            Array values6 = Enum.GetValues(typeof(Skin));
            a = random6.Next(values6.Length);
            Skin skin = (Skin)values6.GetValue(random6.Next(a));
            c.skin = skin;

            return c;

        }
        public static Creature GetRandomCreatures(Planet p, Alimentation al)
        {
            Creature c = new Creature();

            c.alimentation = al;

            Array values2 = Enum.GetValues(typeof(Movement));
            Random random2 = new Random();
            int a = random2.Next(values2.Length);
            Movement mov = (Movement)values2.GetValue(a);


            c.movement = mov;

            c.Size = new Random_Extension().Next(1, 90);

            Array values3 = Enum.GetValues(typeof(MeasureUnit));
            Random random3 = new Random();
            a = random3.Next(values3.Length);
            MeasureUnit mes = (MeasureUnit)values3.GetValue(random3.Next(a));
            c.measureSize = mes;

            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            List<KnownColor> nameList = names.ToList();
            List<KnownColor> toremove = new List<KnownColor>();
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("active")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("desktop")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("button")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("highlight")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("border")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("medium")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("control")).ToList());

            foreach (KnownColor cl in toremove)
            {
                nameList.Remove(cl);
            }


            KnownColor randomColorName = nameList[randomGen.Next(nameList.Count)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            c.color = randomColor;

            Array values4 = Enum.GetValues(typeof(Sociality));
            Random random4 = new Random();
            a = random4.Next(values4.Length);
            Sociality soc = (Sociality)values4.GetValue(random4.Next(a));
            c.sociality = soc;

            mouthType mou;
            do
            {
                Array values5 = Enum.GetValues(typeof(mouthType));
                Random random5 = new Random();
                a = random5.Next(values5.Length);
                mou = (mouthType)values5.GetValue(random5.Next(a));
            }
            while (mou == mouthType.fangs && c.alimentation == Alimentation.herbivore);



            c.mouth = mou;



            Array values6 = Enum.GetValues(typeof(Skin));
            Random random6 = new Random();
            a = random6.Next(values6.Length);
            Skin skin = (Skin)values6.GetValue(random6.Next(a));
            c.skin = skin;

            return c;

        }




        public class Creature
        {
            public int Size = 0;
            public MeasureUnit measureSize;
            public Alimentation alimentation = 0;
            public Movement movement = 0;
            public Color color;
            public Sociality sociality;

            public mouthType mouth;
            public moveType move;
            public Skin skin;

            public List<Creature> eat = new List<Creature>();
            public List<Creature> eatenBy = new List<Creature>();

            public String FlavourText
            {
                get
                {
                    String text = "";

                    text = "This " + GetDescription(alimentation) + 
                    " creature is " + Size + " " + GetDescription(measureSize) + 
                    " long covered with a " + Regex.Replace(color.Name, "([a-z])([A-Z])", "$1 $2").ToLower().Replace("medium", "") +" "+GetDescription(skin) +
                    ". Apparently it cosume it's food with "+GetDescription(mouth)+
                    " , usually this specie " + GetDescription(movement) + ", moving around " + GetDescription(sociality);


                    return text;
                }

            }

        }

        public enum Alimentation : int
        {

            [Description("carnivore")]
            carnivore = 0,
            [Description("herbivore")]
            herbivore = 1,
            [Description("omnivorous")]
            omnivorous = 2,

        }
        public enum CarnivoreAlimentation : int
        {

            [Description("hunter")]
            hunter = 0,
            [Description("saprophagous")]
            saprophagus = 1,

        }
        public enum Movement : int
        {

            [Description("walk")]
            walk = 0,
            [Description("fly")]
            fly = 1,
            [Description("swim")]
            swim = 2,
            [Description("dig")]
            dig = 3,
            [Description("swim with fluid propulsion")]
            spropulsion = 4,
            [Description("fly with atmospheric propulsion")]
            fpropulsion = 5,
            [Description("crawl")]
            crawl = 6,
        }

        public enum MeasureUnit : int
        {
            [Description("meters")]
            m = 0,
            [Description("centimeter")]
            cm = 1,
            [Description("millimeters")]
            mm = 2,
        }

        public enum Sociality : int
        {
            [Description("alone")]
            alone = 0,
            [Description("in couple")]
            couple = 1,
            [Description("in small groups")]
            smallGroups = 2,
            [Description("in large groups")]
            largeGroups = 3
        }

        public enum mouthType : int
        {
            [Description("fangs")]
            fangs = 0,
            [Description("chelicerae")]
            chelicerae = 1,
            [Description("teeths")]
            molars = 2,
            [Description("beak")]
            beak = 3,
            [Description("digestive fluid")]
            digestiveFluid = 4
        }

        public enum moveType : int
        {
            [Description("pawn")]
            fangs = 0,
            [Description("wings")]
            chelicerae = 1,
            [Description("fin")]
            molars = 2,
            [Description("tenclacles")]
            beak = 3
        }
        public enum Skin : int
        {

            [Description("fur")]
            fur = 0,
            [Description("skin")]
            skin = 1,
            [Description("chipping")]
            chipping = 2,
            [Description("scale")]
            scale = 3

        }

        public static string GetDescription(Enum value)
        {
            var enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var descriptionAttribute =
                enumMember == null
                    ? default(DescriptionAttribute)
                    : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return
                descriptionAttribute == null
                    ? value.ToString()
                    : descriptionAttribute.Description;
        }

        public static String animalSpecieName(Creature c)
        {
            String name = "";
            String size = "";
            String color = "";

            if(c.color.R  -80 > c.color.G && c.color.R -80 > c.color.B)
            {

                color = "purpuris";
                
            }
            else if (c.color.R - 70 > c.color.G && c.color.R - 70 > c.color.B)
            {

                color = "rubedo";

            }

            if (c.Size > 15 && c.measureSize == MeasureUnit.m)
            {
                size = "gargantus";
            }
            else if (c.Size > 10 && c.measureSize == MeasureUnit.m)
            {
                size = "";
            }

            name = Planet.generate_planet_name()+" "+color + " " + size;

            return name;

        }

    }
}
