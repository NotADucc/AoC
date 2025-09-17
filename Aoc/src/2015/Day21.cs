namespace AoC._2015;

public class Day21 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc21.txt");
        long res_1 = long.MaxValue, res_2 = long.MinValue;

        var input = File.ReadAllLines(file_name);

        var boss = new Entity(
            name: "boss",
            hp: int.Parse(input[0].Split(": ")[1]),
            base_damage: int.Parse(input[1].Split(": ")[1]),
            base_armour: int.Parse(input[2].Split(": ")[1])
        );

        var player = new Entity(
            name: "player",
            hp: 100,
            base_damage: 0,
            base_armour: 0
        );

        foreach (var weapon in Shop.weapons)
        {
            foreach (var armour in Shop.armour)
            {
                foreach (var jewel1 in Shop.jewellery)
                {
                    foreach (var jewel2 in Shop.jewellery)
                    {
                        if (jewel1 == jewel2)
                            continue;

                        player.weapon = weapon;
                        player.armour = armour;
                        player.jewels = [jewel1, jewel2];

                        bool can_win = can_player_win_fight(player, boss);

                        if (can_win)
                        {
                            res_1 = Math.Min(res_1, player.get_price());
                        }
                        else 
                        { 
                            res_2 = Math.Max(res_2, player.get_price());
                        }
                    }
                }
            }
        }

        return (res_1, res_2);
    }
    private bool can_player_win_fight(Entity player, Entity boss)
    {
        bool player_turn = true;
        while (true)
        {
            var attacker = player;
            var defender = boss;
            if (!player_turn)
            {
                attacker = boss;
                defender = player;
            }
            int dmg = attacker.get_damage();
            if (!defender.take_dmg_and_survive(dmg))
            {
                break;
            }
            player_turn = !player_turn;
        }
        player.reset(); boss.reset();
        return player_turn;
    }
    private class Entity 
    {
        public string name { get; set; }
        public int hp { get; set; }
        private readonly int reset_hp;
        public int base_damage { get; set; }
        public int base_armour { get; set; }
        public Weapon weapon = new Weapon("None", 0, 0);
        public Armour armour = new Armour("None", 0, 0);
        public List<Jewel> jewels = [];

        public Entity(string name, int hp, int base_damage, int base_armour)
        {
            this.name = name;
            this.hp = this.reset_hp = hp;
            this.base_damage = base_damage;
            this.base_armour = base_armour;
        }

        public int get_damage()
        {
            int dmg = base_damage;
            dmg += weapon.damage;
            foreach (var jewel in jewels)
                dmg += jewel.damage;

            return dmg;
        }
        public int calculate_hit(int damage)
        {
            damage -= base_armour;
            damage -= armour.armour;
            foreach (var jewel in jewels)
                damage -= jewel.armour;

            return Math.Max(damage, 1);
        }

        public bool take_dmg_and_survive(int damage)
        {
            hp -= calculate_hit(damage);
            return hp > 0;
        }
        public int get_price() 
        {
            int price = 0;
            price += weapon.cost;
            price += armour.cost;
            foreach (var jewel in jewels)
                price += jewel.cost;

            return price;
        }

        public void reset() 
        {
            hp = reset_hp;
        }
    }
    private class Shop
    {
        public static readonly HashSet<Weapon> weapons = new()
        {
            new Weapon("Dagger",    8, 4),
            new Weapon("Shortsword",10,5),
            new Weapon("Warhammer", 25,6),
            new Weapon("Longsword", 40,7),
            new Weapon("Greataxe ", 74,8),
        };

        public static readonly HashSet<Armour> armour = new()
        {
            new Armour("None", 0, 0),
            new Armour("Leather",   13     ,1),
            new Armour("Chainmail", 31     ,2),
            new Armour("Splintmail",53     ,3),
            new Armour("Bandedmail",75     ,4),
            new Armour("Platemail", 102    ,5),
        };

        public static readonly HashSet<Jewel> jewellery = new()
        {
            new Jewel("None", 0, 0, 0),
            new Jewel("None 2", 0, 0, 0),
            new Jewel("Damage +1",    25     ,1       ,0),
            new Jewel("Damage +2",    50     ,2       ,0),
            new Jewel("Damage +3",   100     ,3       ,0),
            new Jewel("Defense +1",   20     ,0       ,1),
            new Jewel("Defense +2",   40     ,0       ,2),
            new Jewel("Defense +3",   80     ,0       ,3),
        };
    }
    private record Weapon(string name, int cost, int damage);
    private record Armour(string name, int cost, int armour);
    private record Jewel(string name, int cost, int damage, int armour);
}