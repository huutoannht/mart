using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Data.DataContract;
using Data.DataContract.BeUserDC;
using Data.DataContract.CategoryDC;
using Data.DataContract.ProductDC;
using Share.Helper.Models;
using ML.Common;

namespace Share.Helper.Extension
{
    public static class SelectListExtension
    {
        public static SelectList ToSelectList(this object sources, object selectedValue = null)
        {
            string dataText = string.Empty, dataValue = string.Empty;
            var sourceToEnumerable = SourceToEnumerable(sources, ref dataText, ref dataValue);

            if (sourceToEnumerable != null)
            {
                return new SelectList(sourceToEnumerable, dataValue, dataText, selectedValue);
            }

            var items = new List<string>();

            if (selectedValue != null)
            {
                items.Add(selectedValue.ToString());
            }

            return new SelectList(items, selectedValue);
        }

        public static SelectList ToSelectList(this object sources, string dataValueField, string dataTextField, object selectedValue = null)
        {
            string dataText = string.Empty, dataValue = string.Empty;
            var sourceToEnumerable = SourceToEnumerable(sources, ref dataText, ref dataValue);

            if (sourceToEnumerable != null)
            {
                return new SelectList(sourceToEnumerable, dataValueField, dataTextField, selectedValue);
            }

            var items = new List<string>();

            if (selectedValue != null)
            {
                items.Add(selectedValue.ToString());
            }

            return new SelectList(items, selectedValue);
        }

        public static SelectList ToMoneySelectList(this List<decimal> sources, decimal? selectedValue = null)
        {
            if (sources != null)
            {
                return new SelectList(sources.Select(e => new SelectListItem
                {
                    Text = e.ToLocalMoneyString(),
                    Value = e.ToString()
                }), "Value", "Text", selectedValue);                
            }

            return new SelectList(new List<string>());
        }

        public static MultiSelectList ToMultiSelectList(this object sources, object selectedValue = null)
        {
            return sources.ToMultiSelectList(null, null, selectedValue);
        }

        public static MultiSelectList ToMultiSelectList(this object sources, string dataValueField, string dataTextField, object selectedValue = null)
        {
            string dataText = string.Empty, dataValue = string.Empty;
            var sourceToEnumerable = SourceToEnumerable(sources, ref dataText, ref dataValue);
            
            if (string.IsNullOrEmpty(dataTextField)) dataTextField = dataText;
            if (string.IsNullOrEmpty(dataValueField)) dataValueField = dataValue;

            if (sourceToEnumerable != null)
            {
                return new MultiSelectList(sourceToEnumerable, dataValueField, dataTextField, selectedValue.SelectedValueToEnumerable());
            }

            var items = new List<string>();

            if (selectedValue != null)
            {
                items.Add(selectedValue.ToString());
            }

            return new MultiSelectList(items, selectedValue.SelectedValueToEnumerable());
        }

        private static IEnumerable SourceToEnumerable(object sources, ref string dataTextField, ref  string dataValueField)
        {
            if (sources != null)
            {
                dataTextField = "Name"; dataValueField = "Id";

                if (sources is List<Reference>)
                {
                    return sources as List<Reference>;
                }

                if (sources is List<ReferenceModel>)
                {
                    return sources as List<ReferenceModel>;
                }
				
                if (sources is List<TextValueModel>)
                {
                    dataTextField = "Text"; dataValueField = "Value";
                    return sources as List<TextValueModel>;
                }

                if (sources is List<EnumModel>)
                {
                    dataTextField = "Name"; dataValueField = "Value";
                    return sources as List<EnumModel>;
                }

                if (sources is List<Category>)
                {
                    dataTextField = "Name"; dataValueField = "Id";
                    return sources as List<Category>;
                }

                if (sources is List<BeUser>)
                {
                    dataTextField = "FullName"; dataValueField = "Id";
                    return sources as List<BeUser>;
                }

                if (sources is List<Product>)
                {
                    dataTextField = "Name"; dataValueField = "Id";
                    return sources as List<Product>;
                }

                if (sources is List<string>)
                {
                    dataTextField = ""; dataValueField = "";
                    return sources as List<string>;
                }

                if (sources is List<int>)
                {
                    dataTextField = "";
                    dataValueField = "";
                    return sources as List<int>;
                }

                if (sources is List<decimal>)
                {
                    dataTextField = "";
                    dataValueField = "";
                    return sources as List<decimal>;
                }

                if (sources is Dictionary<string, string>)
                {
                    dataTextField = "Value"; dataValueField = "Key";
                    return sources as Dictionary<string, string>;
                }
			}

            return null;
        }

        private static IEnumerable SelectedValueToEnumerable(this object selectedValues)
        {
            if (selectedValues != null)
            {
                if (selectedValues is List<Guid>)
                {
                    return selectedValues as List<Guid>;
                }

                if (selectedValues is List<string>)
                {
                    return selectedValues as List<string>;
                }
            }

            return null;
        }
    }
}