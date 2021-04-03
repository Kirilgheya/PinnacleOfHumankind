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


        public static List<Creature> getEcoSystem(Planet p)
        {
               
            List<Creature> eco = new List<Creature>();

            int n =random.Next(0, 40);

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
            a = random.Next(values2.Length);
            Movement mov = (Movement)values2.GetValue(a);

            c.alimentation = al;
            c.movement = mov;


            List<moveType> moveTypeList = Enum.GetValues(typeof(moveType))
                               .Cast<moveType>()
                               .ToList();







            if (mov == Movement.crawl)
            {
                moveTypeList.Remove(moveType.fin);
                moveTypeList.Remove(moveType.pawn);
                moveTypeList.Remove(moveType.tenclacles);
                moveTypeList.Remove(moveType.wings);
            }
            if (mov == Movement.dig)
            {
                moveTypeList.Remove(moveType.fin);
                moveTypeList.Remove(moveType.tenclacles);
                moveTypeList.Remove(moveType.wings);
            }
            if (mov == Movement.fly)
            {
                moveTypeList.Remove(moveType.fin);
                moveTypeList.Remove(moveType.pawn);
                moveTypeList.Remove(moveType.tenclacles);
            }
            if (mov == Movement.apropulsion)
            {
                moveTypeList.Remove(moveType.fin);
                moveTypeList.Remove(moveType.pawn);

            }
            if (mov == Movement.spropulsion)
            {
                moveTypeList.Remove(moveType.pawn);
                moveTypeList.Remove(moveType.wings);

            }
            if (mov == Movement.swim)
            {
                moveTypeList.Remove(moveType.pawn);
                moveTypeList.Remove(moveType.tenclacles);
                moveTypeList.Remove(moveType.wings);

            }
            if (mov == Movement.walk)
            {
                moveTypeList.Remove(moveType.fin);
                moveTypeList.Remove(moveType.tenclacles);
                moveTypeList.Remove(moveType.wings);
            }

            List<Appendix> app = new List<Appendix>();

            foreach(moveType t in moveTypeList)
            {
                int add = random.Next(2);

                int maxCount = 9 ;

                if(moveTypeList.Count > 2)
                {
                    maxCount =  7;
                }

                if(add == 1)
                {
                    int zampe = 0;
                    if (t == moveType.fin)
                    {
                        zampe = random.Next(2, maxCount);
                    }
                    else
                    {
                        zampe = 2 * random.Next(2 / 2, (maxCount / 2) + 1);
                    }

                   app.Add(new Appendix(zampe, t));
                }

                if(mov != Movement.crawl && app.Count == 0)
                {
                    app.Add(new Appendix(2 * random.Next(2 / 2, (maxCount / 2) + 1), moveTypeList.First()));

                }
            }

            c.app = app;

            c.Size = random.Next(1, 95);

            Array values3 = Enum.GetValues(typeof(MeasureUnit));         
            a = random.Next(values3.Length);
            MeasureUnit mes = (MeasureUnit)values3.GetValue(random.Next(a));
            c.measureSize = mes;

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
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("text")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("window")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("frame")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("navajo")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("tomato")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("rod")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("mocassin")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("water")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("olive")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("bar")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("coral")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("info")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("salmon")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("sky")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("spring")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("aqua")).ToList());
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("lime")).ToList());

            foreach (KnownColor cl in toremove)
            {
                nameList.Remove(cl);
            }


            KnownColor randomColorName = nameList[random.Next(nameList.Count)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            c.color = randomColor;

            Array values4 = Enum.GetValues(typeof(Sociality));
            a = random.Next(values4.Length);
            Sociality soc = (Sociality)values4.GetValue(random.Next(a));
            c.sociality = soc;

            mouthType mou;
            do
            {
                Array values5 = Enum.GetValues(typeof(mouthType));
                a = random.Next(values5.Length);
                mou = (mouthType)values5.GetValue(random.Next(a));
            }
            while (mou == mouthType.fangs && c.alimentation == Alimentation.herbivore);

            
            
            c.mouth = mou;



            Array values6 = Enum.GetValues(typeof(Skin));
            a = random.Next(values6.Length);
            Skin skin = (Skin)values6.GetValue(random.Next(a));
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

            public List<Appendix> app = new List<Appendix>();

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
                    " , usually this specie " + GetDescription(movement) + ", moving around " + GetDescription(sociality) + getMoveFlavour();


                    return text;
                }

            }

            private string getMoveFlavour()
            {
                String retval = "";
                if (this.movement == Movement.crawl)
                {
                    return "";
                }
                else
                {
                   for(int n = 0; n< app.Count(); n++)
                    {
                        if(n == 0)
                        {
                            retval = " using their " + app[0].number + " "+ app[0].shape.ToString(); 
                        }
                        else
                        {
                            retval = retval + " and their " + app[n].number + " " + app[n].shape.ToString();
                        }

                    }

                    return retval;
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
            apropulsion = 5,
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
            pawn = 0,
            [Description("wings")]
            wings = 1,
            [Description("fin")]
            fin = 2,
            [Description("tenclacles")]
            tenclacles = 3
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


        public class Appendix
        {
            public int number;
            public moveType shape;

            public Appendix(int _number, moveType _shape)
            {
                number = _number;
                shape = _shape;
            }
        }
    }

  
}
