﻿namespace Course.Shared.ResponseDTOs;

public sealed record CourseGetResponseDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public List<string?> StudentNames { get; set; } = [];
}