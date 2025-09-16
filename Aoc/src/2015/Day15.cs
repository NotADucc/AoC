using System.Text;

namespace AoC._2015;

public class Day15 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc15.txt");
        string[] instructions = File.ReadAllLines(file_name);
        long res_1 = 0, res_2 = 0;

        var ingredients = instructions
            .Select(extract_instruction)
            .ToList();

        find_best_cookie(
            ingredients: ingredients,
            cookie_ingredients: new Cookie(ingredients),
            total_ingredients: 0,
            exact_calories: -1,
            idx: 0,
            score: ref res_1
        );

        find_best_cookie(
            ingredients:ingredients,
            cookie_ingredients:new Cookie(ingredients),
            total_ingredients:0,
            exact_calories:500,
            idx:0,
            score:ref res_2
        );

        return (res_1, res_2);
    }

    private const int INGREDIENT_MAX_SIZE = 100;
    private static readonly string[] INGREDIENT_PROPERTIES = ["capacity", "durability", "flavor", "texture", "calories"];
    private void find_best_cookie(
        List<Ingredient> ingredients,
        Cookie cookie_ingredients,
        int total_ingredients,
        int exact_calories,
        int idx,
        ref long score
    ) 
    {
        if (total_ingredients == INGREDIENT_MAX_SIZE)
        {
            Dictionary<string, int> property_scores = INGREDIENT_PROPERTIES
                .ToDictionary(x => x, _ => 0);

            foreach (var kvp_cookie in cookie_ingredients.ingredients)
            {
                foreach (var ingredient_property in kvp_cookie.Key.properties)
                {
                    property_scores[ingredient_property.Key] += ingredient_property.Value * kvp_cookie.Value;
                }
            }

            long subscore = 0;
            if (property_scores.All(x => x.Value >= 0))
            {
                subscore = 1;
                foreach (var property_score in property_scores)
                {
                    if (!property_score.Key.Equals("calories"))
                    {
                        subscore *= property_score.Value;
                    }
                }
            }
            if (exact_calories == -1 || exact_calories == property_scores["calories"])
            { 
                score = Math.Max(score, subscore);
            }
            
            return;
        }

        for (int i = idx; i < ingredients.Count; i++)
        {
            var ingredient = ingredients[i];

            cookie_ingredients.ingredients[ingredient]++;
            find_best_cookie(
                ingredients,
                cookie_ingredients,
                total_ingredients + 1,
                exact_calories,
                i,
                ref score
            );
            cookie_ingredients.ingredients[ingredient]--;
        }
    }

    private static Ingredient extract_instruction(string instruction)
    {
        var name_split = instruction
            .Split(':');

        string name = name_split[0];

        var ingredient_split = name_split[1]
            .Split(',', StringSplitOptions.RemoveEmptyEntries);

        var ingredient_obj = new Ingredient()
        {
            name = name,
        };

        foreach (var ingredient in ingredient_split)
        {
            var prop = ingredient
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            ingredient_obj.properties[prop[0]] = int.Parse(prop[1]);
        }

        return ingredient_obj;
    }

    private class Ingredient
    {
        public string name { get; set; }
        public Dictionary<string, int> properties;
        public Ingredient()
        {
            properties = INGREDIENT_PROPERTIES
                .ToDictionary(x => x, _ => 0);
        }

        public override bool Equals(object? obj)
        {
            return obj is Ingredient ingredient &&
                   name == ingredient.name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(name);
        }
    }

    private class Cookie
    {
        public Dictionary<Ingredient, int> ingredients = new Dictionary<Ingredient, int>();

        public Cookie(IEnumerable<Ingredient> ingredients)
        {
            this.ingredients = ingredients.ToDictionary(x => x, _ => 0);
        }

        public string generate_key()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in ingredients)
            {
                sb.Append(item.Key.name);
                sb.Append(item.Value);
            }

            return sb.ToString();
        }
    }
}