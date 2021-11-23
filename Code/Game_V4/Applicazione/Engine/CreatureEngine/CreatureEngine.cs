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

        static Random random = SimulationEngine.random;


        public static List<Creature> GenerateEcoSystem(Planet p)
        {
               
            List<Creature> eco = new List<Creature>();

            int n =random.Next(0, 40);

            while(n > 0)
            {
                eco.Add(GenerateRandomCreaure(p));
                n--;
            }

            List<Creature> carnivoreList = eco.Where(x => x.alimentation == Alimentation.carnivore).ToList();
            List<Creature> herbivoreList = eco.Where(x => x.alimentation == Alimentation.herbivore).ToList();

            if(carnivoreList.Count > 0 && herbivoreList.Count == 0)
            {
                foreach(MovementType m in Enum.GetValues(typeof(MovementType))) 
                {
                    eco.Add(GenerateRandomCreaure(p, m, Alimentation.herbivore));
                }
            }


            foreach (Creature preda in herbivoreList)
            {
                foreach (Creature predatore in carnivoreList)
                {
                    if(predatore.MajorInSize(preda) || predatore.sociality == Sociality.largeGroups || predatore.sociality == Sociality.smallGroups)
                    {
                        foreach (Creature c in eco)
                        {
                            if(c.habitats.Intersect(preda.habitats) != null) 
                            { 
                            if (c == preda)
                            {
                                c.eatenBy.Add(predatore);
                            }
                            if (c == predatore)
                            {
                               c.eat.Add(preda);
                            }
                            }
                        }
                      
                    }
                    else if (predatore.GreatmajorInSize(preda))
                    {
                        foreach (Creature c in eco)
                        {
                            if (c.habitats.Intersect(preda.habitats) != null)
                            {
                                if (c == preda)
                                {
                                    c.eatenBy.Add(predatore);
                                }
                                if (c == predatore)
                                {
                                    c.eat.Add(preda);
                                }
                            }
                        }
                    }
                }
            }

            return eco;
        }


        public static Creature GenerateRandomCreaure(Planet p, MovementType _moveType = MovementType.Unidentified, Alimentation _alimentation = Alimentation.Unidentified)
        {


            Creature c = new Creature();
            int n;


            Alimentation al;
            if (_alimentation == Alimentation.Unidentified)
            {
                Array AlimentatioArray = Enum.GetValues(typeof(Alimentation));
                List<Alimentation> AlimentationList = AlimentatioArray.OfType<Alimentation>().Where(x => x != Alimentation.Unidentified).ToList();
                n = random.Next(AlimentationList.Count());
                al = AlimentationList[n];
            }
            else
            {
                al = _alimentation;
            }

            MovementType mov;
            if (_moveType == MovementType.Unidentified)
            {
                Array MovementTypeArray = Enum.GetValues(typeof(MovementType));
                List<MovementType> MovementTypeList = MovementTypeArray.OfType<MovementType>().Where(x => x != MovementType.Unidentified).ToList();
                n = random.Next(MovementTypeList.Count);
                mov = MovementTypeList[n];
            }
            else
            {
                mov = _moveType;
            }
            c.alimentation = al;
            c.MovementType = mov;

            c.app = RandomAppendixFromMoveType(mov);

            c.SetHabitat();

            c.Size = random.Next(1, 95);

            Array MeaseureUnitArray = Enum.GetValues(typeof(MeasureUnit));
            List<MeasureUnit> MeaseureUnitList = MeaseureUnitArray.OfType<MeasureUnit>().Where(x => x != MeasureUnit.Unidentified).ToList();
            n = random.Next(MeaseureUnitList.Count());
            MeasureUnit mes = MeaseureUnitList[random.Next(n)];
            c.measureSize = mes;

        

            c.color = GetRandomCreatureColor();

            Array SocialityArray = Enum.GetValues(typeof(Sociality));
            List<Sociality> SocialityList = SocialityArray.OfType<Sociality>().Where(x => x != Sociality.Unidentified).ToList();
            n = random.Next(SocialityList.Count);
            Sociality soc = SocialityList[random.Next(n)];
            c.sociality = soc;

            mouthType mou;
            do
            {
                Array mouthTypeArray = Enum.GetValues(typeof(mouthType));
                List<mouthType> MouthTypeList = mouthTypeArray.OfType<mouthType>().Where(x => x != mouthType.Unidentified).ToList();
                n = random.Next(MouthTypeList.Count);
                mou = MouthTypeList[random.Next(n)];
            }
            while ((mou == mouthType.fangs && c.alimentation == Alimentation.herbivore) || (mou == mouthType.chelicerae && c.MovementType == MovementType.flyFeather));



            c.mouth = mou;



            Array SkinArray = Enum.GetValues(typeof(Skin));
            List<Skin> SkinList = SkinArray.OfType<Skin>().Where(x => x != Skin.Unidentified).ToList();
            if (c.MovementType == MovementType.airPropulsion || c.MovementType == MovementType.swimPropulsion)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.feather);
                SkinList.Remove(Skin.scale);
            }
            if (c.MovementType == MovementType.flyFeather)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.skin);
                SkinList.Remove(Skin.scale);
            }
            if (c.MovementType == MovementType.flymembranous)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.feather);
            }
            if (c.MovementType == MovementType.crawl)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.feather);
            }
            if (c.MovementType == MovementType.swim)
            {
                SkinList.Remove(Skin.feather);
            }
            if (c.app.Select(x => x.shape == MovementAppendix.fins).FirstOrDefault() == null)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.feather);
            }
            if (c.mouth == mouthType.chelicerae)
            {
                SkinList.Remove(Skin.fur);
                SkinList.Remove(Skin.feather);
            }
            n = random.Next(SkinList.Count);
            Skin skin = SkinList[random.Next(n)];
            c.skin = skin;

          

            c.extra = GenerateExtraAppendixCount(c);

            return c;

        }

        private static List<ExtraAppendixCount> GenerateExtraAppendixCount(Creature c)
        {
            Array ExtraAppendixArray = Enum.GetValues(typeof(ExtraAppendix));
            List<ExtraAppendix> ExtraAppendixList = ExtraAppendixArray.OfType<ExtraAppendix>().Where(x => x != ExtraAppendix.Unidentified).ToList();

            if (c.MovementType == MovementType.swim)
            {
                ExtraAppendixList.Remove(ExtraAppendix.horns);
                ExtraAppendixList.Remove(ExtraAppendix.trunk);
                ExtraAppendixList.Remove(ExtraAppendix.poisonskin);
            }
            if (c.MovementType == MovementType.flyFeather || c.MovementType == MovementType.flymembranous)
            {
                ExtraAppendixList.Remove(ExtraAppendix.horns);
                ExtraAppendixList.Remove(ExtraAppendix.horn);
                ExtraAppendixList.Remove(ExtraAppendix.poisonskin);
                ExtraAppendixList.Remove(ExtraAppendix.electricGhiandola);
                ExtraAppendixList.Remove(ExtraAppendix.Sheel);
                ExtraAppendixList.Remove(ExtraAppendix.pincers);
                ExtraAppendixList.Remove(ExtraAppendix.spines);
                ExtraAppendixList.Remove(ExtraAppendix.StingedTail);
                ExtraAppendixList.Remove(ExtraAppendix.boneplating);

            }
            if (c.MovementType == MovementType.airPropulsion || c.MovementType == MovementType.swimPropulsion)
            {
                ExtraAppendixList.Remove(ExtraAppendix.horns);
                ExtraAppendixList.Remove(ExtraAppendix.Sheel);
                ExtraAppendixList.Remove(ExtraAppendix.pincers);
                ExtraAppendixList.Remove(ExtraAppendix.StingedTail);
                ExtraAppendixList.Remove(ExtraAppendix.boneplating);
            }
            if(c.MovementType == MovementType.crawl)
            {
                ExtraAppendixList.Remove(ExtraAppendix.horns);
                ExtraAppendixList.Remove(ExtraAppendix.electricGhiandola);
                ExtraAppendixList.Remove(ExtraAppendix.pincers);
                ExtraAppendixList.Remove(ExtraAppendix.spines);
            }
            if (c.MovementType == MovementType.dig)
            {
                ExtraAppendixList.Remove(ExtraAppendix.horns);
                ExtraAppendixList.Remove(ExtraAppendix.poisonskin);
                ExtraAppendixList.Remove(ExtraAppendix.electricGhiandola);
                ExtraAppendixList.Remove(ExtraAppendix.Sheel);
                ExtraAppendixList.Remove(ExtraAppendix.pincers);
                ExtraAppendixList.Remove(ExtraAppendix.spines);
                ExtraAppendixList.Remove(ExtraAppendix.StingedTail);
            }
            if(c.mouth != mouthType.chelicerae && c.mouth != mouthType.chelicerae)
            {
                ExtraAppendixList.Remove(ExtraAppendix.poisonfang);
            }

            List<ExtraAppendixCount> toAdd = new List<ExtraAppendixCount>();
            foreach (ExtraAppendix t in ExtraAppendixList)
            {
                int add = random.Next(9);

                if(add == 1)
                {
                    if(t == ExtraAppendix.horns || t == ExtraAppendix.pincers || t == ExtraAppendix.poisonfang)
                    {
                        toAdd.Add(new ExtraAppendixCount(2, t));
                    }
                    else if(t == ExtraAppendix.bioluminescent || t == ExtraAppendix.boneplating || t == ExtraAppendix.poisonskin || t == ExtraAppendix.Sheel || t == ExtraAppendix.stings || t == ExtraAppendix.LongTail 
                            || t == ExtraAppendix.StingedTail || t == ExtraAppendix.NeuralSpineSail)
                    {
                        toAdd.Add(new ExtraAppendixCount(0, t)); // non numerabile
                    }
                    else
                    {
                        toAdd.Add(new ExtraAppendixCount(1, t)); // 1 numerabile
                    }
                }
            }

            return toAdd;
        }

        private static Color GetRandomCreatureColor()
        {
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
            toremove.AddRange(nameList.Where(x => Color.FromKnownColor(x).Name.ToLower().Contains("app")).ToList());

            foreach (KnownColor cl in toremove)
            {
                nameList.Remove(cl);
            }


            KnownColor randomColorName = nameList[random.Next(nameList.Count)];
            Color randomColor = Color.FromKnownColor(randomColorName);

            return randomColor;
        }

        private static List<MoveAppendixCount> RandomAppendixFromMoveType(MovementType mov)
        {
            List<MovementAppendix> moveTypeList = Enum.GetValues(typeof(MovementAppendix))
                               .Cast<MovementAppendix>()
                               .Where(x => x != MovementAppendix.Unidentified).ToList();

            if (mov == MovementType.crawl)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.featherWings);
                moveTypeList.Remove(MovementAppendix.Membranewings);
            }
            if (mov == MovementType.dig)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.featherWings);
                moveTypeList.Remove(MovementAppendix.Membranewings);
            }
            if (mov == MovementType.flymembranous)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.featherWings);
            }
            if (mov == MovementType.flyFeather)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.Membranewings);
            }
            if (mov == MovementType.airPropulsion)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.Membranewings);

            }
            if (mov == MovementType.swimPropulsion)
            {
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.featherWings);
                moveTypeList.Remove(MovementAppendix.Membranewings);

            }
            if (mov == MovementType.swim)
            {
                moveTypeList.Remove(MovementAppendix.pawn);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.featherWings);
                moveTypeList.Remove(MovementAppendix.Membranewings);

            }
            if (mov == MovementType.walk)
            {
                moveTypeList.Remove(MovementAppendix.fins);
                moveTypeList.Remove(MovementAppendix.tentacles);
                moveTypeList.Remove(MovementAppendix.featherWings);
                moveTypeList.Remove(MovementAppendix.Membranewings);
            }

            List<MoveAppendixCount> app = new List<MoveAppendixCount>();

            foreach (MovementAppendix t in moveTypeList)
            {
                int add = random.Next(2);

                int maxCount = 9;

                if (moveTypeList.Count > 2)
                {
                    maxCount = 7;
                }

                if (add == 1)
                {
                    int zampe = 0;
                    if (t == MovementAppendix.fins)
                    {
                        zampe = random.Next(2, maxCount);
                    }
                    else
                    {
                        zampe = 2 * random.Next(2 / 2, (maxCount / 2) + 1);
                    }

                    app.Add(new MoveAppendixCount(zampe, t));
                }

                if (mov != MovementType.crawl && app.Count == 0)
                {
                    app.Add(new MoveAppendixCount(2 * random.Next(2 / 2, (maxCount / 2) + 1), moveTypeList.First()));

                }
            }

            return app;
        }



        public class Creature
        {

                

            public String Name;
            public int Size = 0;
            public MeasureUnit measureSize;
            public Alimentation alimentation = 0;
            public MovementType MovementType = 0;
            public Color color;
            public Sociality sociality;
            public double SizeInCm
            {
                get
                {
                    double toreturn = 1;
                    double mult = 1;

                    if(measureSize == MeasureUnit.m)
                    {
                        mult = 100;
                    }
                    else if (measureSize == MeasureUnit.mm)
                    {
                        mult = 0.01;
                    }

                    return Size * mult;
                }
            }

            public mouthType mouth;
            public MoveAppendixCount move;
            public List<ExtraAppendixCount> extra;
            public Skin skin;

            public List<MoveAppendixCount> app = new List<MoveAppendixCount>();

            public List<Creature> eat = new List<Creature>();
            public List<Creature> eatenBy = new List<Creature>();

            public List<Habitat> habitats = new List<Habitat>();

            public Creature()
            {
                Name = GeneratRandomName();
            }

            public override string ToString()
            {
                return this.Name;
            }

            public String FlavourText
            {
                get
                {
                    String text = "";

                    text =  Name  + "\n This " + GetDescription(alimentation) + 
                    " creature is " + Size + " " + GetDescription(measureSize) + 
                    " long covered with a " + Regex.Replace(color.Name, "([a-z])([A-Z])", "$1 $2").ToLower().Replace("medium", "") +" "+GetDescription(skin) +
                    ". Apparently it cosume it's food with "+GetDescription(mouth)+
                    " , usually this specie " + GetDescription(MovementType) + ", moving around " + GetDescription(sociality) + getMoveFlavour() + " " + getExtraFlavour() +" "+ getAlimentaryChainString() + " " + gethabitatString() + "\n\n";




                    return text;
                }

            }

            public void SetHabitat()
            {
                if(this.MovementType != MovementType.swimPropulsion && this.MovementType != MovementType.swim)
                {
                    this.habitats.Add(Habitat.Terrestrial);
                }
                else if(this.MovementType != MovementType.walk && this.MovementType != MovementType.dig)
                {
                    this.habitats.Add(Habitat.Marine);
                }
            } 

            private string getAlimentaryChainString()
            {
                String text = "";

                if (eatenBy.Count == 0 && eat.Count != 0)
                {
                    text = "This alpha predators usually eat " + String.Join(" ", eat.Select(x => x.Name));
                }
               else if(eatenBy.Count != 0 && eat.Count != 0)
                {
                    text = "this creature eats " + String.Join(" ", eat.Select(x => x.Name)) + " and are eaten by " + String.Join(" ", eatenBy.Select(x => x.Name));
                }
                else if (eatenBy.Count != 0)
                {
                    text = "this creature are eaten by " + String.Join(" ", eatenBy.Select(x => x.Name));
                }

                return text;
            }

            private String gethabitatString()
            {

                if (habitats.Contains(Habitat.Marine) && habitats.Contains(Habitat.Terrestrial))
                {
                    return "this creature can be found in marine and terrestrial habitats";
                }
                else if (habitats.Contains(Habitat.Marine))
                {
                    return "this creature is mainly marine";
                }
                else
                {
                    return "this creature is mainly terestrial";
                }
            }

            internal bool GreatmajorInSize(Creature preda)
            {

                return this.SizeInCm / preda.SizeInCm > 2;
            }

            internal bool MajorInSize(Creature preda)
            {
                return this.SizeInCm / preda.SizeInCm > 1;
            }

            private string getMoveFlavour()
            {
                String retval = "";
                if (this.MovementType == MovementType.crawl)
                {
                    return "";
                }
                else
                {
                   for(int n = 0; n< app.Count(); n++)
                    {
                        if(n == 0)
                        {
                            retval = " using their " + app[0].number + " " + GetDescription(app[0].shape);
                        }
                        else
                        {
                            retval = retval + " and their " + app[n].number + " " + GetDescription(app[n].shape);
                        }

                    }

                    return retval;
                }
            }
            private string getExtraFlavour()
            {


                String retval = "";

                int n = 0;
                foreach (ExtraAppendixCount t in this.extra)
                {


                    if (n == 0)
                    {
                        retval = "this creature exhibits ";
                    }

                    if (t.number == 1 || t.number == 2)
                    {
                        retval = retval + t.number + " " + GetDescription(t.shape);
                    }
                    if (t.number == 0)
                    {
                        retval = retval +" " + GetDescription(t.shape);
                    }
                    if (n != this.extra.Count && n != this.extra.Count - 1)
                    {
                        retval = retval + ", ";
                    }
                    n++;
                }

                return retval;
            }


        }

        public enum Alimentation : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("carnivore")]
            carnivore = 0,
            [Description("herbivore")]
            herbivore = 1,
            [Description("omnivorous")]
            omnivorous = 2,

        }
        public enum CarnivoreAlimentation : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("hunter")]
            hunter = 0,
            [Description("saprophagous")]
            saprophagus = 1,

        }
        public enum MovementType : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("walk")]
            walk = 0,
            [Description("fly with featherd wings")]
            flyFeather = 1,
            [Description("swim")]
            swim = 2,
            [Description("dig")]
            dig = 3,
            [Description("swim with fluid propulsion")]
            swimPropulsion = 4,
            [Description("fly with atmospheric propulsion")]
            airPropulsion = 5,
            [Description("crawl")]
            crawl = 6,
            [Description("fly with membranous")]
            flymembranous = 7,
        }

        public enum MeasureUnit : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("meters")]
            m = 0,
            [Description("centimeter")]
            cm = 1,
            [Description("millimeters")]
            mm = 2,
        }

        public enum Sociality : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
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
            [Description("Unidentified")]
            Unidentified = -1,
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

        public enum MovementAppendix : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("pawn")]
            pawn = 0,
            [Description("membranous wings")]
            Membranewings = 1,
            [Description("fins")]
            fins = 2,
            [Description("tentacles")]
            tentacles = 3,
            [Description("feathered wings")]
            featherWings = 4,
        }
        public enum ExtraAppendix : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description(" long tail")]
            LongTail = 0,
            [Description("a stinged tail")]
            StingedTail = 1,
            [Description("a trunk")]
            trunk = 2,
            [Description("horn")]
            horn = 3,
            [Description("a frontal bone plating ")]
            boneplating = 4,
            [Description("a sting")]
            stings = 5,
            [Description("a spined body")]
            spines = 6,
            [Description(" a neural spine sail")]
            NeuralSpineSail = 7,
            [Description("a shell")]
            Sheel = 8,
            [Description("pincers")]
            pincers = 9,
            [Description("horns")]
            horns = 10,
            [Description("an electric ghiandola")]
            electricGhiandola = 11,
            [Description("a poisonous skin")]
            poisonskin = 12,
            [Description("poisonus fangs")]
            poisonfang = 13,
            [Description("a bioluminescent aura")]
            bioluminescent = 14,
        }

        public enum Habitat : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("Marine")]
            Marine = 0,
            [Description("Terrestrial")]
            Terrestrial = 1,
        }

        public enum Skin : int
        {
            [Description("Unidentified")]
            Unidentified = -1,
            [Description("fur")]
            fur = 0,
            [Description("skin")]
            skin = 1,
            [Description("scale")]
            scale = 2,
            [Description("feather")]
            feather = 3

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


        public class MoveAppendixCount
        {
            public int number;
            public MovementAppendix shape;
            public MoveAppendixCount(int _number, MovementAppendix _shape)
            {
                number = _number;
                shape = _shape;
            }
        }

        public class ExtraAppendixCount
        {
            public int number;
            public ExtraAppendix shape;

            public ExtraAppendixCount(int _number, ExtraAppendix _shape)
            {
                number = _number;
                shape = _shape;
            }
        }

        public static String GeneratRandomName()
        {
            
            string[] nameComponent1 = new string[] { "Ge", "Me", "Ta", "Bo", "Ke", "Ra", "Ne", "Mi" };
            string[] nameComponent2 = new string[] { "oo", "ue", "as", "to", "ra", "me", "io", "so" };
            string[] nameComponent3 = new string[] { "se", "matt", "lace", "fo", "care", "enb" };

            string nameCompfirst = nameComponent1[random.Next(0, nameComponent1.Length)];
            string nameCompSecond = nameComponent2[random.Next(0, nameComponent2.Length)];
            string nameCompThird = nameComponent3[random.Next(0, nameComponent3.Length)];

            string result = nameCompfirst + nameCompSecond + nameCompThird;

            return result;
        }
    }

  
}
