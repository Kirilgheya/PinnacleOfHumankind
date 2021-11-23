using Applicazione.DataModel;
using DataSerialization.SerializationLogic;
using MainGame.Applicazione;
using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;


namespace DataSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StupidSerializer stupidSerializer;

            PeriodicTable.init();

            List<ChemicalElement> elements = PeriodicTable.GetChemicalElements();


            foreach(ChemicalElement e in elements)
            {

                stupidSerializer = new StupidSerializer(e);
                stupidSerializer.serializeData();

            }
        }
    }
}
