using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Technetium.Text.String;

namespace Technetium.Runner;

class Program
{
    static string a = """
                [
                  {
                    "Name": "Alice Johnson",
                    "Age": "28",
                    "Description": "Software engineer with a passion for AI"
                  },
                  {
                    "Name": "Bob Smith",
                    "Age": "35",
                    "Description": "Marketing specialist and hobby photographer"
                  },
                  {
                    "Name": "Charlie Brown",
                    "Age": "42",
                    "Description": "Experienced project manager in construction"
                  },
                  {
                    "Name": "Diana Lee",
                    "Age": "31",
                    "Description": "Freelance graphic designer and illustrator"
                  },
                  {
                    "Name": "Ethan Davis",
                    "Age": "24",
                    "Description": "Recent graduate in environmental science"
                  },
                  {
                    "Name": "Fiona Wilson",
                    "Age": "39",
                    "Description": "Chef and restaurant owner specializing in fusion cuisine"
                  },
                  {
                    "Name": "George Taylor",
                    "Age": "55",
                    "Description": "Retired military officer turned cybersecurity consultant"
                  },
                  {
                    "Name": "Hannah Martinez",
                    "Age": "29",
                    "Description": "Pediatric nurse and children's book author"
                  },
                  {
                    "Name": "Ian Foster",
                    "Age": "33",
                    "Description": "Professional athlete and fitness coach"
                  },
                  {
                    "Name": "Julia Chang",
                    "Age": "47",
                    "Description": "Corporate lawyer with a focus on intellectual property"
                  }
                ]
                """;
    static string b = """
               {
                 "Name": "Alice Johnson",
                 "Age": "28",
                 "Description": "Software engineer with a passion for AI",
                 "Profile": {
                    "Wealth": false,
                    "Phone": 114514,
                    "ID": "awdx",
                    "Nest": { "Inside": "me" },
                    "House": [
                        {
                           "Type": "Villa",
                           "Location": "3"
                        },
                        {
                            "Type": "Apartment",
                            "Location": 1
                        }
                    ]
                 }
               }
               """;
    static async Task Main(string[] args)
    {
        while (true)
        {
            var path = Console.ReadLine();
            var json = b.FetchInt32(path);
            Console.WriteLine(json);
        }
    }

    class MyClass
    {
        public string Name { get; set; }

        public string Age { get; set; }

        public string Description { get; set; }
    }
}

public static class Utils
{
    public static void Output<T>(this IEnumerable<T> list)
    {
        Console.WriteLine($"[{list
            .Select(x => x.ToString())
            .Aggregate((c, n) => $"{c}, {n}")}]");
    }
}