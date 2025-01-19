using System.Diagnostics;

namespace Grpc;

public record Employee(string Name);

public interface ISkill
{
    string Name { get; }
    string Prefix { get; }
    string FullName { get; }
}

public class Builder :ISkill
{
    public string Name { get; }
    public string Prefix { get; } = Discriminator.ToString();
    public string FullName { get; }
    
    public const char Discriminator = 'B';


    public Builder(string skill)
    {

        // Usually you'd do some argument validation here but i cba
        var parts = skill.Split('-');
        if (!Prefix.Equals(parts[0],StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("Invalid prefix type for this skill");
        
        Name = parts[1];
        FullName = skill;
    }
}

public class Planner :ISkill
{
    public string Name { get; }
    public string Prefix { get; } = Discriminator.ToString();
    public string FullName { get; }
    
    public const char Discriminator = 'P';

    public Planner(string skill)
    {
        // Usually you'd do some argument validation here but i cba
        var parts = skill.Split('-');
        if (!Prefix.Equals(parts[0],StringComparison.OrdinalIgnoreCase)) throw new ArgumentException("Invalid prefix type for this skill");

        Name = parts[1];
        FullName = skill;
    }
}

public static class SkillFactory
{
    public static ISkill Build(string skill)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(skill);
        ArgumentOutOfRangeException.ThrowIfLessThan(skill.Length,3);

        if (!char.IsLetter(skill[0]) || skill[1] != '-')
        {
            throw new ArgumentException("Provided skill is not in the format of a skill");
        }
        
        skill = skill.ToUpper();
        
        return skill[0] switch
        {
            Builder.Discriminator => new Builder(skill),
            Planner.Discriminator => new Planner(skill),
            _ => throw new ArgumentException("Unsupported prefix"),
        };
    }
}