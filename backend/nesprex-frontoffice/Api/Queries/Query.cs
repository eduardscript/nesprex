﻿namespace Api.Queries;

using HotChocolate.Authorization;
using Infra.MongoDb.Repositories.UsersTechnology;

public class Query
{
	/// <summary>
	///     Get all users selected technologies with its capsules and quantities
	/// </summary>
	/// <param name="userId">The user id</param>
	/// <returns></returns>
	public async Task<UserTechnologies> GetUserTechnologies(
		[FromServices] IUserTechnologiesRepository userTechnologiesRepository,
		Guid userId)
	{
		var result = await userTechnologiesRepository.GetUserTechnologies(userId);

		return result;
	}

	[Authorize(Roles = new[] { "Guest" })]
	public async Task<TestRe> GetGuest() => new("you are a guest");

	[Authorize(Roles = new[] { "Admin" })]
	public async Task<TestRe> GetAdmin() => new("you are an awesome ADMIN!!!!!!!!!!!!!!");
}

public record TestRe(string Test);