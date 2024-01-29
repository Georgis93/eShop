﻿using AutoMapper;
using eShop.Data.Context;

namespace eShop.Data.Services;

public class CategoryDbService(EShopContext db, IMapper mapper) : DbService(db, mapper)
{
    public override async Task<List<TDto>> GetAsync<TEntity, TDto>()
    {
        //IncludeNavigationFor<Filter>();    //tanken är att i en DTO kunde vi ha en lista vi kunde fylla med filter, eller om vi hämtar en categori så vill vi hämta alla produkter med den kategorin. 
        //IncludeNavigationFor<Product>();   // denna metod talar om vilka relaterade entiteter vi vill laddda. Filter och produkter samtidigt t ex. 
        IncludeNavigationsFor<Filter>();
        IncludeNavigationsFor<Product>();
        return await base.GetAsync<TEntity, TDto>();
    }

    /*public async Task<List<CategoryGetDTO>> GetCategoriesWithAllRelatedDataAsync()
    {
        IncludeNavigationsFor<Filter>();
        IncludeNavigationsFor<Product>();
        var categories = await base.GetAsync<Category, CategoryGetDTO>();

        foreach (var category in categories)
        {
            if (category is null || category.Filters is null) continue;

            foreach (var filter in category.Filters)
            {
                filter.Options = [];

                var dbSetProperty = db.GetType().GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(filter.Name, StringComparison.OrdinalIgnoreCase) &&
                        p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

                if (dbSetProperty is null) continue;

                // Retrieve the DbSet and cast it to IQueryable<object>
                var dbSet = (IQueryable<object>?)dbSetProperty.GetValue(db);

                if (dbSet is null) continue;

                var data = await dbSet.ToListAsync();

                filter.Options = _mapper.Map<List<OptionDTO>>(data);
            }
        }

        return categories;
    }
    */
    
}



