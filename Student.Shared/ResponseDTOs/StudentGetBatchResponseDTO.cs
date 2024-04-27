namespace Student.Shared.ResponseDTOs;

public sealed record StudentGetBatchResponseDTO
{
    public List<StudentGetResponseDTO> Students { get; init; } = [];
}
