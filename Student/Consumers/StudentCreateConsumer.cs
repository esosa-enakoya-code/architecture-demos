using AutoMapper;
using MassTransit;
using Student.Data.Repositories;
using Student.Domain;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Student.Consumers;

public sealed class StudentCreateConsumer(IMapper mapper, IStudentRepository repository) : IConsumer<IStudentCreateContract>
{
    public async Task Consume(ConsumeContext<IStudentCreateContract> context)
    {
        var student = mapper.Map<StudentDomain>(context.Message);
        student = await repository.CreateAsync(student);
        var response = mapper.Map<StudentCreateResponseDTO>(student);

        await context.RespondAsync(response);
    }
}
