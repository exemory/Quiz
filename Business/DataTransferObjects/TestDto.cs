﻿namespace Business.DataTransferObjects;

public class TestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}