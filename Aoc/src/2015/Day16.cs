using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Dynamic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace AoC._2015;

public class Day16 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc16.txt");
        string[] instructions = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;

        var sues = instructions
            .Select(extract_instruction)
            .ToList();

        List<Compound> requirements = new List<Compound>()
        {
            compound_translator("children", 3),
            compound_translator("cats", 7),
            compound_translator("samoyeds", 2),
            compound_translator("pomeranians", 3),
            compound_translator("akitas", 0),
            compound_translator("vizslas", 0),
            compound_translator("goldfish", 5),
            compound_translator("trees", 3),
            compound_translator("cars", 2),
            compound_translator("perfumes", 1)
        };

        foreach (var sue in sues)
        {
            if (sue.meet_requirements(requirements, true))
            {
                res_1 = sue.id;
            }
            else if (sue.meet_requirements(requirements, false))
            {
                res_2 = sue.id;
            }
        }

        return (res_1, res_2);
    }
    private static AuntSue extract_instruction(string instruction)
    {
        var name_split = instruction
            .Split(':', 2);

        int number = int.Parse(name_split[0].Replace("Sue ", ""));
        var compounds = name_split[1]
            .Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        List<Compound> compounds_list = new List<Compound>(); 
        foreach (var compound in compounds)
        {
            var kvp = compound
                .Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int amount = int.Parse(kvp[1]);
            compounds_list.Add(compound_translator(kvp[0], amount));
        }

        return new AuntSue()
        { 
            id = number,
            compounds = compounds_list
        };
    }
    private class AuntSue
    {
        public int id { get; set; }

        public List<Compound> compounds = new List<Compound>();

        public bool meet_requirements(List<Compound> reqs, bool sol_1) 
        {
            foreach (var req in reqs)
            {
                var compound = get_compound(req);
                if (compound is null)
                    continue;

                if (!compound.compare(req, sol_1))
                {
                    return false;
                }
            }

            return true;
        }
        public Compound? get_compound(Compound compound)
        {
            return this.compounds
                .FirstOrDefault(x => x.Equals(compound));
        }
    }
    private static Compound compound_translator(string name, int amount) => name.ToLower() switch
    {
        "children" => new Child(amount),
        "cats" => new Cat(amount),
        "samoyeds" => new Samoyed(amount),
        "pomeranians" => new Pomerian(amount),
        "akitas" => new Akita(amount),
        "vizslas" => new Vizsla(amount),
        "goldfish" => new Goldfish(amount),
        "trees" => new Tree(amount),
        "cars" => new Car(amount),
        "perfumes" => new Perfume(amount),
        _ => throw new ArgumentException(name),
    };
    private class Compound(int amount)
    {
        public int amount { get; set; } = amount;
        public virtual bool compare(Compound compount, bool _)
        {
            if (!this.Equals(compount))
                return false;
            
            return this.amount == compount.amount;
        }
        public override bool Equals(object? obj)
        {
            return obj is Compound compound
                && this.GetType().Name == obj.GetType().Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.GetType().Name);
        }
    }
    private class Child(int amount) : Compound(amount) { }
    private class Cat(int amount) : Compound(amount) 
    {
        public override bool compare(Compound compount, bool sol_1)
        {
            if (!this.Equals(compount))
                return false;

            if (sol_1)
                return this.amount == compount.amount;
            else 
                return this.amount > compount.amount;
        }
    }
    private class Samoyed(int amount) : Compound(amount) { }
    private class Pomerian(int amount) : Compound(amount) 
    {
        public override bool compare(Compound compount, bool sol_1)
        {
            if (!this.Equals(compount))
                return false;

            if (sol_1)
                return this.amount == compount.amount;
            else
                return this.amount < compount.amount;
        }
    }
    private class Akita(int amount) : Compound(amount){ }
    private class Vizsla(int amount) : Compound(amount){ }
    private class Goldfish(int amount) : Compound(amount)
    {
        public override bool compare(Compound compount, bool sol_1)
        {
            if (!this.Equals(compount))
                return false;

            if (sol_1)
                return this.amount == compount.amount;
            else
                return this.amount < compount.amount;
        }
    }
    private class Tree(int amount) : Compound(amount)
    {
        public override bool compare(Compound compount, bool sol_1)
        {
            if (!this.Equals(compount))
                return false;

            if (sol_1)
                return this.amount == compount.amount;
            else
                return this.amount > compount.amount;
        }
    }
    private class Car(int amount) : Compound(amount){ }
    private class Perfume(int amount) : Compound(amount) { }
}