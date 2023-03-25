// See https://aka.ms/new-console-template for more information

const int HOMEPLANET = 9;
const int MINTRIP = 5;

List<int> StringToDigits(string digits)
{
	var list = new List<int>(); 
	foreach (var c in digits.Replace(",",""))
	{
		list.Add(int.Parse($"{c}"));
	}
	return list;
}

void Main(string planets)
{
	var exoplanets = StringToDigits(planets); 	
	var jumps = getFirstTwo(exoplanets);
	var tripsHome = new Dictionary<string,List<int>>();

	while (jumps.Count > 0)
	{
		var hyperjumps = new List<List<int>>();
		foreach (var g in jumps)
		{
			hyperjumps.AddRange(FindJumps(g, exoplanets));
		}
		jumps = hyperjumps;
	    FindHyperJumpsHome(jumps,exoplanets,HOMEPLANET,MINTRIP,tripsHome);
	}
	
	foreach (var k in tripsHome.Keys)
    {
        Console.WriteLine(k);
    }
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
	var links = new List<int>();
	var a = exoplanets[TakeDigit(g,-2)];
	var b = exoplanets[TakeDigit(g,-1)];

	PlanetCalculations(a,b,links);
	
	if (g.Count > 2)
	{
		var c = exoplanets[TakeDigit(g,-3)] * 10 + a;
		PlanetCalculations(c, b, links);
	}

	return links.Where(d=>d!=0).Distinct().ToList();
}

int TakeDigit(List<int> jumps, int offset)
{
	return jumps.Skip(jumps.Count+offset).Take(1).ToList()[0];
}

void PlanetCalculations(int a, int b, List<int> links)
{
	links.Add(LastDigit(a + b));
	if (a > b)
		links.Add(LastDigit(a - b));
	if (a % b == 0)
		links.Add(LastDigit(a / b));
	links.Add(LastDigit(a * b));
}

int LastDigit(double d)
{
	var str = d.ToString();
	return int.Parse(str.Substring(str.Length-1));
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

if (args.Length > 0)
	Main(args[0]);
else
	Main("3,4,8,4,7,8,62");
