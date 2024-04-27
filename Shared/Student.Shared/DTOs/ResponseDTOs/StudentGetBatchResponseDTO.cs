namespace Student.Shared.DTOs.ResponseDTOs;

public sealed record StudentGetBatchResponseDTO
{
    public List<StudentGetResponseDTO> Students { get; init; } = [];
}
