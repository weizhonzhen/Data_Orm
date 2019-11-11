﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastData.CacheModel;
using FastData.Base;
using FastData.Config;

namespace FastData.Property
{
    /// <summary>
    /// 缓存类
    /// </summary>
    internal static class PropertyCache
    {
        #region 泛型缓存属性成员
        /// <summary>
        /// 泛型缓存属性成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<PropertyModel> GetPropertyInfo<T>(bool IsCache = true)
        {
            var list = new List<PropertyModel>();
            var key = string.Format("{0}.{1}", typeof(T).Namespace, typeof(T).Name);
            var config = DataConfig.GetConfig();

            if (IsCache)
            {
                if (DbCache.Exists(config.CacheType,key))
                    return DbCache.Get<List<PropertyModel>>(config.CacheType, key);
                else
                {
                    typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(a =>
                        {
                            var temp = new PropertyModel();
                            temp.Name = a.Name;
                            temp.PropertyType = a.PropertyType;
                            list.Add(temp);
                        });

                    DbCache.Set<List<PropertyModel>>(config.CacheType,key, list);
                }
            }
            else
            {
                DbCache.Remove(config.CacheType,key);
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(a =>
                {
                    var temp = new PropertyModel();
                    temp.Name = a.Name;
                    temp.PropertyType = a.PropertyType;
                    list.Add(temp);
                });
            }

            return list;
        }
        #endregion

        #region 缓存发属性成员
        public static List<PropertyModel> GetPropertyInfo(object model, bool IsCache = true)
        {
            var list = new List<PropertyModel>();
            var key = string.Format("{0}.{1}", model.GetType().Namespace, model.GetType().Name);
            var config = DataConfig.GetConfig();

            if (IsCache)
            {
                if (DbCache.Exists(config.CacheType, key))
                    return DbCache.Get<List<PropertyModel>>(config.CacheType, key);
                else
                {
                    model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(a =>
                    {
                        var temp = new PropertyModel();
                        temp.Name = a.Name;
                        temp.PropertyType = a.PropertyType;
                        list.Add(temp);
                    });

                    DbCache.Set<List<PropertyModel>>(config.CacheType, key, list);
                }
            }
            else
            {
                model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(a =>
                {
                    var temp = new PropertyModel();
                    temp.Name = a.Name;
                    temp.PropertyType = a.PropertyType;
                    list.Add(temp);
                });
            }

            return list;
        }
        #endregion

        #region 泛型特性成员
        /// <summary>
        /// 泛型特性成员
        /// </summary>
        public static List<ColumnModel> GetAttributesColumnInfo(string tableName, List<PropertyInfo> ListInfo)
        {
            var list = new List<ColumnModel>();

            foreach (var info in ListInfo)
            {
                var temp = new ColumnModel();
                temp.Name = info.Name;
                var dynSet = new DynamicSet<ColumnModel>();
                var paramList = GetPropertyInfo<ColumnModel>(true);

                foreach (var item in info.CustomAttributes)
                {
                    foreach (var param in item.NamedArguments)
                    {
                        if (paramList.Exists(b => b.Name.ToLower() == param.MemberName.ToLower()))
                            dynSet.SetValue(temp, param.MemberName, param.TypedValue.Value, true);
                    }
                }

                list.Add(temp);
            }
            
            return list;
        }
        #endregion

        #region 泛型缓存特性成员
        /// <summary>
        /// 泛型缓存特性成员
        /// </summary>
        public static string GetAttributesTableInfo(List<Attribute> listAttribute)
        {
            var result = "";
            foreach(var item in listAttribute)
            {
                if (item is TableAttribute)
                    result = (item as TableAttribute).Comments;
            }

            return result;
        }
        #endregion
    }
}
