﻿namespace Application.Services.CarsMake.Models;

public class IntegrationResponse
{
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public int MyProperty { get; set; }
    public List<Result> Results {  get; set; }
}

public class Result
{
    public int Make_ID { get; set; }
    public string Make_Name { get; set; }
    public int Model_ID { get; set; }
    public string  Model_Name { get; set; }
}
