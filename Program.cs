// Quanta Puzzle Solver
using System.Reflection.Metadata.Ecma335;

var program = new QuantaSolver();
if (args.Length > 1)
    program.Solve(args[1]);
else
    Console.WriteLine("No planets provided");
//

public class QuantaSolver
{
    const int MINTRIP = 3;

    List<int> StringToDigits(string digits)
    {
        var list = new List<int>(); 
        foreach (var c in digits.Replace(",",""))
        {
            list.Add(int.Parse($"{c}"));
        }
        return list;
    }

    public void Solve(string planets)
    {
        var tripsHome = GetTripsHome(planets);
        foreach (var trip in tripsHome)
        {
            Console.WriteLine(trip);
        }
    }

    public List<string> GetTripsHome(string planets) {
        var exoplanets = StringToDigits(planets);  
        var homeplanet = exoplanets.Last();
        exoplanets.Remove(homeplanet);  
        var jumps = getFirstTwo(exoplanets);
        var tripsHome = new Dictionary<string,List<int>>();

        while (jumps.Count > 0)
        {
            var hyperjumps = new List<List<int>>();
            foreach (var g in jumps)
            {
                hyperjumps.AddRange(FindJumps(g, exoplanets));
            }
            jumps=hyperjumps;
            FindHyperJumpsHome(jumps,exoplanets,homeplanet,MINTRIP,tripsHome);
        }
        return tripsHome.Keys.ToList();
    }
    void FindHyperJumpsHome(List<List<int>> jumps, List<int> exoplanets, int homeplanet, int mintrip, Dictionary<string,List<int>> trips)
    {
        foreach (var j in jumps)
        {
            if (j.Count < mintrip)
                return;
                
            if (CalculatePlanetValues(j, exoplanets).Contains(homeplanet))
            {
                var path = string.Join("", j.Select(j => exoplanets[j]).ToList());
                if (!trips.ContainsKey(path))
                {
                    trips.Add(path, j);
                }
            }
        }
    }

    List<List<int>> FindJumps(List<int> g, List<int> exoplanets)
    {
        var unvisited = Enumerable.Range(0,exoplanets.Count).Except(g).ToList();
        var reachable = NextReachableJumps(g, unvisited, exoplanets);
        return reachable;
    }

    List<List<int>> NextReachableJumps(List<int> g, List<int> unvisited, List<int> exoplanets)
    {
        var next = new List<List<int>>();
        var nextPlanets = CalculatePlanetValues(g, exoplanets);
        foreach (var n in nextPlanets)
        {
            foreach (var idx in unvisited)
            {
                if (exoplanets[idx] == n)
                {
                    var jump = new List<int>();
                    jump.AddRange(g);
                    jump.Add(idx);
                    next.Add(jump);
                }
            }
        }
        return next;
    }

    List<int> CalculatePlanetValues(List<int> g, List<int> exoplanets)
    {
        var a = exoplanets[TakeDigit(g,-2)];
        var b = exoplanets[TakeDigit(g,-1)];
        return PlanetCalculations(a,b);
    }

    int TakeDigit(List<int> jumps, int offset)
    {
        return jumps[jumps.Count+offset];
    }

    List<int> PlanetCalculations(int a, int b)
    {
        var links = new List<int>();
        links.Add(LastDigit(a + b));
        if (a > b)
            links.Add(LastDigit(a - b));
        if (a % b == 0)
            links.Add(LastDigit(a / b));
        links.Add(LastDigit(a * b));
        return links.Where(d=>d!=0).Distinct().ToList();
    }

    int LastDigit(double d)
    {
        return (int) d % 10;
    }

    List<List<int>> getFirstTwo(List<int> planets)
    {
        var pairs = new List<List<int>>();
        for (int i = 0; i < planets.Count; i++)
        {
            for (int j = 0; j < planets.Count; j++)
            {
                if (i != j)
                {
                    var pair = new List<int>();
                    pair.Add(i);
                    pair.Add(j);
                    pairs.Add(pair);
                }            
            }
        }
        return pairs;
    }

}
