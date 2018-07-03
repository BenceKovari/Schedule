﻿using FinalExamScheduling.Model;
using FinalExamScheduling.Schedulers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExamScheduling
{
    public class Program
    {

        public static Context context = new Context();

        static void Main(string[] args)
        {
            FileInfo existingFile = new FileInfo(@"D:\BME\8.félév\Kutatás\Input.xlsx");
           
            ExcelHelper eh = new ExcelHelper();
            eh.Read(existingFile, context);

            context.addID();

            GeneticScheduler genSch = new GeneticScheduler(context);
            Schedule sch = genSch.Run();


            eh.Write(@"D:\BME\8.félév\Kutatás\Done.xlsx", sch);


            Console.WriteLine();

        }

    }
}
