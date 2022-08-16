using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.Generic;
using Common.Application.Contracts;
using static Common.Application.Strings.English;

namespace Common.Application
{
    //  got help from: https://www.pluralsight.com/guides/property-copying-between-two-objects-using-reflection
    public static class Projection
    {
        #region Enums
        public enum CalendarMode
        {
            ToGregorian,
            ToJalali
        }

        public enum DateTimeMode
        {
            OnlyDate,
            BothDateAndTime
        }

        public enum SelectListType
        {
            AllRecords,
            JustNonRemoved
        }

        public enum NullOrEmptyPicture
        {
            Allow = 0,
            Ignore = 1,
        }
        #endregion

        public static T DecompressFromJson<T>(this T self) where T : IJson
        {
            var selfProps = self.GetType().GetProperties();
            var jsonObject = JsonConvert.DeserializeObject<T>(self.Json);
            var jsonProps = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

            foreach (var selfProp in selfProps)
                if (jsonProps.Any(x => x.Name == selfProp.Name))
                {
                    var jsonProp = jsonProps.FirstOrDefault(x => x.Name == selfProp.Name);

                    if (jsonProp.Name.Contains("Date"))
                    { 
                        var gregorian = DateTime.Parse((string)jsonProp.GetValue(jsonObject));
                        var dateString = gregorian.ToString();
                        selfProp.SetValue(self, dateString);
                    }

                    else
                    {
                        var jsonValue = jsonProp?.GetValue(jsonObject) ?? null;
                        selfProp.SetValue(self, jsonValue);
                    }
                }

            return self;
        }

        public static string CompressIntoJson<T>(this T self)
        {
            Dictionary<string, object> toJson = new();
            var selfProps = self.GetType().GetProperties();
            var baseProps = self.GetType().BaseType.GetProperties();
            var jsonProps = selfProps.Where(x => baseProps.All(y => x.Name != y.Name)).ToArray();

            foreach (var jsonProp in jsonProps)
            {
                var selfValue = selfProps.FirstOrDefault(x => x.Name == jsonProp.Name)?.GetValue(self);

                if (jsonProp.Name.Contains("Date"))
                {
                    if (selfValue is not null)
                    {
                        var persianDateString = (string)selfValue;
                        var gregorianDate = persianDateString.ToGregorianDateTime();
                        toJson.Add(jsonProp.Name, gregorianDate);
                    }
                }

                else toJson.Add(jsonProp.Name, selfValue);
            }

            return JsonConvert.SerializeObject(toJson);
        }

        public static IQueryable<TTarget> FromList<TTarget, TSource>(this TTarget self, IEnumerable<TSource> sources, DateTimeMode dateTimeMode = DateTimeMode.OnlyDate)
        {
            List<TTarget> output = new();
            var toProperties = self.GetType().GetProperties();

            foreach (var source in sources)
            {
                TTarget target = ((TTarget)Activator.CreateInstance(typeof(TTarget), Array.Empty<object>()));

                var fromProperties = source.GetType().GetProperties();

                foreach (var fromProperty in fromProperties)
                    foreach (var toProperty in toProperties)
                        if (fromProperty.Name == toProperty.Name)
                        {
                            if (fromProperty.PropertyType == toProperty.PropertyType)
                            {
                                if (fromProperty.Name == "Picture")
                                {
                                    string picturePath = fromProperty.GetValue(source) as string;

                                    if (string.IsNullOrWhiteSpace(picturePath))
                                        toProperty.SetValue(target, Query.NoPictureProductFilePath);

                                    else
                                        toProperty.SetValue(target, picturePath);
                                }

                                else
                                    toProperty.SetValue(target, fromProperty.GetValue(source));
                            }

                            else
                            {
                                if (fromProperty.Name.Contains("Date"))
                                {
                                    var dateString = string.Empty;

                                    if (dateTimeMode == DateTimeMode.OnlyDate)
                                        dateString = ((DateTime)fromProperty.GetValue(source)).ToJalaliString();

                                    else if (dateTimeMode == DateTimeMode.BothDateAndTime)
                                        dateString = ((DateTime)fromProperty.GetValue(source)).ToJalaliFullString();

                                    toProperty.SetValue(target, dateString);
                                }
                            }

                            break;
                        }

                output.Add(target);
            }

            return output.AsQueryable();
        }

        public static List<TTarget> GetSelectList<TTarget, TSource>(this TTarget self, IEnumerable<TSource> sources, SelectListType selectListType = SelectListType.AllRecords)
        {
            List<TTarget> output = new();
            var toProperties = self.GetType().GetProperties();

            foreach (var source in sources)
            {
                TTarget target = (TTarget)Activator.CreateInstance(typeof(TTarget), Array.Empty<object>());
                var fromProperties = source.GetType().GetProperties();

                foreach (var fromProperty in fromProperties)
                    foreach (var toProperty in toProperties)
                        if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                        {
                            if (fromProperty.Name.ToLower().Contains("slug"))
                                toProperty.SetValue(target, fromProperty.GetValue(source));

                            if (fromProperty.Name.ToLower().Contains("name"))
                                toProperty.SetValue(target, fromProperty.GetValue(source));

                            else if (fromProperty.Name.ToLower().Contains("id"))
                                toProperty.SetValue(target, fromProperty.GetValue(source));

                            else if (fromProperty.Name.ToLower() == "isremoved")
                                toProperty.SetValue(target, fromProperty.GetValue(source));

                            break;
                        }

                output.Add(target);
            }

            List<TTarget> selectedList = new(output);

            if (!toProperties.Any(x => x.Name.ToLower().Equals("isremoved")))
                return selectedList;

            if (selectListType == SelectListType.JustNonRemoved)
            {
                selectedList = new();
                foreach (var each in output)
                {
                    var fromProperties = each.GetType().GetProperties();

                    foreach (var fromProperty in fromProperties)
                        if (fromProperty.Name == "IsRemoved")
                        {
                            var value = (bool)fromProperty.GetValue(each);
                            if (!value)
                            {
                                selectedList.Add(each);
                                break;
                            }
                        }
                }
            }

            return selectedList;
        }

        public static TTarget From<TTarget, TSource>(this TTarget self, TSource source, CalendarMode calendarMode = CalendarMode.ToGregorian, NullOrEmptyPicture nullOrEmptyPicture = NullOrEmptyPicture.Allow)
        {
            if (source is null) return default;

            var fromProperties = source.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
                foreach (var toProperty in toProperties)
                    if (fromProperty.Name == toProperty.Name)
                    {
                        if (fromProperty.PropertyType == toProperty.PropertyType)
                        {
                            if (fromProperty.Name == "IsRemoved") continue;

                            else if (fromProperty.Name.Equals("Picture"))
                            {
                                string value = fromProperty.GetValue(source) as string;

                                if (!string.IsNullOrWhiteSpace(value))
                                    toProperty.SetValue(self, fromProperty.GetValue(source));

                                else if (nullOrEmptyPicture == NullOrEmptyPicture.Allow)
                                    toProperty.SetValue(self, string.IsNullOrWhiteSpace(value) ? Query.NoPictureProductFilePath : value);
                            }

                            else
                                toProperty.SetValue(self, fromProperty.GetValue(source));
                        }

                        else
                        {
                            if (fromProperty.Name.Contains("Date"))
                            {
                                if (calendarMode == CalendarMode.ToJalali)
                                {
                                    var date = (DateTime)fromProperty.GetValue(source);
                                    var dateString = date.ToString();

                                    toProperty.SetValue(self, dateString);
                                }

                                else if (calendarMode == CalendarMode.ToGregorian)
                                {
                                    var src = (string)fromProperty.GetValue(source);

                                    if (src is not null)
                                    {
                                        var date = src.ToGregorianDateTime();
                                        toProperty.SetValue(self, date);
                                    }
                                }
                            }
                        }

                        break;
                    }

            return self;
        }
    }
}
