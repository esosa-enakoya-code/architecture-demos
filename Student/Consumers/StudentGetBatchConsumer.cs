using AutoMapper;
using MassTransit;
using Student.Data.Repositories;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Student.Consumers;

public sealed class StudentGetBatchConsumer(IMapper mapper, IStudentRepository repository) : IConsumer<IStudentGetBatchContract>
{
    public async Task Consume(ConsumeContext<IStudentGetBatchContract> context)
    {
        var students = await repository.GetBatchAsync(context.Message.Ids);
        var response = mapper.Map<StudentGetBatchResponseDTO>(students);

        await context.RespondAsync(response);
    }
}
