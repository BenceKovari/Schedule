﻿using FinalExamScheduling.Model;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExamScheduling.Schedulers
{
    public class SchedulingFitness : IFitness
    {
        //private int president;
        private List<Instructor> presidents, secretaries, members;

        public SchedulingFitness(List<Instructor> allPresident, List<Instructor> allSecretary, List<Instructor> allMember)
        {
            presidents = allPresident;
            secretaries = allSecretary;
            members = allMember;
        }

        public double GetInstructorAvailableScore(FinalExam fi)
        {
            double score = 0;
            if (fi.supervisor.availability[fi.id] == false)
            {
                    score += 5;               
            }
            if (fi.president.availability[fi.id] == false)
            {
                    score += 1000;
            }
            if (fi.secretary.availability[fi.id] == false)
            {
                    score += 1000;
            }
            if (fi.member.availability[fi.id] == false)
            {
                    score += 5;
            }
            if (fi.examiner.availability[fi.id] == false)
            {
                    score += 1000;
            }

            
            return score;
        }

        public double GetRolesScore(FinalExam fi)
        {
            double score = 0;
            if (fi.supervisor.roles.HasFlag(Role.President) && fi.supervisor != fi.president)
            {
                score += 2;
            }
            if (fi.supervisor.roles.HasFlag(Role.Secretary) && fi.supervisor != fi.secretary)
            {
                score += 1;
            }
            if (fi.examiner.roles.HasFlag(Role.President) && fi.examiner != fi.president)
            {
                score += 1;
            }
        
            return score;
        }

        public double GetPresidentChangeScore(Schedule sch)
        {
            double score = 0;

            for (int i = 0; i < sch.schedule.Count; i+=5)
            {
                if(sch.schedule[i].president != sch.schedule[i + 1].president)
                {
                    score += 1000;
                }
                if (sch.schedule[i+1].president != sch.schedule[i + 2].president)
                {
                    score += 1000;
                }
                if (sch.schedule[i+2].president != sch.schedule[i + 3].president)
                {
                    score += 1000;
                }
                if (sch.schedule[i+3].president != sch.schedule[i + 4].president)
                {
                    score += 1000;
                }

            }
        
            return score;
        }

        public double GetSecretaryChangeScore(Schedule sch)
        {
            double score = 0;

            for (int i = 0; i < sch.schedule.Count; i += 5)
            {
                if (sch.schedule[i].secretary != sch.schedule[i + 1].secretary)
                {
                    score += 1000;
                }
                if (sch.schedule[i + 1].secretary != sch.schedule[i + 2].secretary)
                {
                    score += 1000;
                }
                if (sch.schedule[i + 2].secretary != sch.schedule[i + 3].secretary)
                {
                    score += 1000;
                }
                if (sch.schedule[i + 3].secretary != sch.schedule[i + 4].secretary)
                {
                    score += 1000;
                }

            }

            return score;
        }

        public double GetPresidentWorkloadScore(Schedule schedule)
        {
            double score = 0;
            Dictionary<Instructor, int> presidentWorkload = new Dictionary<Instructor, int>();

            foreach (Instructor instr in presidents)
            {
                presidentWorkload.Add(instr, 0);
            }

            GetPresidentWorkload(schedule, presidentWorkload);

            int optimalWorkload = 100 / presidents.Count;

            foreach (Instructor pres in presidentWorkload.Keys)
            {
                if (presidentWorkload[pres] < optimalWorkload * 0.5)
                {
                    score += 300;
                }
                if (presidentWorkload[pres] < optimalWorkload * 0.3 && presidentWorkload[pres] > optimalWorkload * 0.5)
                {
                    score += 200;
                }
                if (presidentWorkload[pres] < optimalWorkload * 0.1 && presidentWorkload[pres] > optimalWorkload * 0.3)
                {
                    score += 100;
                }

                if (presidentWorkload[pres] > optimalWorkload * 1.5)
                {
                    score += 300;
                }
                if (presidentWorkload[pres] > optimalWorkload * 1.3 && presidentWorkload[pres] < optimalWorkload * 1.5)
                {
                    score += 200;
                }
                if (presidentWorkload[pres] > optimalWorkload * 1.1 && presidentWorkload[pres] < optimalWorkload * 1.3)
                {
                    score += 100;
                }
            }
            
            return score;
        }

        public double GetSecretaryWorkloadScore(Schedule schedule)
        {
            double score = 0;
            Dictionary<Instructor, int> secretaryWorkload = new Dictionary<Instructor, int>();

            foreach (Instructor instr in secretaries)
            {
                secretaryWorkload.Add(instr, 0);
            }

            GetSecretaryWorkload(schedule, secretaryWorkload);

            int optimalWorkload = 100 / secretaries.Count;

            foreach (Instructor secr in secretaryWorkload.Keys)
            {
                if (secretaryWorkload[secr] < optimalWorkload * 0.5)
                {
                    score += 300;
                }
                if (secretaryWorkload[secr] < optimalWorkload * 0.3 && secretaryWorkload[secr] > optimalWorkload * 0.5)
                {
                    score += 200;
                }
                if (secretaryWorkload[secr] < optimalWorkload * 0.1 && secretaryWorkload[secr] > optimalWorkload * 0.3)
                {
                    score += 100;
                }

                if (secretaryWorkload[secr] > optimalWorkload * 1.5)
                {
                    score += 300;
                }
                if (secretaryWorkload[secr] > optimalWorkload * 1.3 && secretaryWorkload[secr] < optimalWorkload * 1.5)
                {
                    score += 200;
                }
                if (secretaryWorkload[secr] > optimalWorkload * 1.1 && secretaryWorkload[secr] < optimalWorkload * 1.3)
                {
                    score += 100;
                }
            }

            return score;
        }

        public double GetMemberWorkloadScore(Schedule schedule)
        {
            double score = 0;
            Dictionary<Instructor, int> memberWorkload = new Dictionary<Instructor, int>();

            foreach (Instructor instr in members)
            {
                memberWorkload.Add(instr, 0);
            }

            GetMemberWorkload(schedule, memberWorkload);

            double optimalWorkload = 100 / members.Count;

            foreach (Instructor memb in memberWorkload.Keys)
            {
                if (memberWorkload[memb] < optimalWorkload * 0.5)
                {
                    score += 300;
                }
                if (memberWorkload[memb] < optimalWorkload * 0.3 && memberWorkload[memb] > optimalWorkload * 0.5)
                {
                    score += 200;
                }
                if (memberWorkload[memb] < optimalWorkload * 0.1 && memberWorkload[memb] > optimalWorkload * 0.3)
                {
                    score += 100;
                }

                if (memberWorkload[memb] > optimalWorkload * 1.5)
                {
                    score += 300;
                }
                if (memberWorkload[memb] > optimalWorkload * 1.3 && memberWorkload[memb] < optimalWorkload * 1.5)
                {
                    score += 200;
                }
                if (memberWorkload[memb] > optimalWorkload * 1.1 && memberWorkload[memb] < optimalWorkload * 1.3)
                {
                    score += 100;
                }
            }

            return score;
        }

        /* public double GetStudentAgainScore(Schedule schedule)
         {

         }*/


        public void GetInstructorPerRoleWorkload(Schedule sch, Dictionary<Instructor, int> presidentWorkload, 
            Dictionary<Instructor, int> secretaryWorkload, Dictionary<Instructor, int> memberWorkload, 
            Dictionary<Instructor, int> examinerWorkload)
        {
            foreach (FinalExam fi in sch.schedule)
            {
                presidentWorkload[fi.president]++;
                secretaryWorkload[fi.secretary]++;
                memberWorkload[fi.member]++;
                examinerWorkload[fi.examiner]++;
            }
        }

        public void GetPresidentWorkload(Schedule sch, Dictionary<Instructor, int> presidentWorkload)
        {

            foreach (FinalExam fi in sch.schedule)
            {
                presidentWorkload[fi.president]++;
            }

        }
        public void GetSecretaryWorkload(Schedule sch, Dictionary<Instructor, int> secretaryWorkload)
        {

            foreach (FinalExam fi in sch.schedule)
            {
                secretaryWorkload[fi.secretary]++;
            }

        }
        public void GetMemberWorkload(Schedule sch, Dictionary<Instructor, int> memberWorkload)
        {

            foreach (FinalExam fi in sch.schedule)
            {
                memberWorkload[fi.member]++;
            }

        }


        public double Evaluate(IChromosome chromosome)
        {

            Schedule sch = new Schedule();
            for (int i = 0; i < 100; i++)
            {
                sch.schedule.Add((FinalExam)chromosome.GetGene(i).Value);
            }



            


            List<Student> studentBefore = new List<Student>();
            double score = 0;
            foreach (FinalExam fi in sch.schedule)
            {
                
                if (studentBefore.Contains(fi.student))
                {
                    score += 10000;
                }
                score += GetInstructorAvailableScore(fi)
                        + GetRolesScore(fi);

                studentBefore.Add(fi.student);
            }

            score += GetPresidentWorkloadScore(sch)
                + GetSecretaryWorkloadScore(sch)
                + GetMemberWorkloadScore(sch)
                + GetPresidentChangeScore(sch)
                + GetSecretaryChangeScore(sch);

            return 10000 - score;

            //return 1 / (
             //       GetInstructorAvailableScore(sch));
        }

    }
}
