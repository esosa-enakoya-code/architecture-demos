using AutoMapper;
using MassTransit;
using Student.Data.Repositories;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Student.Consumers;

public sealed class StudentEnrollConsumer(IMapper mapper, IStudentRepository repository) : IConsumer<IStudentEnrollContract>
{
    public async Task Consume(ConsumeContext<IStudentEnrollContract> context)
    {
        var request = context.Message;
        var students = await repository.AddCourseToStudentAsync(request.StudentId, request.CourseId);
        var response = mapper.Map<StudentEnrollResponseDTO>(students);

        await context.RespondAsync(response);
    }
}