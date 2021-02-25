using System;
using System.Collections.Generic;

public class Company
{
    private readonly List<TeamLead> teamLeads;

    public Company(int teamLeadsAmount, int[] programmersAmount)
    {
        teamLeads = new List<TeamLead>();
        for (int i = 0; i < teamLeadsAmount; i++)
        {
            List<Programmer> programmers = new List<Programmer>();
            for (int j = 0; j < programmersAmount[i]; j++)
            {
                programmers.Add(new Programmer(j + 1));
            }
            teamLeads.Add(new TeamLead(i + 1, programmers));
        }
    }

    public List<TeamLead> TeamLeads
    {
        get { return teamLeads; }
    }

    public void PrintTeams()
    {
        for (int i = 0; i < teamLeads.Count; i++)
        {
            Console.WriteLine(teamLeads[i]);
        }
    }
}