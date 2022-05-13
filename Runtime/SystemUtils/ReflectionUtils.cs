//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace CippSharp.Core.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Common binding flags for most public and non public methods.
        /// </summary>
        public const BindingFlags Common = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// "Caught exception: " prefix
        /// </summary>
        private const string CaughtExceptionPrefix = "Caught exception: ";
        
        /// <summary>
        /// A better name for logs
        /// </summary>
        private static readonly string LogName = $"[{nameof(ReflectionUtils)}]: ";

        #region Generic → Find Type(s)
        
        /// <summary>
        /// Find type via string
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <param name="foundType"></param>
        /// <returns></returns>
        public static bool FindType(string typeFullName, out Type foundType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName != typeFullName)
                    {
                        continue;
                    }
                    
                    foundType = type;
                    return true;
                }
            }

            foundType = null;
            return false;
        }

        /// <summary>
        /// Find types via predicate
        /// </summary>
        /// <param name="predicate">must not be null</param>
        /// <param name="foundTypes"></param>
        /// <returns></returns>
        public static bool FindTypes(Predicate<Type> predicate, out List<Type> foundTypes)
        {
            foundTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies() from type in assembly.GetTypes() where predicate.Invoke(type) select type).ToList();
            return foundTypes.Count > 0;
        }
        
        #endregion
        
        #region Generic → Create Instance
        
        /// <summary>
        /// Create an instance of a type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="flags"></param>
        /// <returns>success</returns>
        public static bool TryCreateInstance(Type type, out object instance, BindingFlags flags = Common)
        {
            try
            {
                ConstructorInfo constructor = type.GetConstructors(flags).FirstOrDefault(c => c.GetParameters().Length == 0);
                if (constructor == null)
                {
                    instance = null;
                    return false;
                }

                instance = constructor.Invoke(null);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(null, nameof(HasField), e, out UnityObject o), o);
                instance = null;
                return false;
            }
        }
        
        #endregion
        
        #region Generic → Methods
        
        #region Reflection → Get Public Constants Fields
        
        /// <summary>
        /// Old way to retrieve all public constant fields of a type
        /// 
        /// https://stackoverflow.com/questions/10261824/how-can-i-get-all-constants-of-a-type-by-reflection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FieldInfo[] GetPublicConstantsFields(Type type)
        {
            ArrayList constants = new ArrayList();

            FieldInfo[] fieldInfos = type.GetFields(
                // Gets all public and static fields
                BindingFlags.Public | BindingFlags.Static | 
                // This tells it to get the fields from all base types as well
                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach(FieldInfo fi in fieldInfos)
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determines if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if(fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi);           

            // Return an array of FieldInfos
            return (FieldInfo[])constants.ToArray(typeof(FieldInfo));
        }
        
        #endregion
        
        #region Reflection → Get Enum Attributes

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
        
        /// <summary>
        /// Gets attributes on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T[] GetAttributesOfType<T>(Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return ArrayUtils.SelectIf(attributes, a => a is T, a => (T)a).ToArray();
        }

        #endregion
        
        #region Reflection → Print Stuffs
        
        /// <summary>
        /// Log methods of type in console.
        /// </summary>
        public static void PrintMethods(Type type, BindingFlags flags = Common, UnityEngine.Object context = null)
        {
            List<string> methodNames = new List<string>();
            foreach (var methodInfo in type.GetMethods(flags))
            {
                string methodName = methodInfo.Name;
                methodNames.Add(methodName);
                string logName = StringUtils.LogName(context);
                string message = string.Format("Method name: <i>{0}</i>, Overload Count: <i>{1}</i>.", methodName, methodNames.Count(m => m == methodName));
                Debug.Log(logName+message, context);
            }
        }
        
        /// <summary>
        /// Log members of type in console.
        /// </summary>
        public static void PrintMembers(Type type, BindingFlags flags = Common, UnityEngine.Object context = null)
        {
            List<string> membersNames = new List<string>();
            foreach (var members in type.GetMembers(flags))
            {
                string memberName = members.Name;
                membersNames.Add(memberName);
                string logName = StringUtils.LogName(context);
                string message = string.Format("Method name: <i>{0}</i>, Overload Count: <i>{1}</i>.", memberName, membersNames.Count(m => m == memberName));
                Debug.Log(logName+message, context);
            }
        }

        #endregion
        
        #endregion

        #region Generic → Is
        
        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(MemberInfo member)
        {
            return member is FieldInfo;
        }

        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(MemberInfo member, out FieldInfo field)
        {
            if (member is FieldInfo f)
            {
                field = f;
                return true;
            }
            else
            {
                field = null;
                return false;
            }
        }
        
        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(MemberInfo member)
        {
            return member is PropertyInfo;
        }

        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(MemberInfo member, out PropertyInfo property)
        {
            if (member is PropertyInfo p)
            {
                property = p;
                return true;
            }
            else
            {
                property = null;
                return false;
            }
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(MemberInfo member)
        {
            return member is MethodInfo;
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(MemberInfo member, out  MethodInfo method)
        {
            if (member is MethodInfo m)
            {
                method = m;
                return true;
            }
            else
            {
                method = null;
                return false;
            }
        }

        #endregion
        
        
        #region Reflection → MemberInfo(s)
        
        /// <summary>
        /// Returns true if the context object has the target member.
        /// It also throws out the interested member.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memberName"></param>
        /// <param name="member"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasMember(object context, string memberName, out MemberInfo member, BindingFlags flags = Common)
        {
            try
            {
                member = context.GetType().GetMember(memberName, flags).FirstOrDefault();
                return member != null;
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(HasMember), e, out UnityObject o), o);
                
                member = null;
                return false;
            }            
        }

        #region → Get Value
        
        /// <summary>
        /// Returns the value of target member if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memberName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetMemberValue<T>(object context, string memberName, out T result, BindingFlags bindingFlags = Common)
        {
            try
            {
                MemberInfo member = context.GetType().GetMember(memberName, bindingFlags).FirstOrDefault();
                return TryGetMemberValue(context, member, out result);
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TryGetMemberValue), e, out UnityObject o), o);
            }
            
            result = default(T);
            return false;
        }
        
        /// <summary>
        /// If you already have the member, it returns the value of target member
        /// if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>success</returns>
        public static bool TryGetMemberValue<T>(object context, MemberInfo member, out T result)
        {
            try
            {
                if (member != null)
                {
                    if (IsFieldInfo(member, out FieldInfo f))
                    {
                        result = (T) f.GetValue(context);
                        return true;
                    }
                    else if (IsPropertyInfo(member, out PropertyInfo p))
                    {
                        result = (T) p.GetValue(context, null);
                        return true;
                    }
                    else if (IsMethodInfo(member, out MethodInfo m))
                    {
                        result = (T) m.Invoke(context, null);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TryGetMemberValue), e, out UnityObject o), o);
            }
            
            result = default(T);
            return false;
        }
        
        #endregion

        #region → Set Value

        public static bool TrySetMemberValue(object context, string memberName, object memberValue, out object result, BindingFlags bindingFlags = Common)
        {
            try
            {
                MemberInfo member = context.GetType().GetMember(memberName, bindingFlags).FirstOrDefault();
                return TrySetMemberValue(context, member, memberValue, out result);
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TrySetMemberValue), e, out UnityObject o), o);
            }
            
            result = null;
            return false;
        }

        
        /// <summary>
        /// Try to set a member value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <param name="memberValue">in case of MethodInfo this is 'parameters' or one parameter</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TrySetMemberValue(object context, MemberInfo member, object memberValue, out object result)
        {
            if (member == null)
            {
                result = null;
                return false;
            }

            try
            {
                if (IsFieldInfo(member, out FieldInfo f))
                {
                    result = null;
                    f.SetValue(context, memberValue);
                    return true;
                }
                else if (IsPropertyInfo(member, out PropertyInfo p))
                {
                    result = null;
                    p.SetValue(context, memberValue);
                    return true;
                }
                else if (IsMethodInfo(member, out MethodInfo m))
                {
                    if (ArrayUtils.TryToObjectArray(memberValue, out object[] parameters))
                    {
                        result = m.Invoke(context, parameters);
                        return true;
                    }
                    else
                    {
                        result = m.Invoke(context, new []{memberValue});
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TrySetMemberValue), e, out UnityObject o), o);
            }
            
            result = null;
            return false;
        }

        #endregion
        
        #endregion
        
        #region Reflection → FieldInfo(s)
        
        /// <summary>
        /// Returns true if the context object has the target field. It also throws out the interested field.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasField(object context, string fieldName, out FieldInfo field, BindingFlags flags = Common)
        {
            try
            {
                field = context.GetType().GetField(fieldName, flags);
                return field != null;
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(HasField), e, out UnityObject o), o);
                
                field = null;
                return false;
            }            
        }

        #region → Get Value

        /// <summary>
        /// Returns the value of target field if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static T GetFieldValueOrDefault<T>(object context, string fieldName, BindingFlags bindingFlags = Common)
        {
            TryGetFieldValue(context, fieldName, out T result, bindingFlags);
            return result;
        }

        /// <summary>
        /// Try to return the value of target field if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetFieldValue<T>(object context, string fieldName, out T result, BindingFlags bindingFlags = Common)
        {
            try
            {
                FieldInfo fieldInfo = context.GetType().GetField(fieldName, bindingFlags);
                if (fieldInfo != null)
                {
                    result = (T) fieldInfo.GetValue(context);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TryGetFieldValue), e, out UnityObject o), o);
            }
            
            result = default(T); 
            return false;
        }

        #endregion
        
        #region → Set Value
        
        /// <summary>
        /// Returns true if successful set the new value to the field.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetFieldValue(object context, string fieldName, object fieldValue, BindingFlags flags = Common)
        {
            return TrySetFieldValue(ref context, fieldName, fieldValue);
        }

        /// <summary>
        /// Returns true if successful set the new value to the field.
        /// 
        /// NOTE: ref supports 'ValueTypes' numbers and 'structs'
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetFieldValue(ref object context, string fieldName, object fieldValue, BindingFlags flags = Common)
        {
            try
            {
                FieldInfo fieldInfo = context.GetType().GetField(fieldName, flags);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(context, fieldValue);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TrySetFieldValue), e, out UnityObject o), o);
            }

            return false;
        }
        
        #endregion

        #endregion
        
        #region Reflection → PropertyInfo(s)
        
        /// <summary>
        /// Returns true if the context object has the target property. It also throws out the interested property.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="property"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasProperty(object context, string propertyName, out PropertyInfo property, BindingFlags flags = Common)
        {
            try
            {
                property = context.GetType().GetProperty(propertyName, flags);
                return property != null;
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(HasProperty), e, out UnityObject o), o);
                
                property = null;
                return false;
            }
        }

        #region → Get Value
        
        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="contextType"></param>
        /// <param name="propertyName"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetStaticPropertyValueOrDefault<T>(Type contextType, string propertyName, BindingFlags bindingFlags = Common)
        {
            TryGetStaticPropertyValue(contextType, propertyName, out T result, bindingFlags);
            return result;
        }
        
        /// <summary>
        /// Try to retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="contextType"></param>
        /// <param name="propertyName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetStaticPropertyValue<T>(Type contextType, string propertyName, out T result, BindingFlags bindingFlags = Common)
        {
            try
            {
                bindingFlags |= BindingFlags.Static;
                PropertyInfo property = contextType.GetProperty(propertyName, bindingFlags);
                if (property != null)
                {
                    result = (T) property.GetValue(null);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(null, nameof(TryGetStaticPropertyValue), e, out UnityObject o), o);
            }

            result = default(T);
            return false;
        }
        
        
        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object context, string propertyName, BindingFlags bindingFlags = Common)
        {
            TryGetPropertyValue(context, propertyName, out T result, bindingFlags);
            return result;
        }

        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        public static bool TryGetPropertyValue<T>(object context, string propertyName, out T result, BindingFlags bindingFlags = Common)
        {
            try
            {
                PropertyInfo propertyInfo = context.GetType().GetProperty(propertyName, bindingFlags);
                if (propertyInfo != null)
                {
                    result = (T) propertyInfo.GetValue(context, null);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TryGetPropertyValue), e, out UnityObject o), o);
            }
            
            result = default(T);
            return false;
        }
        
        #endregion
        
        #region → Set Value

        /// <summary>
        /// Returns true if successful set the new value to the property.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetPropertyValue(object context, string propertyName, object propertyValue, BindingFlags flags = Common)
        {
            return TrySetPropertyValue(ref context, propertyName, propertyValue, flags);
        }

        /// <summary>
        /// Returns true if successful set the new value to the property.
        ///
        /// NOTE: ref supports 'ValueTypes' numbers and 'structs'
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetPropertyValue(ref object context, string propertyName, object propertyValue, BindingFlags flags = Common)
        {
            try
            {
                PropertyInfo propertyInfo = context.GetType().GetProperty(propertyName, flags);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(context, propertyValue);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TrySetPropertyValue), e, out UnityObject o), o);
            }
            
            return false;
        }
        
        #endregion
        
        #endregion

        #region Reflection → MethodInfo(s)

        #region → Has MethodInfo
      
        /// <summary>
        /// Find a method via string
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="methodInfo"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool FindMethod(Type type, string methodName, out MethodInfo methodInfo, BindingFlags flags = Common)
        {
            methodInfo = type.GetMethod(methodName, flags);
            return methodInfo != null;
        }
        
        /// <summary>
        /// Returns true if the context object has the target method. It also throws out the interested method.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="method"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasMethod(object context, string methodName, out MethodInfo method, BindingFlags flags = Common)
        {
            try
            {
                method = context.GetType().GetMethod(methodName, flags);
                return method != null;
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(HasMethod), e, out UnityObject o), o);
                
                method = null;
                return false;
            }
        }
        
        #endregion
        
        /// <summary>
        /// Check a condition on parameters of a method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool HasParametersCondition(MethodInfo method, Predicate<ParameterInfo[]> predicate)
        {
            try
            {
                ParameterInfo[] parameters = method.GetParameters();
                return predicate.Invoke(parameters);
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(null, nameof(HasParametersCondition), e, out UnityObject o), o);                return false;
            }
        }

        #region → Invoke
        
        /// <summary>
        /// Call method if exists on target object.
        /// 
        /// Different wording of <see cref="TryCallMethod"/> 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <param name="flags"></param>
        /// <returns>success</returns>
        public static bool TryInvokeMethod(object context, string methodName, out object result, object[] parameters = null, BindingFlags flags = Common)
        {
            return TryCallMethod(context, methodName, out result, parameters, flags);
        }

        /// <summary>
        /// Call method if exists on target object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <param name="flags"></param>
        /// <returns>success</returns>
        public static bool TryCallMethod(object context, string methodName, out object result, object[] parameters = null, BindingFlags flags = Common)
        {
            try
            {
                MethodInfo methodInfo = context.GetType().GetMethod(methodName, flags);
                if (methodInfo != null)
                {
                    result = methodInfo.Invoke(context, parameters);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(MessageGenericExceptionError(context, nameof(TryCallMethod), e, out UnityObject o), o); 
            }

            result = null;
            return false;
        }
        
        #endregion
        
        #endregion
        
        
        private static string MessageGenericExceptionError(object context, string methodName, Exception e, out UnityObject o)
        {
            o = context as UnityObject;
            string logName = o != null ? StringUtils.LogName(o) : LogName;
            return logName + $"{methodName} failed. {CaughtExceptionPrefix}{e.Message}.";
        }
    }
}
