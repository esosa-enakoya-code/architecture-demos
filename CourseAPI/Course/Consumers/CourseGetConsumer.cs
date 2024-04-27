using AutoMapper;
using Course.Data.Repositories;
using Course.Shared.MessageContracts;
using Course.Shared.ResponseDTOs;
using MassTransit;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Course.Consumers;

public sealed class CourseGetConsumer(IMapper mapper, ICourseRepository repository, IRequestClient<IStudentGetBatchContract> requestClient)
    : IConsumer<ICourseGetContract>
{
    public async Task Consume(ConsumeContext<ICourseGetContract> context)
    {
        var request = context.Message;
        var course = await repository.GetAsync(request.Id);
        var studentBatchResponse = await requestClient.GetResponse<StudentGetBatchResponseDTO>(new { Ids = course.StudentIds });

        var response = mapper.Map<CourseGetResponseDTO>(course);
        response.StudentNames = studentBatchResponse.Message.Students.Select(s => s.Name).ToList();

        await context.RespondAsync(response);
    }
}
