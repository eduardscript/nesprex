﻿using Infra.MongoDb.Repositories.Technology;
using Shared.Domain.Entities;

namespace Api.Queries;

public class Query
{
    public Task<IEnumerable<Technology>> GetTechnologiesAsync(
        [Service] ITechnologyRepository technologyRepository,
        TechnologyFilter? filter = null)
    {
        return technologyRepository.GetTechnologiesAsync(filter);
    }
}