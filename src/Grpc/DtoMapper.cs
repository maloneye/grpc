using Google.Protobuf.Collections;

namespace Grpc;

public static class DtoMapper
{
    public static EmployeeDto Map(this Employee employee) => new()
    {
        Name = employee.Name,
    };

    public static Employee Map(this EmployeeDto employee) => new Employee(employee.Name);

    public static SkillsDto Map(this IEnumerable<ISkill> skills) => new SkillsDto()
    {
        Skills = { skills.Select(x => x.FullName) },
    };

    public static IReadOnlyList<ISkill> Map(this SkillsDto dto) => dto.Skills.Select(SkillFactory.Build).ToList();
}