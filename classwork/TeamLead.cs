using System;
using System.Collections.Generic;

public class TeamLead : Programmer
{
    private readonly List<Programmer> programmers;

    public TeamLead(int id, List<Programmer> programmers) : base(id)
    {
        this.programmers = programmers;
    }

    public List<Programmer> Programmers
    {
        get { return programmers; }
    }

    public void HuntProgrammers(List<TeamLead> teamLeads)
    {
        foreach(var teamLead in teamLeads)
        {
            for(int i = 0; i < teamLead.Programmers.Count; i++)
            {
                if(teamLead.Programmers[i].LinesOfCode % this.Id == 0)
                {
                    teamLead.Programmers.Remove(teamLead.Programmers[i]);
                    this.Programmers.Add(teamLead.Programmers[i]);
                }
            }
        }
    }

    public int GetWrittenLinesOfCode()
    {
        int linesOfCode = 0;
        foreach(var programmer in programmers)
        {
            linesOfCode += programmer.LinesOfCode;
        }
        linesOfCode += this.LinesOfCode;
        return linesOfCode;
    }

    public override string ToString()
    {
        return String.Format("Team lead #{0}\nAmount of programmers in team: {1}", Id, programmers.Count);
    }
}