using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using ML.Common;

namespace Data.DataContract
{
    [Serializable]
    [DataContract]
    public class BaseFindRequest
    {
        [DataMember]
        public PageOption PageOption { get; set; }

        [DataMember]
        public SortOption SortOption { get; set; }

        public bool PagingIsValid
        {
            get { return PageOption != null && PageOption.IsValid; }
        }

        public bool IsSortingValid
        {
            get { return SortOption != null && SortOption.Items.Count != 0; }
        }

        public IQueryable<T> ApplyPageOption<T>(IQueryable<T> query)
        {
            return PagingIsValid ? query.Skip(PageOption.PageStartIndex).Take(PageOption.PageSize) : query;
        }

        public IEnumerable<T> ApplyPageOption<T>(IEnumerable<T> query)
        {
            return PagingIsValid ? query.Skip(PageOption.PageStartIndex).Take(PageOption.PageSize) : query;
        }

        public IQueryable<T> ApplySortOption<T>(IQueryable<T> query)
        {
            if (!IsSortingValid)
            {
                return query;
            }

            var typeOfT = typeof(T);

            var propSortItems = (from sortItem in SortOption.Items
                                 join propOfT in typeOfT.GetProperties() on sortItem.FieldName.ToLower() equals propOfT.Name.ToLower()
                                 select new { SortProp = propOfT, SortDirection = sortItem.Direction }).ToList();

            if (propSortItems.Count == 0)
            {
                return query;
            }

            var typeParams = new[] { Expression.Parameter(typeof(T), "") };

            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";

            var queryExpression = query.Expression;

            foreach (var propSortItem in propSortItems)
            {
                queryExpression = Expression.Call(
                    typeof(Queryable),
                    propSortItem.SortDirection == SortDirection.Asc ? methodAsc : methodDesc,
                    new[] { typeOfT, propSortItem.SortProp.PropertyType },
                    queryExpression,
                    Expression.Lambda(Expression.Property(typeParams[0], propSortItem.SortProp), typeParams));

                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }

            return query.Provider.CreateQuery<T>(queryExpression);
        }

        public BaseFindResponse<TDestination> GetResponse<T, TDestination>(IQueryable<T> query) where TDestination : class
        {
            var queryPageOption = ApplyPageOption(query);

            return new BaseFindResponse<TDestination>
            {
                TotalRecords = query.Count(),
                Results = queryPageOption.MapEnumerable<T, TDestination>().ToList()
            };
        }

        public BaseFindResponse<TDestination> GetResponse<T, TDestination>(IEnumerable<T> query) where TDestination : class
        {
            var queryPageOption = ApplyPageOption(query);

            return new BaseFindResponse<TDestination>
            {
                TotalRecords = query.Count(),
                Results = queryPageOption.MapEnumerable<T, TDestination>().ToList()
            };
        }
    }
}
