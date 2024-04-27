using AutoMapper;
using Course.Data.Repositories;
using Course.Shared.MessageContracts;
using Course.Shared.ResponseDTOs;
using MassTransit;
using Student.Shared.MessageContracts;
using Student.Shared.ResponseDTOs;

namespace Course.Consumers;

public sealed class CourseEnrollConsumer
    (IMapper mapper,
    ICourseRepository repository,
    IRequestClient<IStudentGetContract> getRequestClient,
    IRequestClient<IStudentEnrollContract> enrollRequestClient,
    IRequestClient<IStudentGetBatchContract> getBatchRequestClient) : IConsumer<ICourseEnrollContract>
{
    public async Task Consume(ConsumeContext<ICourseEnrollContract> context)
    {
        var studentResponse = await getRequestClient.GetResponse<StudentGetResponseDTO>(new { Id = context.Message.StudentId });
        var course = await repository.AddStudentToCourseAsync(context.Message.CourseId, studentResponse.Message.Id);
        await enrollRequestClient.GetResponse<StudentEnrollResponseDTO>(new { StudentId = studentResponse.Message.Id, CourseId = course.Id });

        var studentBatchResponse = await getBatchRequestClient.GetResponse<StudentGetBatchResponseDTO>(new { Ids = course.StudentIds });
        var response = mapper.Map<CourseEnrollResponseDTO>(course);
        response.StudentNames = studentBatchResponse.Message.Students.Select(s => s.Name).ToList();

        await context.RespondAsync(response);
    }
}