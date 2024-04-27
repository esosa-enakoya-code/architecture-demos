using AutoMapper;
using Course.Data.Repositories;
using Course.Domain;
using Course.Shared.MessageContracts;
using Course.Shared.ResponseDTOs;
using MassTransit;

namespace Course.Consumers;

public sealed class CourseCreateConsumer(IMapper mapper, ICourseRepository repository) : IConsumer<ICourseCreateContract>
{
    public async Task Consume(ConsumeContext<ICourseCreateContract> context)
    {
        var course = mapper.Map<CourseDomain>(context.Message);
        course = await repository.CreateAsync(course);
        var response = mapper.Map<CourseCreateResponseDTO>(course);

        await context.RespondAsync(response);
    }
}
