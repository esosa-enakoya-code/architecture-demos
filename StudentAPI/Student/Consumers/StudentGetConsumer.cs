using AutoMapper;
using Course.Shared.MessageContracts;
using Course.Shared.ResponseDTOs;
using MassTransit;
using Student.Data.Repositories;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Student.Consumers;

public sealed class StudentGetConsumer(IMapper mapper,IStudentRepository repository, IRequestClient<ICourseGetContract> requestClient) : IConsumer<IStudentGetContract>
{
    public async Task Consume(ConsumeContext<IStudentGetContract> context)
    {
        var student = await repository.GetAsync(context.Message.Id);
        var course = await requestClient.GetResponse<CourseGetResponseDTO>(new { student.Id });

        var response = mapper.Map<StudentGetResponseDTO>(student);
        response.CourseName = course.Message.Name;

        await context.RespondAsync(response);
    }
}
