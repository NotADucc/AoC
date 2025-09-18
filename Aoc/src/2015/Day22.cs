using System.Xml.Linq;

namespace AoC._2015;

public class Day22 : IRun<long, long>
{
    public (long, long) Run()
    {
        string file_name = Path.Combine(Helper.GetInputFilesDir(), "aoc22.txt");
        long res_1 = long.MaxValue, res_2 = long.MaxValue;

        var input = File.ReadAllLines(file_name);

        var boss = new Entity(
            name: "boss",
            mana: 0,
            hp: int.Parse(input[0].Split(": ")[1]),
            base_damage: int.Parse(input[1].Split(": ")[1])
        );

        var player = new Entity(
            name: "player",
            mana:500,
            hp: 50,
            base_damage: 0
        );

        // it's technically possible that you get the wrong answer
        // increase iteration count for more accurate reading
        for (int i = 0; i < 40_000; i++)
        {
            bool has_won = has_player_won_fight(player, boss, false);

            if (has_won)
            {
                res_1 = Math.Min(res_1, player.mana_spend);
            }
            player.reset_mana_spend();

            has_won = has_player_won_fight(player, boss, true);
            if (has_won)
            { 
                res_2 = Math.Min(res_2, player.mana_spend);
            }
            player.reset_mana_spend();
        }

        return (res_1, res_2);
    }

    private bool has_player_won_fight(Entity player, Entity boss, bool hard_mode)
    {
        bool reset_state(bool has_player_won)
        {
            player.reset_combat_stats(); 
            boss.reset_combat_stats();
            return has_player_won;
        }
        EoTManager eot_manager = new EoTManager(boss:boss, player:player);
        bool player_turn = true;
        
        Spell burn = SpellFactory.Burn;
        EoT eot_burn = burn.effect_on_enemy!();

        if (hard_mode)
        { 
            eot_manager.add_player_effect(eot_burn);
        }

        while (true)
        {
            eot_manager.trigger_boss_effect();
            if (boss.has_died())
            {
                return reset_state(true);
            }
            if (hard_mode && !player_turn)
            {
                eot_manager.remove_player_effect(eot_burn);
                eot_manager.trigger_player_effect();
                eot_manager.insert_player_effect_at_front(eot_burn);
            }
            else 
            {
                eot_manager.trigger_player_effect();
            }
            if (player.has_died())
            {
                return reset_state(false);
            }
            if (player_turn)
            {
                Spell valid_spell = null;
                while (true)
                {
                    bool added_eot_caster = true, added_eot_enemy = true;
                    valid_spell = SpellFactory.get_random_spell();
                    if (valid_spell.effect_on_caster is not null)
                    {
                        added_eot_caster = eot_manager.add_player_effect(valid_spell.effect_on_caster());
                    }
                    if (valid_spell.effect_on_enemy is not null)
                    {
                        added_eot_enemy = eot_manager.add_boss_effect(valid_spell.effect_on_enemy());
                    }
                    if (added_eot_caster && added_eot_enemy)
                        break;
                }

                int dmg = valid_spell.damage;
                if (!player.use_mana(valid_spell.cost))
                {
                    return reset_state(false);
                }

                if (!boss.take_dmg_and_survive(dmg))
                {
                    return reset_state(true);
                }
            }
            else 
            {
                int dmg = boss.base_damage;
                if (!player.take_dmg_and_survive(dmg))
                {
                    return reset_state(false);
                }
            }
            player_turn = !player_turn;
        }
    }
    private class Entity
    {
        public string name { get; init; }
        public int hp { get; set; }
        private readonly int reset_hp;

        public int mana { get; set; }
        private readonly int reset_mana;
        public int mana_spend { get; private set; }

        public int base_damage { get; set; }
        private readonly int reset_base_damage;
        public int base_armour { get; set; }
        public Entity(string name, int hp, int mana, int base_damage)
        {
            this.name = name;
            this.hp = this.reset_hp = hp;
            this.mana = this.reset_mana = mana;
            this.base_damage = this.reset_base_damage = base_damage;
            this.mana_spend = this.base_armour = 0;
        }

        private int calculate_hit(int damage) => Math.Max(damage - base_armour, 1);
        public bool take_dmg_and_survive(int damage)
        {
            if (damage <= 0)
                return !has_died();

            hp -= calculate_hit(damage);
            return hp > 0;
        }
        public bool has_died() => hp <= 0;
        public bool use_mana(int mana)
        {
            if (this.mana < mana)
                return false;

            this.mana -= mana;
            this.mana_spend += mana;

            return true;
        }
        public void reset_combat_stats()
        {
            hp = reset_hp;
            mana = reset_mana;
            base_damage = reset_base_damage;
            base_armour = 0;
        }
        public void reset_mana_spend() => mana_spend = 0;
    }
    private class EoTManager 
    {
        private readonly Entity boss;
        private readonly Entity player;
        private readonly List<EoT> effects_on_boss = new();
        private readonly List<EoT> effects_on_player = new();
        private readonly List<string> working_boss_effects = new();
        private readonly List<string> working_player_effects = new();

        public EoTManager(Entity boss, Entity player)
        {
            this.boss = boss;
            this.player = player;
        }
        public bool remove_boss_effect(EoT effect) => remove_effect(effects_on_boss, effect);
        public bool remove_player_effect(EoT effect) => remove_effect(effects_on_player, effect);
        private bool remove_effect(List<EoT> effects, EoT effect)
        {
            if (!effects.Contains(effect))
                return false;
            effects.Remove(effect);
            return true;
        }
        public bool insert_boss_effect_at_front(EoT effect) => add_effect(effects_on_boss, effect, 0);
        public bool insert_player_effect_at_front(EoT effect) => add_effect(effects_on_player, effect, 0);
        public bool add_boss_effect(EoT effect) => add_effect(effects_on_boss, effect, effects_on_boss.Count);
        public bool add_player_effect(EoT effect) => add_effect(effects_on_player, effect, effects_on_player.Count);
        private bool add_effect(List<EoT> effects, EoT effect, int idx)
        {
            if (effects.Contains(effect))
                return false;
            effects.Insert(idx, effect);
            return true;
        }
        public Entity trigger_boss_effect() => apply_effects(boss, effects_on_boss, false);
        public Entity trigger_player_effect() => apply_effects(player, effects_on_player, true);
        private Entity apply_effects(Entity entity, List<EoT> effects, bool is_player)
        {
            var applied_effects = is_player ? working_player_effects : working_boss_effects;
            for (int i = 0; i < effects.Count; i++)
            {
                var eot = effects[i];
                if (eot.Persistent || !applied_effects.Contains(eot.Name)) 
                {
                    if (!applied_effects.Contains(eot.Name))
                        applied_effects.Add(eot.Name);
                    eot.Effect(entity);
                }
                eot.Rounds--;
                if (eot.Rounds <= 0)
                {
                    if (eot.Cleanup is not null)
                        eot.Cleanup(entity);

                    effects.RemoveAt(i);
                    applied_effects.Remove(eot.Name);
                    i--;
                }
                if (entity.has_died())
                    break;
            }
            return entity;
        }
    }
    private class SpellFactory
    {
        public static readonly Spell MagicMissile = new Spell(
            name: "magic missile",
            cost: 53,
            damage: 4,
            effect_on_caster: null,
            effect_on_enemy: null
        );

        public static readonly Spell Drain = new Spell(
            name: "drain",
            cost: 73,
            damage: 2,
            effect_on_caster: () => new EoT("drain", 1, true, (e) => e.hp += 2, null),
            effect_on_enemy: null
        );

        public static readonly Spell Shield = new Spell(
            name: "shield",
            cost: 113,
            damage: 0,
            effect_on_caster: () => new EoT("shield", 6, false, (e) => e.base_armour += 7, (e) => e.base_armour -= 7),
            effect_on_enemy: null
        );

        public static readonly Spell Poison = new Spell(
            name: "poison",
            cost: 173,
            damage: 0,
            effect_on_caster: null,
            effect_on_enemy: () => new EoT("poison", 6, true, (e) => e.hp -= 3, null)
        );

        public static readonly Spell Recharge = new Spell(
            name: "recharge",
            cost: 229,
            damage: 0,
            effect_on_caster: () => new EoT("recharge", 5, true, (e) => e.mana += 101, null),
            effect_on_enemy: null
        );

        public static readonly Spell Burn = new Spell(
            name: "burn",
            cost: 0,
            damage: 0,
            effect_on_caster: null,
            effect_on_enemy: () => new EoT("burn", int.MaxValue, true, (e) => e.hp -= 1, null)
        );

        private static readonly List<Spell> available_spells = new List<Spell>()
        {
            MagicMissile,
            Drain,
            Shield,
            Poison,
            Recharge
        };
        private static readonly Random random = new Random();
        public static Spell get_random_spell() => available_spells[random.Next(available_spells.Count)];
    }
    private record Spell(
        string name,
        int cost,
        int damage,
        Func<EoT>? effect_on_caster,
        Func<EoT>? effect_on_enemy
    )
    {
        public virtual bool Equals(Spell? obj) => obj is not null && name == obj.name;
        public override int GetHashCode() => HashCode.Combine(name);
    }

    private class EoT(string name, int rounds, bool persistent, Action<Entity> effect, Action<Entity>? cleanup)
    {
        public string Name { get; } = name;
        public int Rounds { get; set; } = rounds;
        public bool Persistent { get; } = persistent;
        public Action<Entity> Effect { get; } = effect;
        public Action<Entity>? Cleanup { get; } = cleanup;
        public override bool Equals(object? obj) => obj is EoT t && Name == t.Name;
        public override int GetHashCode() => HashCode.Combine(Name);
    }
}