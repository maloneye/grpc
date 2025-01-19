using Grpc.Core;
using Microsoft.AspNetCore.Http.Features;

namespace Grpc.Server;

public class SimpleService : Simple.SimpleBase
{
    public override Task<SkillsDto> Request(EmployeeDto request, ServerCallContext context)
    {
        var employee = request.Map();
        var skill = GetSkills(employee);

        return Task.FromResult(skill.Map());
    }
    
    private IEnumerable<ISkill> GetSkills(Employee employee) =>
    [
        new Builder("B-Coder"),
        new Planner("P-Project-Manager"),
    ];
}