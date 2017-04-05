//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// 构造器反射策略，此策略主要包含两个任务，一个是选择构造器，二是创建该构造函数参数列表，然后设置<see cref="ConstructorPolicy"/>政策方法(二级策略)
    /// </summary>
    public class ConstructorReflectionStrategy : ReflectionStrategy<ConstructorInfo>
    {
        /// <summary>
        /// See <see cref="ReflectionStrategy{T}.GetMembers"/> for more information.
        /// </summary>
        protected override IEnumerable<IReflectionMemberInfo<ConstructorInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            List<IReflectionMemberInfo<ConstructorInfo>> result = new List<IReflectionMemberInfo<ConstructorInfo>>();
            ICreationPolicy existingPolicy = context.Policies.Get<ICreationPolicy>(typeToBuild, idToBuild);

            if (existing == null && (existingPolicy == null || existingPolicy is DefaultCreationPolicy))
            {
                ConstructorInfo injectionCtor = null;
                ConstructorInfo[] ctors = typeToBuild.GetConstructors();

                if (ctors.Length == 1)
                    injectionCtor = ctors[0];
                else
                {
                    foreach (ConstructorInfo ctor in ctors)
                    {
                        if (Attribute.IsDefined(ctor, typeof(InjectionConstructorAttribute)))
                        {
                            // Multiple decorated constructors aren't valid
                            if (injectionCtor != null)
                                throw new InvalidAttributeException();

                            injectionCtor = ctor;
                        }
                    }
                }

                if (injectionCtor != null)
                    result.Add(new ReflectionMemberInfo<ConstructorInfo>(injectionCtor));
            }

            return result;
        }

        /// <summary>
        /// See <see cref="ReflectionStrategy{T}.AddParametersToPolicy"/> for more information.
        /// </summary>
        protected override void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<ConstructorInfo> member, IEnumerable<IParameter> parameters)
        {
            ConstructorPolicy policy = new ConstructorPolicy();

            foreach (IParameter parameter in parameters)
                policy.AddParameter(parameter);

            context.Policies.Set<ICreationPolicy>(policy, typeToBuild, idToBuild);
        }

        /// <summary>
        /// See <see cref="ReflectionStrategy{T}.MemberRequiresProcessing"/> for more information.
        /// </summary>
        protected override bool MemberRequiresProcessing(IReflectionMemberInfo<ConstructorInfo> member)
        {
            return true;
        }
    }
}
