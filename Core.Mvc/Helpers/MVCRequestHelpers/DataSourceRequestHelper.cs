using Core.Cmn.FarsiUtils;
using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mvc.Helpers.MVCRequestHelpers
{
    public static class DataSourceRequestHelper
    {
        private static Dictionary<FilterDescriptor, CompositeFilterDescriptor> _convertedSingleFilters;
        public static void RefineFilterValues(this IList<IFilterDescriptor>  filters )
        {
            _convertedSingleFilters = new Dictionary<FilterDescriptor, CompositeFilterDescriptor>();
            if(filters.Count > 0 )
            {
                var mainFilterObj = filters.First();
                //mainFilterObj is FilterDescriptor
                if (mainFilterObj is FilterDescriptor)
                {
                    var result = AmendFilterValuesForSingleFilterDescriptor(mainFilterObj as FilterDescriptor);
                    if (result != null)
                    {
                        filters.RemoveAt(0);
                        filters.Add(result);
                    }
                }
                //mainFilterObj is CompositeFilterDescriptor
                else 
                {
                    AmendFilterValuesForCompositeFilterDescriptor(mainFilterObj as CompositeFilterDescriptor);
                }
            }
        }

        private static void AmendFilterValuesForCompositeFilterDescriptor(CompositeFilterDescriptor compositeFilterDescriptor)
        {
            var filterConditions = compositeFilterDescriptor.FilterDescriptors;
            Dictionary<FilterDescriptor , CompositeFilterDescriptor> convertedSingleFilters = null;

            foreach (var filterItem in filterConditions)
            {
                if(filterItem is CompositeFilterDescriptor)
                {
                   AmendFilterValuesForCompositeFilterDescriptor(filterItem as CompositeFilterDescriptor);
                }
                else
                {
                    var fItem = filterItem as FilterDescriptor;
                    var result = AmendFilterValuesForSingleFilterDescriptor(fItem);
                    
                    if (result != null )
                    {
                        if (result is CompositeFilterDescriptor)
                        {
                            convertedSingleFilters = convertedSingleFilters ?? new Dictionary<FilterDescriptor, CompositeFilterDescriptor>();
                            convertedSingleFilters.Add(fItem, result as CompositeFilterDescriptor);
                        }

                        else
                        {
                            result = fItem;
                        }
                    }
                }
            }

            if (convertedSingleFilters != null)
            {
                foreach (var singleFilterItem in convertedSingleFilters.Keys)
                {
                    filterConditions[filterConditions.IndexOf(singleFilterItem)] = convertedSingleFilters[singleFilterItem];
                }
            }
        }


        private static IFilterDescriptor AmendFilterValuesForSingleFilterDescriptor(IFilterDescriptor filterDescriptor)
        {
            IFilterDescriptor retVal = null;
            var fItem = (filterDescriptor as FilterDescriptor);
            var filterValObj = fItem.Value;
            if (filterValObj.GetType() == typeof(string))
            {
                string filterVal = filterValObj.ToString();
                
                if(filterVal.Contains(",dt"))
                {
                    switch (fItem.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            retVal = getDateRangeCompositeFilterConditionForEquality(fItem, filterVal);
                            break;
                        case FilterOperator.IsGreaterThan:
                            getDateRangeCompositeFilterConditionForGreaterThan(fItem, filterVal);
                            break;
                        case FilterOperator.IsLessThan:
                            getDateRangeCompositeFilterConditionForLessThan(fItem, filterVal);
                            break;
                        case FilterOperator.IsNotEqualTo:
                            retVal = getDateRangeCompositeFilterConditionForInEquality(fItem, filterVal);
                            break;
                        default:
                            break;
                    }
                }
               
                else if (filterVal.Contains(",nv"))
                {
                       //TODO:
                            //1.Update both of "ConvertedValue" and "Value" Properties to the desired value from currently multi secion data of these fields.
                            //2.Update "Member" Property value.
                    retVal = new FilterDescriptor();
                    var newValue = fItem.ConvertedValue.ToString().Split(',');//.Split(":")
                    fItem.Member = newValue[1].Split(':')[1];
                    //fItem.ConvertedValue = newValue[0];
                    fItem.Value = newValue[0];
                    retVal = fItem;
                }
            }

            else
            {
                retVal = filterDescriptor;

            }

            return retVal;
        }

        private static CompositeFilterDescriptor getDateRangeCompositeFilterConditionForInEquality(FilterDescriptor fItem, string filterVal)
        {
            var dateTimeValue = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 00:00 ق.ظ");
            fItem.Operator = FilterOperator.IsLessThan;
            fItem.Value = dateTimeValue;
            var compositeFilter = new CompositeFilterDescriptor();
            var secondFilterRule = new FilterDescriptor();
            secondFilterRule.Operator = FilterOperator.IsGreaterThan;
            secondFilterRule.Value = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 23:59 ب.ظ");
            secondFilterRule.Member = fItem.Member;
            compositeFilter.LogicalOperator = FilterCompositionLogicalOperator.Or;
            compositeFilter.FilterDescriptors.Add(fItem);
            compositeFilter.FilterDescriptors.Add(secondFilterRule);
            return compositeFilter;
        }

        private static void getDateRangeCompositeFilterConditionForLessThan(FilterDescriptor fItem, string filterVal)
        {
            var dateTimeValue = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 00:00 ب.ظ");
            fItem.Value = dateTimeValue;
        }

        private static void getDateRangeCompositeFilterConditionForGreaterThan(FilterDescriptor fItem, string filterVal)
        {
            var dateTimeValue = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 00:00 ق.ظ");
            fItem.Value = dateTimeValue;

        }

        private static CompositeFilterDescriptor getDateRangeCompositeFilterConditionForEquality(FilterDescriptor fItem, string filterVal)
        {
            var dateTimeValue = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 00:00 ق.ظ");
            fItem.Operator = FilterOperator.IsGreaterThanOrEqualTo;
            fItem.Value = dateTimeValue;
            var compositeFilter = new CompositeFilterDescriptor();
            var secondFilterRule = new FilterDescriptor();
            secondFilterRule.Operator = FilterOperator.IsLessThan;
            secondFilterRule.Value = PersianDateConverter.ToGregorianDateTime(filterVal.Split(',')[0] + " 23:59 ب.ظ");
            secondFilterRule.Member = fItem.Member;
            compositeFilter.LogicalOperator = FilterCompositionLogicalOperator.And;
            compositeFilter.FilterDescriptors.Add(fItem);
            compositeFilter.FilterDescriptors.Add(secondFilterRule);
            return compositeFilter;
        }
    }
}
